using FoodTelegramBot.DB;
using FoodTelegramBot.DB.Entities;
using FoodTelegramBot.Models;
using FoodTelegramBot.Models.Commands;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Xunit;
using User = FoodTelegramBot.DB.Entities.User;

namespace FoodTelegramBot.Tests.Tests
{
    public class StartCommandTests
    {
        [Fact]
        private void ContaintMethodReturnsFalse()
        {
            //Arrange
            var dbMock = new Mock<TelegramBotContext>();
            var startCommand = new StartCommand(dbMock.Object);
            var message = new Message()
            {
                Text = Guid.NewGuid().ToString()
            };

            //Act
            var result = startCommand.IsContains(message);

            //Assert
            Assert.False(result);
        }

        [Fact]
        private void ContaintMethodReturnsTrue()
        {
            //Arrange
            var dbMock = new Mock<TelegramBotContext>();
            var startCommand = new StartCommand(dbMock.Object);
            var message = new Message()
            {
                Text = "/start"
            };

            //Act
            var result = startCommand.IsContains(message);

            //Assert
            Assert.True(result);
        }

        [Fact]
        private async Task ExecuteReturnsAlreadyRegisterMessage()
        {
            //Arrange
            var mocks = GetMocks();

            var users = GetUsers().AsQueryable().BuildMock();
            SetSettingsInDb(mocks.userMock, users);

            mocks.dbMock.Setup(m => m.Users).Returns(mocks.userMock.Object);
            var startCommand = new StartCommand(mocks.dbMock.Object);
            var message = new Message()
            {
                Chat = new Chat()
                {
                    Id = 1
                },
                From = new Telegram.Bot.Types.User()
                {
                    Username = "Test",
                    FirstName = "Test",
                    LastName = "Test"
                }
            };

            var client = new TelegramBotClient(AppConfig.Token);

            //Act
            var result = await startCommand.Execute(message, client);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.isSuccessed);
            Assert.Equal("User is registreted", result.Message);
        }

        [Fact]
        private async Task ExecuteReturnsNewUserIsAdded()
        {
            //Arrange
            var mocks = GetMocks();

            var users = GetUsers().AsQueryable().BuildMock();
            SetSettingsInDb(mocks.userMock, users);

            mocks.dbMock.Setup(m => m.Users).Returns(mocks.userMock.Object);
            var startCommand = new StartCommand(mocks.dbMock.Object);
            var message = new Message()
            {
                Chat = new Chat()
                {
                    Id = 2
                },
                From = new Telegram.Bot.Types.User()
                {
                    Username = "Test",
                    FirstName = "Test",
                    LastName = "Test"
                }
            };

            var client = new TelegramBotClient(AppConfig.Token);

            //Act
            var result = await startCommand.Execute(message, client);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.isSuccessed);
            Assert.Equal("User have been added", result.Message);
        }

        private (Mock<TelegramBotContext> dbMock, Mock<DbSet<User>> userMock, Mock<DbSet<Country>> countryMock) GetMocks()
        {
            var dbMock = new Mock<TelegramBotContext>();
            var userMock = new Mock<DbSet<User>>();
            var countryMock = new Mock<DbSet<Country>>();

            return (dbMock, userMock, countryMock);
        }

        private List<User> GetUsers()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Cart = new Cart()
                    {
                        IsOrdered = false,
                        PizzaNames = new List<PizzaName>(),
                        Price = 0.0m,
                        Id = 1
                    },
                    Country = new Country()
                    {
                        Id = 1,
                        Name = "Ukraine",
                        City = "Kyiv"
                    },
                    ChatId = 1,
                    CountryId = 1,
                    Email = "test@gmail.com",
                    FirstName = "Test",
                    LastName = "Test",
                    PhoneNumber = "1111",
                    UserName = "Test"
                }
            };
        }

        private static void SetSettingsInDb<T>(Mock<DbSet<T>> mock, Mock<IQueryable<T>> items) where T : class
        {
            mock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(items.Object.Provider);
            mock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(items.Object.Expression);
            mock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(items.Object.ElementType);
            mock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(items.Object.GetEnumerator());
        }
    }
}
