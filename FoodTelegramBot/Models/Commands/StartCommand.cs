﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodTelegramBot.DB;
using FoodTelegramBot.DB.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
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
        
        public override async Task<OperationsDetails> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var replyMessageId = message.MessageId;

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

            var user = new User()
            {
                UserName = message.From.Username,
                FirstName = message.From.FirstName,
                LastName = message.From.LastName,
                ChatId = message.Chat.Id,
                Cart = new Cart()
                {
                    PizzaNames = new List<PizzaName>(),
                    Price = 0.0m,
                    IsOrdered = false
                },
                Country = new Country()
            };

            var sameUserInDb = await _db.Users.FirstOrDefaultAsync(u => u.ChatId == user.ChatId);

            if (sameUserInDb == null)
            {
                try
                {
                    await client.SendTextMessageAsync(chatId, "Привет! С помощью этого бота вы можете заказать еду на дом." +
                                                              " Чтобы начать, выберите еду которую хотите заказать \U0001F354", replyToMessageId: replyMessageId, replyMarkup: keyboardButtons);
                    await _db.Users.AddAsync(user);
                    await _db.SaveChangesAsync();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return new OperationsDetails("User have been added", true);
            }
            else
            {
                try
                {
                    await client.SendTextMessageAsync(chatId, $"Вы уже зарегестрированы!");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return new OperationsDetails("User is registreted", false);
            }
        }

        public override bool IsContains(Message message)
        {
            return message.Text.Contains(Name);
        }
    }
}