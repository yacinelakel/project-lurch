using System.Threading.Tasks;
using FluentAssertions;
using Lurch.Telegram.Bot.Core.Commands;
using Lurch.Telegram.Bot.Core.Messages;
using Lurch.Telegram.Bot.Core.Services;
using Lurch.Telegram.Bot.UnitTests.Helpers;
using Moq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Xunit;

namespace Lurch.Telegram.Bot.UnitTests.CommandTests
{
    public class EchoCommandExecutorTests
    {
        #region CanExecute(TelegramCommand command)
        [Fact]
        public void CanExecute_returns_false_if_command_is_not_a_command()
        {
            var executor = new EchoCommandExecutor(null, null);

            var command = new TelegramCommand(
                TelegramTextMessage.Create(
                    TelegramMessage.Create(
                        new TelegramUpdate
                        {
                            Message = new Message { Text = "" }
                        })));

            command.IsCommand.Should().BeFalse();

            var result = executor.CanExecute(command);

            result.Should().BeFalse();
        }

        [Fact]
        public void CanExecute_returns_false_if_command_is_null()
        {
            var executor = new EchoCommandExecutor(null, null);

            var result = executor.CanExecute(null);

            result.Should().BeFalse();
        }

        [Fact]
        public void CanExecute_returns_false_if_command_name_is_not_equal()
        {
            var executor = new EchoCommandExecutor(null, null);

            var command = UnitTestHelper.CreateTelegramCommand("/bleh");

            command.IsCommand.Should().BeTrue();
            command.CommandName.Should().Be("/bleh");

            var result = executor.CanExecute(command);

            result.Should().BeFalse();
        }

        [Fact]
        public void CanExecute_returns_false_if_the_rest_after_removing_command_is_empty()
        {
            var executor = new EchoCommandExecutor(null, null);

            var command = UnitTestHelper.CreateTelegramCommand("/echo");

            command.IsCommand.Should().BeTrue();
            command.CommandName.Should().Be("/echo");
            command.Rest.Should().BeEmpty();

            var result = executor.CanExecute(command);

            result.Should().BeFalse();
        }

        [Fact]
        public void CanExecute_returns_false_if_the_rest_after_removing_command_is_only_whitespaces()
        {
            var executor = new EchoCommandExecutor(null, null);

            var command = UnitTestHelper.CreateTelegramCommand("/echo          ");

            command.IsCommand.Should().BeTrue();
            command.CommandName.Should().Be("/echo");
            // Only whitespaces
            command.Rest.Should().BeNullOrWhiteSpace();
            command.Rest.Should().NotBeEmpty();
            command.Rest.Should().NotBeNull();

            var result = executor.CanExecute(command);

            result.Should().BeFalse();
        }


        #endregion

        #region ExecuteCommand(TelegramCommand command)

        [Fact]
        public async Task ExecuteCommand_sends_text_message_to_chat_with_the_rest_of_the_command()
        {
            var fakeBotService = new Mock<ITelegramBotService>();
            var fakeClient = new Mock<ITelegramBotClient>();
            fakeBotService.SetupGet(x => x.Client).Returns(() => fakeClient.Object);

            var command = UnitTestHelper.CreateTelegramCommand("/echo hello world");

            var executor = new EchoCommandExecutor(null, fakeBotService.Object);

            await executor.ExecuteCommand(command);

            fakeClient.VerifySendTextMessageAsync(command.Message.Chat.Id, command.Rest, Times.Once());
        }


        [Fact]
        public async Task ExecuteCommand_does_not_send_text_message_if_command_is_null()
        {
            var fakeBotService = new Mock<ITelegramBotService>();
            var fakeClient = new Mock<ITelegramBotClient>();
            fakeBotService.SetupGet(x => x.Client).Returns(() => fakeClient.Object);


            var executor = new EchoCommandExecutor(null, fakeBotService.Object);

            await executor.ExecuteCommand(null);

            fakeClient.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ExecuteCommand_does_not_send_text_message_if_command_is_not_a_command()
        {
            var fakeBotService = new Mock<ITelegramBotService>();
            var fakeClient = new Mock<ITelegramBotClient>();
            fakeBotService.SetupGet(x => x.Client).Returns(() => fakeClient.Object);

            var command = UnitTestHelper.CreateTelegramCommand("");
            command.IsCommand.Should().BeFalse();

            var executor = new EchoCommandExecutor(null, fakeBotService.Object);

            await executor.ExecuteCommand(command);

            fakeClient.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ExecuteCommand_does_not_send_text_message_if_command_name_is_incorrect()
        {
            var fakeBotService = new Mock<ITelegramBotService>();
            var fakeClient = new Mock<ITelegramBotClient>();
            fakeBotService.SetupGet(x => x.Client).Returns(() => fakeClient.Object);

            var command = UnitTestHelper.CreateTelegramCommand("/bleh");
            command.IsCommand.Should().BeTrue();
            command.CommandName.Should().Be("/bleh");

            var executor = new EchoCommandExecutor(null, fakeBotService.Object);

            await executor.ExecuteCommand(command);

            fakeClient.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ExecuteCommand_does_not_send_text_message_if_command_rest_is_empty()
        {
            var fakeBotService = new Mock<ITelegramBotService>();
            var fakeClient = new Mock<ITelegramBotClient>();
            fakeBotService.SetupGet(x => x.Client).Returns(() => fakeClient.Object);

            var command = UnitTestHelper.CreateTelegramCommand("/echo");
            command.IsCommand.Should().BeTrue();
            command.CommandName.Should().Be("/echo");
            command.Rest.Should().BeEmpty();

            var executor = new EchoCommandExecutor(null, fakeBotService.Object);

            await executor.ExecuteCommand(command);

            fakeClient.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ExecuteCommand_does_not_send_text_message_if_command_rest_is_only_whitespaces()
        {
            var fakeBotService = new Mock<ITelegramBotService>();
            var fakeClient = new Mock<ITelegramBotClient>();
            fakeBotService.SetupGet(x => x.Client).Returns(() => fakeClient.Object);

            var command = UnitTestHelper.CreateTelegramCommand("/echo        ");
            command.IsCommand.Should().BeTrue();
            command.CommandName.Should().Be("/echo");
            // Rest should be whitespaces
            command.Rest.Should().NotBeEmpty();
            command.Rest.Should().NotBeNull();
            command.Rest.Should().BeNullOrWhiteSpace();

            var executor = new EchoCommandExecutor(null, fakeBotService.Object);

            await executor.ExecuteCommand(command);

            fakeClient.VerifyNoOtherCalls();
        }

        #endregion
    }
}