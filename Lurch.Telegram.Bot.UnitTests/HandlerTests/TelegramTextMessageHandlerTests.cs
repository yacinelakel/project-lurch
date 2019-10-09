using System.Threading.Tasks;
using Lurch.Telegram.Bot.Core.Commands;
using Lurch.Telegram.Bot.Core.Handlers;
using Lurch.Telegram.Bot.UnitTests.Helpers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Lurch.Telegram.Bot.UnitTests.HandlerTests
{
    public class TelegramTextMessageHandlerTests
    {
        [Fact]
        public async Task HandleAsync_should_do_nothing_if_message_is_not_a_command()
        {
            var fakeLogger = new Mock<ILogger<TelegramTextMessageHandler>>();
            var fakeCommandExecutor = new Mock<IExecuteTelegramCommand>();

            var textMessage = UnitTestHelper.CreateTelegramTextMessage("bleg");

            var handler = new TelegramTextMessageHandler(fakeLogger.Object, new[] {fakeCommandExecutor.Object});

            await handler.HandleAsync(textMessage);

            fakeCommandExecutor.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task HandleAsync_should_do_nothing_if_text_message_is_null()
        {
            var fakeLogger = new Mock<ILogger<TelegramTextMessageHandler>>();
            var fakeCommandExecutor = new Mock<IExecuteTelegramCommand>();

            var handler = new TelegramTextMessageHandler(fakeLogger.Object, new[] {fakeCommandExecutor.Object});

            await handler.HandleAsync(null);

            fakeCommandExecutor.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task HandleAsync_should_only_call_ExecuteCommand_on_commandExecutors_that_can_execute()
        {
            var fakeLogger = new Mock<ILogger<TelegramTextMessageHandler>>();
            var fakeCommandExecutor1 = new Mock<IExecuteTelegramCommand>();
            var fakeCommandExecutor2 = new Mock<IExecuteTelegramCommand>();
            var fakeCommandExecutor3 = new Mock<IExecuteTelegramCommand>();

            fakeCommandExecutor1.Setup(x => x.CanExecute(It.IsAny<TelegramCommand>())).Returns(true);
            fakeCommandExecutor2.Setup(x => x.CanExecute(It.IsAny<TelegramCommand>())).Returns(true);
            fakeCommandExecutor3.Setup(x => x.CanExecute(It.IsAny<TelegramCommand>())).Returns(false);

            var textMessage = UnitTestHelper.CreateTelegramTextMessage("/test");

            var handler = new TelegramTextMessageHandler(fakeLogger.Object, new[]
            {
                fakeCommandExecutor1.Object, fakeCommandExecutor2.Object, fakeCommandExecutor3.Object
            });

            await handler.HandleAsync(textMessage);

            fakeCommandExecutor1.Verify(x => x.ExecuteCommand(It.IsAny<TelegramCommand>()), Times.Once);
            fakeCommandExecutor2.Verify(x => x.ExecuteCommand(It.IsAny<TelegramCommand>()), Times.Once);
            fakeCommandExecutor3.Verify(x => x.ExecuteCommand(It.IsAny<TelegramCommand>()), Times.Never);
        }
    }
}