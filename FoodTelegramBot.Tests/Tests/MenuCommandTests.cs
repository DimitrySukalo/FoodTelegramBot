using FoodTelegramBot.Models;
using FoodTelegramBot.Models.Commands;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Xunit;

namespace FoodTelegramBot.Tests.Tests
{
    public class MenuCommandTests
    {
        [Fact]
        private void ContaintMethodReturnsFalse()
        {
            //Arrange
            var menuCommand = new MenuCommand();
            var message = new Message()
            {
                Text = Guid.NewGuid().ToString()
            };

            //Act
            var result = menuCommand.IsContains(message);

            //Assert
            Assert.False(result);
        }

        [Fact]
        private void ContaintMethodReturnsTrue()
        {
            //Arrange
            var menuCommand = new MenuCommand();
            var message = new Message()
            {
                Text = $"/menu"
            };

            //Act
            var result = menuCommand.IsContains(message);

            //Assert
            Assert.True(result);
        }

        [Fact]
        private async Task ExecuteMethodReturnsMenuIsSended()
        {
            //Arrange
            var menuCommand = new MenuCommand();
            var message = new Message()
            {
                Chat = new Chat()
                {
                    Id = 1
                }
            };

            var client = new TelegramBotClient(AppConfig.Token);

            //Act
            var result = await menuCommand.Execute(message, client);

            //Assert
            Assert.NotNull(result);
            Assert.True(result?.isSuccessed);
            Assert.Equal("Menu is sended", result?.Message);
        }
    }
}
