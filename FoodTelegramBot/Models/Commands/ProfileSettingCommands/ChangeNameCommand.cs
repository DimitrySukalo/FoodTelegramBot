using FoodTelegramBot.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FoodTelegramBot.Models.Commands
{
    public class ChangeNameCommand : CommandBase
    {
        private readonly TelegramBotContext _db;

        public ChangeNameCommand(TelegramBotContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), " was null");
        }

        public override string Name => @"/changeName";

        public override async Task<OperationsDetails> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var words = message.Text.Split('@');

            if (words[1] != "YourNewName")
            {
                var currentUser = await _db.Users.FirstOrDefaultAsync(u => u.ChatId == chatId);

                if (currentUser != null)
                {
                    currentUser.FirstName = words[1];

                    await _db.SaveChangesAsync();

                    try
                    {
                        await client.SendTextMessageAsync(chatId, $"Ваше имя изменено на: {words[1]}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    return new OperationsDetails("Name is changed", true);
                }

                return new OperationsDetails("User is null", false);
            }

            try
            {
                await client.SendTextMessageAsync(chatId, "Пожалуйста, введите вместо YourNewName своё имя");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new OperationsDetails("Not valid name", false);
        }

        public override bool IsContains(Message message)
        {
            return message.Text.Contains(Name);
        }
    }
}
