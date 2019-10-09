using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Lurch.Telegram.Bot.Core.Handlers;
using Lurch.Telegram.Bot.Core.Messages;
using Lurch.Telegram.Bot.Core.Services;
using Lurch.Telegram.Bot.UnitTests.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Lurch.Telegram.Bot.UnitTests.HandlerTests
{
    public class TelegramUpdateHandlerTests
    {
        public static IEnumerable<object[]> DoNothingUpdateTypes() =>
            UnitTestHelper.GetEnumerableValues<UpdateType>((e) =>
                !e.IsObsolete() && e.As<UpdateType>() != UpdateType.Message);

        [Theory]
        [MemberData(nameof(DoNothingUpdateTypes))]
        public async Task HandleAsync_should_do_nothing_if_type_is(UpdateType type)
        {
            var fakeLogger = new Mock<ILogger<TelegramUpdateHandler>>();
            var fakeMessageHandler = new Mock<IHandleTelegramMessage>();
            var fakeOptions = new Mock<IOptions<TelegramBotConfiguration>>();
            var fakeBotService = new Mock<ITelegramBotService>();

            var handler = new TelegramUpdateHandler(
                fakeLogger.Object,
                fakeMessageHandler.Object,
                fakeBotService.Object,
                fakeOptions.Object);

            await handler.HandleAsync(TelegramUpdateFactory.CreateFakeTelegramUpdate(type));

            fakeBotService.VerifyNoOtherCalls();
            fakeMessageHandler.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task HandleAsync_should_do_nothing_if_update_is_null()
        {
            var fakeLogger = new Mock<ILogger<TelegramUpdateHandler>>();
            var fakeMessageHandler = new Mock<IHandleTelegramMessage>();
            var fakeOptions = new Mock<IOptions<TelegramBotConfiguration>>();
            var fakeBotService = new Mock<ITelegramBotService>();

            var handler = new TelegramUpdateHandler(
                fakeLogger.Object,
                fakeMessageHandler.Object,
                fakeBotService.Object,
                fakeOptions.Object);

            await handler.HandleAsync(null);

            fakeBotService.VerifyNoOtherCalls();
            fakeMessageHandler.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task HandleAsync_calls_message_handler_when_update_type_is_Message()
        {
            var fakeLogger = new Mock<ILogger<TelegramUpdateHandler>>();
            var fakeMessageHandler = new Mock<IHandleTelegramMessage>();
            var fakeOptions = new Mock<IOptions<TelegramBotConfiguration>>();
            var fakeBotService = new Mock<ITelegramBotService>();

            var handler = new TelegramUpdateHandler(
                fakeLogger.Object,
                fakeMessageHandler.Object,
                fakeBotService.Object,
                fakeOptions.Object);

            await handler.HandleAsync(TelegramUpdateFactory.CreateFakeTelegramUpdate(UpdateType.Message));

            fakeMessageHandler.Verify(x => x.HandleAsync(It.IsAny<TelegramMessage>()), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_catches_and_reports_exception_when_message_handler_throws_and_exception_forwarding_is_enabled()
        {
            var fakeLogger = new Mock<ILogger<TelegramUpdateHandler>>();
            var fakeMessageHandler = new Mock<IHandleTelegramMessage>();
            var fakeOptions = new Mock<IOptions<TelegramBotConfiguration>>();
            var fakeBotService = new Mock<ITelegramBotService>();

            var exception = new Exception("whoops");
            fakeMessageHandler.Setup(x => x.HandleAsync(It.IsAny<TelegramMessage>()))
                .ThrowsAsync(exception);

            var fakeClient = new Mock<ITelegramBotClient>();
            fakeBotService.SetupGet(x => x.Client).Returns(fakeClient.Object);

            var fakeConfig = new TelegramBotConfiguration
            {
                EnableExceptionForwarding = true,
                ExceptionChatId = 42
            };
            fakeOptions.SetupGet(x => x.Value).Returns(fakeConfig);

            var handler = new TelegramUpdateHandler(
                fakeLogger.Object,
                fakeMessageHandler.Object,
                fakeBotService.Object,
                fakeOptions.Object);

            await handler.HandleAsync(TelegramUpdateFactory.CreateFakeTelegramUpdate(UpdateType.Message));

            var expectedMessage = $"```\n{exception}\n```";

            fakeClient.VerifySendTextMessageAsync(fakeConfig.ExceptionChatId, expectedMessage, Times.Once(), ParseMode.Markdown);
        }

        [Fact]
        public async Task HandleAsync_does_not_report_or_catch_exception_if_message_handler_fails()
        {
            var fakeLogger = new Mock<ILogger<TelegramUpdateHandler>>();
            var fakeMessageHandler = new Mock<IHandleTelegramMessage>();
            var fakeOptions = new Mock<IOptions<TelegramBotConfiguration>>();
            var fakeBotService = new Mock<ITelegramBotService>();

            var exception = new Exception("whoops");
            fakeMessageHandler.Setup(x => x.HandleAsync(It.IsAny<TelegramMessage>()))
                .ThrowsAsync(exception);

            var fakeClient = new Mock<ITelegramBotClient>();
            fakeBotService.SetupGet(x => x.Client).Returns(fakeClient.Object);

            var fakeConfig = new TelegramBotConfiguration
            {
                EnableExceptionForwarding = false,
                ExceptionChatId = 42
            };
            fakeOptions.SetupGet(x => x.Value).Returns(fakeConfig);

            var handler = new TelegramUpdateHandler(
                fakeLogger.Object,
                fakeMessageHandler.Object,
                fakeBotService.Object,
                fakeOptions.Object);

            
            Func<Task> action =  async () => await handler.HandleAsync(TelegramUpdateFactory.CreateFakeTelegramUpdate(UpdateType.Message));

            await action.Should().ThrowAsync<Exception>();

            fakeClient.VerifyNoOtherCalls();
        }
    }
}
