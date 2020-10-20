using FoodTelegramBot.DB;
using FoodTelegramBot.Models.Commands;
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

            _commands = new List<CommandBase>() { new StartCommand(db) };
            _client = new TelegramBotClient(AppConfig.Token);

            return _client;
        }
    }
}