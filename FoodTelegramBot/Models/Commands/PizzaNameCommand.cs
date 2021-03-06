﻿using FoodTelegramBot.DB.Entities.Pizzas;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

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

        public override async Task<OperationsDetails> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var description = GetDescriptionOfPizza();
            var keyboard = new InlineKeyboardMarkup
            (
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Добавить в корзину", $"/addPizza{pizza.Name}")
                    }
                }
            );

            try
            {
                using (var stream = System.IO.File.Open(pizza.PhotoPath, FileMode.Open))
                {
                    await client.SendPhotoAsync(chatId, new InputOnlineFile(stream), description, replyMarkup: keyboard);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new OperationsDetails("Pizza is sended", true);
        }

        private string GetDescriptionOfPizza()
        {
            StringBuilder description = new StringBuilder();
            description.Append($"\U0001F355 Название: {pizza.Name}\n");
            description.Append($"\U0001F4B0 Цена: {pizza.GetCost()} грн\n");
            description.Append($"\U0001F53C Вес: {pizza.Weight} гр\n");

            description.Append("\U0001F963 Ингредиеты:\n");
            foreach (var ingredient in pizza.Ingredients)
            {
                description.Append($"{ingredient.Name}\n");
            }

            return description.ToString();
        }

        public override bool IsContains(Message message)
        {
            return message.Text.Contains(Name);
        }
    }
}
