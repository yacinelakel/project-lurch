using System;
using System.Collections.Generic;
using FluentAssertions;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Lurch.Telegram.Bot.UnitTests.Helpers
{
    public class TelegramMessageFactoryTests
    {

        public static IEnumerable<object[]> NoObsoleteMessageTypes() =>
            UnitTestHelper.GetEnumerableValues<MessageType>(e => !e.IsObsolete());

        [Theory]
        [MemberData(nameof(NoObsoleteMessageTypes))]
        public void CreateFakeTelegramMessage_should_create_message_with_type(MessageType type)
        {
            var result = TelegramMessageFactory.CreateFakeTelegramMessage(type);

            result.Type.Should().Be(type);
        }
    }
}