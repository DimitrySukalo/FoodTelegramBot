using FoodTelegramBot.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FoodTelegramBot.Models.Commands
{
    public class CartCommand : CommandBase
    {
        private readonly TelegramBotContext _db;

        public CartCommand(TelegramBotContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), " was null.");
        }

        public override string Name => @"/cart";

        public override async Task<OperationsDetails> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var user = await _db.Users.Include(u => u.Cart).ThenInclude(c => c.PizzaNames).FirstOrDefaultAsync(u => u.ChatId == chatId);
            
            if(user != null)
            {
                var pizzas = user.Cart.PizzaNames;
                var priceOfCart = user.Cart.Price;
                var content = new StringBuilder();

                if (pizzas.Count > 0)
                {
                    content.Append("Пиццы:\n");
                    foreach (var piiza in pizzas)
                    {
                        content.Append($"{piiza.Name}\n");
                    }
                    content.Append($"Сумма заказа: {priceOfCart} грн");
                }
                else if(pizzas.Count == 0)
                {
                    content.Append($"Сумма заказа: {priceOfCart} грн");
                }

                var keyboard = new InlineKeyboardMarkup
                (
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Заказать", "/toOrder")
                        }
                    }
                );

                await client.SendTextMessageAsync(chatId, content.ToString(), replyMarkup: keyboard);

                return new OperationsDetails("Cart is sended", true);
            }

            return new OperationsDetails("User is null", false);
        }

        public override bool IsContains(Message message)
        {
            return message.Text.Contains(Name); 
        }
    }
}
