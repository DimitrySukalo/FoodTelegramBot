﻿using FoodTelegramBot.DB;
using FoodTelegramBot.DB.Entities.Pizzas;
using FoodTelegramBot.Models.Commands;
using FoodTelegramBot.Models.Commands.ProfileSettingCommands;
using System.Collections.Generic;
using Telegram.Bot;

namespace FoodTelegramBot.Models
{
    public static class Bot
    {
        private static TelegramBotClient _client;
        private static List<CommandBase> _commands;

        public static IReadOnlyList<CommandBase> Commands => _commands.AsReadOnly();

        public static TelegramBotClient GetClient(TelegramBotContext db)
        {
            if (_client != null)
                return _client;

            _commands = new List<CommandBase>()
            {
                new StartCommand(db),
                new MenuCommand(),
                new PizzaMenuCommand(),
                new PizzaNameCommand(new MargaritaPizza()),
                new PizzaNameCommand(new PekingChicken()),
                new AddPizzaToCartCommand(db),
                new CartCommand(db),
                new OrderProducts(db),
                new ProfileCommand(db),
                new ChangeNameCommand(db),
                new ChangeLastNameCommand(db),
                new ChangeUserNameCommand(db),
                new ChangeCountryNameCommand(db),
                new ChangeCityCommand(db),
                new ChangePhoneNumberCommand(db),
                new ChangeEmailCommand(db)
            };
            _client = new TelegramBotClient(AppConfig.Token);

            return _client;
        }
    }
}