using FoodTelegramBot.DB.Entities.Pizzas;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace FoodTelegramBot.Models.Commands
{
    public class PizzaNameCommand : CommandBase
    {
        private readonly PizzaBase pizza;

        public PizzaNameCommand(PizzaBase pizza)
        {
            this.pizza = pizza ?? throw new ArgumentNullException(nameof(pizza), " was null.");
        }

        public override string Name => $"/{pizza.Name}";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var description = GetDescriptionOfPizza();

            using (var stream = System.IO.File.Open(pizza.PhotoPath, FileMode.Open))
            {
                await client.SendPhotoAsync(chatId, new InputOnlineFile(stream), description);
            }
        }

        private string GetDescriptionOfPizza()
        {
            StringBuilder description = new StringBuilder();
            description.Append($"Название: {pizza.Name}\n");
            description.Append($"Цена: {pizza.GetCost()}\n");
            description.Append("Ингредиеты:\n");
            foreach (var ingredient in pizza.Ingredients)
            {
                description.Append($"\t{ingredient.Name}\n");
            }

            return description.ToString();
        }

        public override bool IsContains(Message message)
        {
            return message.Text.Contains(Name);
        }
    }
}
