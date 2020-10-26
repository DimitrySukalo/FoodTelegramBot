using FoodTelegramBot.DB;
using FoodTelegramBot.DB.Entities;
using FoodTelegramBot.DB.Entities.Pizzas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FoodTelegramBot.Models.Commands
{
    public class AddPizzaToCartCommand : CommandBase
    {
        private readonly TelegramBotContext _db;
        public override string Name => @"/addPizza";

        public AddPizzaToCartCommand(TelegramBotContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context), " was null");
        }

        public override async Task<OperationsDetails> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var user = await _db.Users.Include(u => u.Cart).ThenInclude(c => c.PizzaNames).FirstOrDefaultAsync(u => u.ChatId == chatId);

            if(user != null)
            {
                var pizzaName = message.Text.Trim(Name.ToCharArray());
                var pizza = PizzaList.Pizzas.FirstOrDefault(p => p.Name == pizzaName);
                if(pizza != null)
                {
                    user.Cart.PizzaNames.Add(new PizzaName() { Name = pizzaName });
                    user.Cart.Price += pizza.GetCost();

                    await _db.SaveChangesAsync();
                    await client.SendTextMessageAsync(chatId, "Пицца была успешно добавлена у вашу корзину! Чтобы увидеть свою корзину введите /cart или вернитесь в меню и выберите \"Корзина\".");

                    return new OperationsDetails("Pizza have been added", true);
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
