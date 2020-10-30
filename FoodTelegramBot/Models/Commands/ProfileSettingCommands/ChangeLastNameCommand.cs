using FoodTelegramBot.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FoodTelegramBot.Models.Commands
{
    public class ChangeLastNameCommand : CommandBase
    {
        private readonly TelegramBotContext _db;

        public ChangeLastNameCommand(TelegramBotContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), " was null");
        }

        public override string Name => @"/changeLastName";

        public override async Task<OperationsDetails> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var words = message.Text.Split('@');

            if (words[1] != "YourNewLastName")
            {
                var currentUser = await _db.Users.FirstOrDefaultAsync(u => u.ChatId == chatId);

                if (currentUser != null)
                {
                    currentUser.LastName = words[1];

                    await _db.SaveChangesAsync();

                    try
                    {
                        await client.SendTextMessageAsync(chatId, $"Ваша фамилия изменена на: {words[1]}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    return new OperationsDetails("Lastname is changed", true);
                }

                return new OperationsDetails("User is null", false);
            }

            try
            {
                await client.SendTextMessageAsync(chatId, "Пожалуйста, введите вместо YourNewLastName свою фамилию");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new OperationsDetails("Not valid lastname", false);
        }

        public override bool IsContains(Message message)
        {
            return message.Text.Contains(Name);
        }
    }
}
