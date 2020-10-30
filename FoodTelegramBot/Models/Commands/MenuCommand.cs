using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FoodTelegramBot.Models.Commands
{
    public class MenuCommand : CommandBase
    {
        public override string Name => @"/menu";

        public override async Task<OperationsDetails> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            var keyboardButtons = new InlineKeyboardMarkup
            (
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Пицца \U0001F355", "/pizzas")
                    }
                }
            );

            try
            {
                await client.SendTextMessageAsync(chatId, "Меню: ", replyMarkup: keyboardButtons);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new OperationsDetails("Menu is sended", true);
        }

        public override bool IsContains(Message message)
        {
            return message.Text.Contains(Name);
        }
    }
}
