using FoodTelegramBot.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FoodTelegramBot.Models.Commands.ProfileSettingCommands
{
    public class ChangeCityCommand : CommandBase
    {
        private readonly TelegramBotContext _db;

        public ChangeCityCommand(TelegramBotContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), " was null");
        }

        public override string Name => @"/changeCity";

        public override async Task<OperationsDetails> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var words = message.Text.Split('@');

            if (words[1] != "YourNewCity")
            {
                var currentUser = await _db.Users.Include(u => u.Country).FirstOrDefaultAsync(u => u.ChatId == chatId);

                if (currentUser != null)
                {
                    currentUser.Country.City = words[1];

                    await _db.SaveChangesAsync();

                    try
                    {
                        await client.SendTextMessageAsync(chatId, $"Ваш город изменён на: {words[1]}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    return new OperationsDetails("City is changed", true);
                }

                return new OperationsDetails("User is null", false);
            }

            try
            {
                await client.SendTextMessageAsync(chatId, "Пожалуйста, введите вместо YourNewCity свой новый город");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new OperationsDetails("Not valid city", false);
        }

        public override bool IsContains(Message message)
        {
            return message.Text.Contains(Name);
        }
    }
}
