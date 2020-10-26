using FoodTelegramBot.Models;
using FoodTelegramBot.Models.Commands;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Xunit;

namespace FoodTelegramBot.Tests.Tests
{
    public class PizzaMenuCommandTests
    {
        [Fact]
        private void ContaintMethodReturnsFalse()
        {
            //Arrange
            var pizzaNameCommand = new PizzaMenuCommand();
            var message = new Message()
            {
                Text = Guid.NewGuid().ToString()
            };

            //Act
            var result = pizzaNameCommand.IsContains(message);

            //Assert
            Assert.False(result);
        }

        [Fact]
        private void ContaintMethodReturnsTrue()
        {
            //Arrange
            var pizzaNameCommand = new PizzaMenuCommand();
            var message = new Message()
            {
                Text = $"/pizzas"
            };

            //Act
            var result = pizzaNameCommand.IsContains(message);

            //Assert
            Assert.True(result);
        }

        [Fact]
        private async Task ExecuteMethodReturnsSendedMenu()
        {
            //Arrange
            var client = new TelegramBotClient(AppConfig.Token);
            var message = new Message()
            {
                Chat = new Chat()
                {
                    Id = 1
                }
            };

            var pizzaNameCommand = new PizzaMenuCommand();

            //Act
            var result = await pizzaNameCommand.Execute(message, client);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.isSuccessed);
            Assert.Equal("Menu is sended", result.Message);
        }
    }
}
