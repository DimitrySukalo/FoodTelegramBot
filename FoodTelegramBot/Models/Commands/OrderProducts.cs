using FoodTelegramBot.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FoodTelegramBot.Models.Commands
{
    public class OrderProducts : CommandBase
    {
        private readonly TelegramBotContext _db;

        public override string Name => @"/toOrder";

        public OrderProducts(TelegramBotContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), " was null.");
        }

        public override async Task<OperationsDetails> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var user = await _db.Users.Include(u => u.Cart).ThenInclude(c => c.PizzaNames).FirstOrDefaultAsync(u => u.ChatId == chatId);

            if(user != null)
            {
                if (user.Cart.Price > 0)
                {
                    if (string.IsNullOrWhiteSpace(user.PhoneNumber) || string.IsNullOrWhiteSpace(user.Email)
                        || string.IsNullOrWhiteSpace(user.Country.Name) || string.IsNullOrWhiteSpace(user.Country.City))
                    {
                        await client.SendTextMessageAsync(chatId, "Ваша информация не заполнена до конца. Введите /profile , чтобы заполнить свои учётные данные.");
                        return new OperationsDetails("Profile is not filled", false);
                    }
                    else
                    {
                        user.Cart.IsOrdered = true;
                        await client.SendTextMessageAsync(chatId, "Спасибо за заказ! Ожидайте дзвонка!");
                        return new OperationsDetails("Products is ordered", true);
                    }
                }
                else if(user.Cart.Price == 0)
                {
                    await client.SendTextMessageAsync(chatId, "Вы не выбрали ни одного продукта!");

                    return new OperationsDetails("Cart is null", false);
                }
            }

            return new OperationsDetails("User is null", false);
        }

        public override bool IsContains(Message message)
        {
            return message.Text.Contains(Name);
        }
    }
}
