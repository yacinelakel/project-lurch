using System;
using FluentAssertions;
using Lurch.Karma.Core;
using Xunit;

namespace Lurch.Karma.UnitTests.Core
{
    public class UserTests
    {
        [Fact]
        public void Creating_a_new_user_has_0_karma()
        {
            var user = new User(0);

            user.Karma.Amount.Should().Be(0);
        }

        [Fact]
        public void Creating_a_new_user_with_a_karma_amount_sets_karma()
        {
            var user = new User(0, 30);

            user.Karma.Amount.Should().Be(30);
        }
    }
}
