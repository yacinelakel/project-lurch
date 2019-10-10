using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Lurch.Telegram.Bot.Core.Handlers;
using Lurch.Telegram.Bot.Core.Messages;
using Lurch.Telegram.Bot.UnitTests.Helpers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Telegram.Bot.Types.Enums;

namespace Lurch.Telegram.Bot.UnitTests.HandlerTests
{
    public class TelegramMessageHandlerTests
    {
        [Fact]
        public async Task HandleAsync_should_do_nothing_if_message_is_null()
        {
            var fakeTextMessageHandler = new Mock<IHandleTelegramTextMessage>();
            var fakeLogger = new Mock<ILogger<TelegramMessageHandler>>();

            var messageHandler = new TelegramMessageHandler(fakeLogger.Object, fakeTextMessageHandler.Object);

            await messageHandler.HandleAsync(null);

            fakeTextMessageHandler.VerifyNoOtherCalls();
        }


        public static IEnumerable<object[]> DoNothingMessageTypes() => 
            UnitTestHelper.GetEnumerableValues<MessageType>((e) => 
                !e.IsObsolete() && e.As<MessageType>() != MessageType.Text);

        [Theory]
        [MemberData(nameof(DoNothingMessageTypes))]
        public async Task HandleAsync_should_do_nothing_if_type_is(MessageType type)
        {
            var fakeTextMessageHandler = new Mock<IHandleTelegramTextMessage>();
            var fakeLogger = new Mock<ILogger<TelegramMessageHandler>>();

            var message = TelegramMessageFactory.CreateFakeTelegramMessage(type);

            var messageHandler = new TelegramMessageHandler(fakeLogger.Object, fakeTextMessageHandler.Object);

            await messageHandler.HandleAsync(message);

            fakeTextMessageHandler.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task HandleAsync_should_call_correct_handler_when_type_is_Text()
        {
            var fakeTextMessageHandler = new Mock<IHandleTelegramTextMessage>();
            var fakeLogger = new Mock<ILogger<TelegramMessageHandler>>();

            var message = TelegramMessageFactory.CreateFakeTelegramMessage(MessageType.Text);

            var messageHandler = new TelegramMessageHandler(fakeLogger.Object, fakeTextMessageHandler.Object);

            await messageHandler.HandleAsync(message);

            fakeTextMessageHandler.Verify(x => x.HandleAsync(It.IsAny<TelegramTextMessage>()), Times.Once);
        }

      
    }
}