using FoodTelegramBot.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FoodTelegramBot.Models.Commands.ProfileSettingCommands
{
    public class ChangeEmailCommand : CommandBase
    {
        private readonly TelegramBotContext _db;

        public ChangeEmailCommand(TelegramBotContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), " was null");
        }

        public override string Name => @"/changeEmail";

        public override async Task<OperationsDetails> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var words = message.Text.Split('@');
            if(words.Count() != 3)
            {
                try
                {
                    await client.SendTextMessageAsync(chatId, "Неверный формат email-a");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return new OperationsDetails("Not valid email", false);
            }

            var currentUser = await _db.Users.FirstOrDefaultAsync(u => u.ChatId == chatId);

            if (currentUser != null)
            {
                currentUser.Email = $"{words[1]}@{words[2]}";

                await _db.SaveChangesAsync();

                try
                {
                    await client.SendTextMessageAsync(chatId, $"Ваш email изменён на: {words[1]}@{words[2]}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return new OperationsDetails("Email is changed", true);
            }

            return new OperationsDetails("User is null", false);
        }

        public override bool IsContains(Message message)
        {
            return message.Text.Contains(Name);
        }
    }
}
