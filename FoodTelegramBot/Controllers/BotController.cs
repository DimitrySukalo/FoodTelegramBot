using FoodTelegramBot.Models;
using FoodTelegramBot.Models.Commands;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

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
            _client.OnCallbackQuery += ClientOnCallBack;
        }

        private async void ClientOnCallBack(object sender, CallbackQueryEventArgs e)
        {
            var commands = Bot.Commands;
            var message = new Message();
            
            message.Text = e.CallbackQuery.Data.ToString();

            foreach(var command in commands)
            {
                if(command.IsContains(message))
                {
                    await command.Execute(e.CallbackQuery.Message, _client);
                }
            }
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