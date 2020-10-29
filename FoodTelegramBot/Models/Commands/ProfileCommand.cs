using FoodTelegramBot.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FoodTelegramBot.Models.Commands
{
    public class ProfileCommand : CommandBase
    {
        private readonly TelegramBotContext _db;

        public ProfileCommand(TelegramBotContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), " was null");
        }

        public override string Name => @"/profile";

        public override async Task<OperationsDetails> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var currentUser = await _db.Users.Include(u => u.Country).Include(u => u.Cart).FirstOrDefaultAsync(u => u.ChatId == chatId);
            
            if(currentUser != null)
            {
                var data = GetProfileData(currentUser);
                await client.SendTextMessageAsync(chatId, $"Ваша информация:\n{data}\n" +
                                                          $"Вы можете изменить такие настройки как: имя, фамилия, никнейм, страну и город. Комманды для изменений: \n" +
                                                          $"/changeName@{AppConfig.Name} - изменить имя\n" +
                                                          $"/changeLastName@{AppConfig.Name} - изменить фамилия\n" +
                                                          $"/changeUserName@{AppConfig.Name} - изменить ник\n" +
                                                          $"/changeCountry@{AppConfig.Name} - сменить страну\n" +
                                                          $"/changeCity@{AppConfig.Name} - сменить город\n" +
                                                          $"/changePhoneNumber@{AppConfig.Name} - сменить номер телефона\n" +
                                                          $"/changeEmail@{AppConfig.Name} - сменить почту\n" +
                                                          $"Если вы не заполните эту информацию, то вы не сможете заказать продукты в нашем боте!");

                return new OperationsDetails("Data is sended", true);
            }

            return new OperationsDetails("User is null", false);
        }

        private static string GetProfileData(DB.Entities.User currentUser)
        {
            var profileData = new StringBuilder();
            profileData.Append($"Номер чата: {currentUser.ChatId}\n");
            profileData.Append($"Имя: {CheckIfNull(currentUser.FirstName)}\n");
            profileData.Append($"Фамилия: {CheckIfNull(currentUser.LastName)}\n");
            profileData.Append($"Никнейм: {CheckIfNull(currentUser.UserName)}\n");
            profileData.Append($"Номер телефона: {CheckIfNull(currentUser.PhoneNumber)}\n");
            profileData.Append($"Електронная почта: {CheckIfNull(currentUser.Email)}\n");
            profileData.Append($"Страна: {CheckIfNull(currentUser.Country.Name)}\n");
            profileData.Append($"Город: {CheckIfNull(currentUser.Country.City)}\n");

            return profileData.ToString();
        }

        private static string CheckIfNull(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                return "Не указано";
            else
                return data;
        }

        public override bool IsContains(Message message)
        {
            return message.Text.Contains(Name);
        }
    }
}
