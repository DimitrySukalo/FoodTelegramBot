﻿using FoodTelegramBot.DB.Entities.Pizzas;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FoodTelegramBot.Models.Commands
{
    public class PizzaMenuCommand : CommandBase
    {
        public override string Name => @"/pizzas";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();

            var listOfPizzas = PizzaList.Pizzas;
            foreach(var pizza in listOfPizzas)
            {
                var button = new InlineKeyboardButton();
                button.CallbackData = $"/{pizza.Name}";
                button.Text = pizza.Name;
                buttons.Add(button);
            }

            var keyboard = new InlineKeyboardMarkup(buttons);

            await client.SendTextMessageAsync(chatId, "Выберите пиццу: ", replyMarkup: keyboard);
        }

        public override bool IsContains(Message message)
        {
            return message.Text.Contains(Name);
        }
    }
}
