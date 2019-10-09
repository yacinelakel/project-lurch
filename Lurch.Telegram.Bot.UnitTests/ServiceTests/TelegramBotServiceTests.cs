using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Lurch.Telegram.Bot.Core.Services;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Lurch.Telegram.Bot.UnitTests.ServiceTests
{
    public class TelegramBotServiceTests
    {
        [Fact]
        public void Constructor_should_throw_if_options_is_null()
        {
            Func<TelegramBotService> act = () => new TelegramBotService(null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_should_throw_if_options_value_is_null()
        {
            Func<TelegramBotService> act = () => new TelegramBotService(new Mock<IOptions<TelegramBotConfiguration>>().Object);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_should_create_client()
        {
            var fakeOptions = new Mock<IOptions<TelegramBotConfiguration>>();
            var fakeConfig = new TelegramBotConfiguration
            {
                BotToken = "1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy"
            };

            fakeOptions.SetupGet(x => x.Value).Returns(fakeConfig);


            var service = new TelegramBotService(fakeOptions.Object);

            service.Client.Should().NotBeNull();
        }
    }
}
