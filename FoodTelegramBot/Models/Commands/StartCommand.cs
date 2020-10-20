using System;
using System.Threading.Tasks;
using FoodTelegramBot.DB;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using User = FoodTelegramBot.DB.Entities.User;

namespace FoodTelegramBot.Models.Commands
{
    public class StartCommand : CommandBase
    {
        private readonly TelegramBotContext _db;
        
        public StartCommand(TelegramBotContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), " was null.");
        }
        
        public override string Name { get; } = @"/start";
        
        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var replyMessageId = message.MessageId;

            var user = new User()
            {
                UserName = message.From.Username,
                FirstName = message.From.FirstName,
                LastName = message.From.LastName,
                ChatId = message.Chat.Id
            };

            var sameUserInDb = await _db.Users.FirstOrDefaultAsync(u => u.ChatId == user.ChatId);

            if (sameUserInDb == null)
            {
                await client.SendTextMessageAsync(chatId, "Привет! С помощью этого бота вы можете заказать еду на дом. " +
                                                          "Чтобы начать нажмите /foods@deliverfood_bot", replyToMessageId: replyMessageId);
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
            }
            else
            {
                await client.SendTextMessageAsync(chatId, "Вы уже зарегестрированы!");
            }
        }

        public override bool IsContains(Message message)
        {
            return message.Type == MessageType.Text && message.Text.Contains(Name);
        }
    }
}