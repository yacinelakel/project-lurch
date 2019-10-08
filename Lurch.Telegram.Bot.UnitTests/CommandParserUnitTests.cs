using System;
using System.Linq;
using FluentAssertions;
using Lurch.Telegram.Bot.Core;
using Lurch.Telegram.Bot.Core.Commands;
using Lurch.Telegram.Bot.Core.Messages;
using Telegram.Bot.Types;
using Xunit;

namespace Lurch.Telegram.Bot.UnitTests
{
    public class CommandParserUnitTests
    {
        [Theory]
        [InlineData(@"\echo hello world", @"\echo", " hello world", "hello", "world")]
        [InlineData(@"   \echo hello world", @"\echo", " hello world", "hello", "world")]
        [InlineData(@"\echo hello world.  Have fun, parse code, bleh \echo ", @"\echo", @" hello world.  Have fun, parse code, bleh \echo ", "hello", "world.", "Have", "fun,", "parse","code,", "bleh", @"\echo")]
        public void Should_parse_as_a_command(string input, string expectedCommand, string expectedRest, params string[] expectedArgs)
        {
            var parser = new TelegramCommand(CreateFake(input));

            parser.IsCommand.Should().BeTrue();
            parser.Message.Text.Should().Be(input);
            parser.Command.Should().Be(expectedCommand);
            parser.Rest.Should().Be(expectedRest);
            parser.Args.Should().HaveCount(expectedArgs.Length);
            foreach (var (arg, i) in parser.Args.Select((v,i) => (v,i)))
            {
                arg.Should().Be(expectedArgs[i]);
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData("hello world")]
        [InlineData("hello /command world")]
        public void Should_parse_as_not_a_command(string input)
        {
            var parser = new TelegramCommand(CreateFake(input));
            parser.IsCommand.Should().BeFalse();
            parser.Message.Text.Should().Be(input);
            parser.Args.Should().BeNullOrEmpty();
            parser.Rest.Should().BeNullOrEmpty();
            parser.Command.Should().BeNullOrEmpty();
        }

        private TelegramTextMessage CreateFake(string input)
        {
            var fakeUpdate = new TelegramUpdate
            {
                Message = new Message
                {
                    Text = input
                }
            };

            return TelegramTextMessage.Create(TelegramMessage.Create(fakeUpdate));
        }
    }
}
