using FoodTelegramBot.DB.Entities.Pizzas;
using FoodTelegramBot.Models;
using FoodTelegramBot.Models.Commands;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Xunit;

namespace FoodTelegramBot.Tests.Tests
{
    public class PizzaNameCommandTests
    {
        [Fact]
        private void ContaintMethodReturnsFalse()
        {
            //Arrange
            var pizzaModel = new MargaritaPizza();
            var pizzaNameCommand = new PizzaNameCommand(pizzaModel);
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
            var pizzaModel = new MargaritaPizza();
            var pizzaNameCommand = new PizzaNameCommand(pizzaModel);
            var message = new Message()
            {
                Text = $"/{pizzaModel.Name}"
            };

            //Act
            var result = pizzaNameCommand.IsContains(message);

            //Assert
            Assert.True(result);
        }

        [Fact]
        private async Task ExecuteReturnsSendedPizza()
        {
            //Arrange
            var pizzaModel = new MargaritaPizza();
            var pizzaNameCommand = new PizzaNameCommand(pizzaModel);
            var message = new Message()
            {
                Chat = new Chat()
                {
                    Id = 1
                }
            };

            var client = new TelegramBotClient(AppConfig.Token);

            //Act
            var result = await pizzaNameCommand.Execute(message, client);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Pizza is sended", result.Message);
            Assert.True(result.isSuccessed);
        }
    }
}
