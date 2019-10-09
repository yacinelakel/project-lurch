using System.Linq;
using FluentAssertions;
using Lurch.Telegram.Bot.Core.Commands;
using Lurch.Telegram.Bot.UnitTests.Helpers;
using Xunit;

namespace Lurch.Telegram.Bot.UnitTests.CommandTests
{
    public class TelegramCommandTests
    {
        [Theory]
        [InlineData("/echo hello world", "/echo", " hello world", "hello", "world")]
        [InlineData("   /echo hello world", "/echo", " hello world", "hello", "world")]
        [InlineData("/echo hello world.  Have fun, parse code, bleh /echo ", "/echo",
            " hello world.  Have fun, parse code, bleh /echo ", "hello", "world.", "Have", "fun,", "parse", "code,",
            "bleh", "/echo")]
        public void Should_create_command_from_a_valid_command(string input, string expectedCommand,
            string expectedRest, params string[] expectedArgs)
        {
            var textMessage = UnitTestHelper.CreateTelegramTextMessage(input);
            var command = new TelegramCommand(textMessage);

            command.IsCommand.Should().BeTrue();
            command.Message.Text.Should().Be(textMessage.Text);
            command.CommandName.Should().Be(expectedCommand);
            command.Rest.Should().Be(expectedRest);
            command.Args.Should().HaveCount(expectedArgs.Length);
            foreach (var (arg, i) in command.Args.Select((v, i) => (v, i))) arg.Should().Be(expectedArgs[i]);
        }

        [Theory]
        [InlineData("")]
        [InlineData("hello world")]
        [InlineData("hello /command world")]
        public void Should_create_command_from_an_invalid_command(string input)
        {
            var textMessage = UnitTestHelper.CreateTelegramTextMessage(input);
            var command = new TelegramCommand(textMessage);

            command.IsCommand.Should().BeFalse();
            command.Message.Text.Should().Be(input);
            command.Args.Should().BeNullOrEmpty();
            command.Rest.Should().BeNullOrEmpty();
            command.CommandName.Should().BeNullOrEmpty();
        }
    }
}