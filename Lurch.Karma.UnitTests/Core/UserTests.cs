using System;
using FluentAssertions;
using Lurch.Karma.Core;
using Xunit;

namespace Lurch.Karma.UnitTests.Core
{
    public class UserTests
    {
        [Fact]
        public void Creating_a_new_user_with_a_karma_amount_sets_karma()
        {
            var user = User.Create( 30);

            user.Karma.Amount.Should().Be(30);
        }

        [Fact]
        public void Creating_a_new_user_with_no_karma_amount_sets_karma_to_zero()
        {
            var user = User.Create();

            user.Karma.Amount.Should().Be(0);
        }
    }
}
