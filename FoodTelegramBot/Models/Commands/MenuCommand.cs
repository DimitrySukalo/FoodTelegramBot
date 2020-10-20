using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FoodTelegramBot.Models.Commands
{
    public class MenuCommand : CommandBase
    {
        public override string Name => @"/menu";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            var keyboardButtons = new InlineKeyboardMarkup
            (
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Пицца \U0001F355", "/pizzas"),
                        InlineKeyboardButton.WithCallbackData("Суши \U0001F363", "/sushies")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Напитки \U0001F378", "/drinks"),
                        InlineKeyboardButton.WithCallbackData("Салаты \U0001F340", "/salads")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Корзина \U0001F4B0", "/cart")
                    }
                }
            );

            await client.SendTextMessageAsync(chatId, "Меню: ",replyMarkup: keyboardButtons);
        }

        public override bool IsContains(Message message)
        {
            return message.Type == MessageType.Text && message.Text.Contains(Name);
        }
    }
}
