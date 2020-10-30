using FoodTelegramBot.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FoodTelegramBot.Models.Commands
{
    public class ChangeCountryNameCommand : CommandBase
    {
        private readonly TelegramBotContext _db;

        public ChangeCountryNameCommand(TelegramBotContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), " was null");
        }

        public override string Name => @"/changeCountry";

        public override async Task<OperationsDetails> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var words = message.Text.Split('@');

            if (words[1] != "YourNewCountry")
            {
                var currentUser = await _db.Users.Include(u => u.Country).FirstOrDefaultAsync(u => u.ChatId == chatId);

                if (currentUser != null)
                {
                    currentUser.Country.Name = words[1];

                    await _db.SaveChangesAsync();

                    try
                    {
                        await client.SendTextMessageAsync(chatId, $"Ваша стана изменена на: {words[1]}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    return new OperationsDetails("Country is changed", true);
                }

                return new OperationsDetails("User is null", false);
            }

            try
            {
                await client.SendTextMessageAsync(chatId, "Пожалуйста, введите вместо YourNewCountry свою новую страну");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new OperationsDetails("Not valid country", false);
        }

        public override bool IsContains(Message message)
        {
            return message.Text.Contains(Name);
        }
    }
}
