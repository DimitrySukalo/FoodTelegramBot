﻿using FoodTelegramBot.DB;
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
    public class AddPizzaToCartCommandTests
    {
        [Fact]
        private void ContaintMethodReturnsFalse()
        {
            //Arrange
            var dbMock = new Mock<TelegramBotContext>();
            var addPizzaToCartCommand = new AddPizzaToCartCommand(dbMock.Object);
            var message = new Message()
            {
                Text = Guid.NewGuid().ToString()
            };

            //Act
            var result = addPizzaToCartCommand.IsContains(message);

            //Assert
            Assert.False(result);
        }

        [Fact]
        private void ContaintMethodReturnsTrue()
        {
            //Arrange
            var dbMock = new Mock<TelegramBotContext>();
            var addPizzaToCartCommand = new AddPizzaToCartCommand(dbMock.Object);
            var message = new Message()
            {
                Text = "/addPizza"
            };

            //Act
            var result = addPizzaToCartCommand.IsContains(message);

            //Assert
            Assert.True(result);
        }

        [Fact]
        private async Task ExecuteMethodReturnsUserIsNull()
        {
            //Arrange
            var mocks = GetMocks();

            var users = GetUsers().AsQueryable().BuildMock();
            SetSettingsInDb(mocks.userMock, users);

            mocks.dbMock.Setup(m => m.Users).Returns(mocks.userMock.Object);
            var addPizzaToCartCommand = new AddPizzaToCartCommand(mocks.dbMock.Object);
            var message = new Message()
            {
                Chat = new Chat()
                {
                    Id = 2
                }
            };

            var client = new TelegramBotClient(AppConfig.Token);

            //Act
            var result = await addPizzaToCartCommand.Execute(message, client);

            //Assert
            Assert.NotNull(result);
            Assert.False(result?.isSuccessed);
            Assert.Equal("User is null", result?.Message);
        }

        [Fact]
        private async Task ExecuteMethodReturnsAddedPizza()
        {
            //Arrange
            var mocks = GetMocks();

            var users = GetUsers().AsQueryable().BuildMock();
            SetSettingsInDb(mocks.userMock, users);

            mocks.dbMock.Setup(m => m.Users).Returns(mocks.userMock.Object);
            var addPizzaToCartCommand = new AddPizzaToCartCommand(mocks.dbMock.Object);
            var message = new Message()
            {
                Chat = new Chat()
                {
                    Id = 1
                },
                Text = "Маргарита"
            };

            var client = new TelegramBotClient(AppConfig.Token);

            //Act
            var result = await addPizzaToCartCommand.Execute(message, client);

            //Assert
            Assert.NotNull(result);
            Assert.True(result?.isSuccessed);
            Assert.Equal("Pizza have been added", result?.Message);
        }

        [Fact]
        private async Task ExecuteMethodReturnsPizzaIsNull()
        {
            //Arrange
            var mocks = GetMocks();

            var users = GetUsers().AsQueryable().BuildMock();
            SetSettingsInDb(mocks.userMock, users);

            mocks.dbMock.Setup(m => m.Users).Returns(mocks.userMock.Object);
            var addPizzaToCartCommand = new AddPizzaToCartCommand(mocks.dbMock.Object);
            var message = new Message()
            {
                Chat = new Chat()
                {
                    Id = 1
                },
                Text = Guid.NewGuid().ToString()
            };

            var client = new TelegramBotClient(AppConfig.Token);

            //Act
            var result = await addPizzaToCartCommand.Execute(message, client);

            //Assert
            Assert.NotNull(result);
            Assert.False(result?.isSuccessed);
            Assert.Equal("Pizza is null", result?.Message);
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
