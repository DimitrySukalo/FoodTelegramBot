using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FoodTelegramBot.Models.Commands
{
    public abstract class CommandBase
    {
        public abstract string Name { get; }

        public abstract Task<OperationsDetails> Execute(Message message, TelegramBotClient client);

        public abstract bool IsContains(Message message);
    }
}