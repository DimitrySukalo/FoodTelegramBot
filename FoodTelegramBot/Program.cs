using FoodTelegramBot.Controllers;
using FoodTelegramBot.DB;
using FoodTelegramBot.Models;
using System;

namespace FoodTelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            BotController botController = new BotController(Bot.GetClient(new TelegramBotContext()));
            botController.StartBot();


            Console.WriteLine("Bot server is started...");
            Console.ReadLine();
        }
    }
}
