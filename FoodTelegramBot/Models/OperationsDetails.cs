using System;

namespace FoodTelegramBot.Models
{
    public class OperationsDetails
    {
        public string Message { get; }

        public bool isSuccessed { get; }

        public OperationsDetails(string message, bool successed)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message), " was null.");
            isSuccessed = successed;
        }
    }
}
