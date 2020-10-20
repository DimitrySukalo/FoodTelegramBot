using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FoodTelegramBot.Models.Commands
{
    public class PizzaMenuCommand : CommandBase
    {
        public override string Name => @"/pizzas";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            
        }

        public override bool IsContains(Message message)
        {
            return message.Type == MessageType.Text && message.Text.Contains(Name);
        }
    }
}
