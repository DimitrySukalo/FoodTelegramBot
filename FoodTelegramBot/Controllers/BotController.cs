using FoodTelegramBot.Models;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace FoodTelegramBot.Controllers
{
    public class BotController
    {
        private readonly TelegramBotClient _client;

        public BotController(TelegramBotClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client), " was null.");
        }

        public void StartBot()
        {
            _client.StartReceiving();
            _client.OnMessage += ClientOnMessage;
        }

        private void ClientOnMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            var commands = Bot.Commands;

            foreach (var command in commands)
            {
                if (command.IsContains(message))
                {
                    command.Execute(message, _client);
                    break;
                }
            }
        }
    }
}