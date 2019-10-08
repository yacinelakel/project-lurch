using FluentAssertions;
using Lurch.Karma.Core;
using Xunit;

namespace Lurch.Karma.UnitTests.Core
{
    public class KarmaTests
    {
        [Fact]
        public void Parameterless_constructor_sets_initial_amount()
        {
            var karma = new Karma.Core.Karma();
            karma.Amount.Should().Be(0);
        }

        [Fact]
        public void Can_create_karma_with_an_amount()
        {
            var karma = new Karma.Core.Karma(10);
            karma.Amount.Should().Be(10);
        }

        [Theory]
        [InlineData(10, 20, 30)]
        [InlineData(-5, -10, -15)]
        [InlineData(-10, 20, 10)]
        public void Can_add_two_karma_with_operator(int leftAmount, int rightAmount, int expectedSum)
        {
            var leftKarma = new Karma.Core.Karma(leftAmount);
            var rightKarma = new Karma.Core.Karma(rightAmount);

            (leftKarma + rightKarma).Amount.Should().Be(expectedSum);
        }

        [Theory]
        [InlineData(10, 20, 30)]
        [InlineData(-5, -10, -15)]
        [InlineData(-10, 20, 10)]
        public void Can_add_a_karma_and_an_int_with_operator(int leftAmount, int rightAmount, int expectedSum)
        {
            var leftKarma = new Karma.Core.Karma(leftAmount);
            var rightKarma = new Karma.Core.Karma(rightAmount);

            (leftKarma + rightAmount).Amount.Should().Be(expectedSum);
            (leftAmount + rightKarma).Amount.Should().Be(expectedSum);
        }

        [Fact]
        public void Two_karma_are_equal_if_amount_is_equal()
        {
            var karma1 = new Karma.Core.Karma(10);
            var karma2 = new Karma.Core.Karma(10);

            (karma1 == karma2).Should().BeTrue();
            karma1.Equals(karma2).Should().BeTrue();
            karma1.GetHashCode().Should().Be(karma2.GetHashCode());
        }

        [Fact]
        public void Two_karma_are_not_equal_if_amount_is_not_equal()
        {
            var karma1 = new Karma.Core.Karma(30);
            var karma2 = new Karma.Core.Karma(10);

            (karma1 == karma2).Should().BeFalse();
            karma1.Equals(karma2).Should().BeFalse();
            karma1.GetHashCode().Should().NotBe(karma2.GetHashCode());
        }
    }
}
