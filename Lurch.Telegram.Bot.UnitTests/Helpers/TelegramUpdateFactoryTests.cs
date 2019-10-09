using System.Collections.Generic;
using FluentAssertions;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Lurch.Telegram.Bot.UnitTests.Helpers
{
    public class TelegramUpdateFactoryTests
    {
        public static IEnumerable<object[]> UpdateTypes() => UnitTestHelper.GetEnumerableValues<UpdateType>();

        [Theory]
        [MemberData(nameof(UpdateTypes))]
        public void CreateFakeTelegramUpdate_should_create_update_with_type(UpdateType type)
        {
            TelegramUpdateFactory.CreateFakeTelegramUpdate(type).Type.Should().Be(type);
        }
    }
}