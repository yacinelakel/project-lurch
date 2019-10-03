using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Lurch.Karma.Core;
using Lurch.Karma.Data;
using Xunit;

namespace Lurch.Karma.IntegrationTests
{
    public class UserRepositoryIntegrationTests : DatabaseIntegrationFixture
    {
        private UserRepository _userRepository;
        public UserRepositoryIntegrationTests()
        {
            _userRepository = new UserRepository(KarmaContext);
            SeedUsers(KarmaContext);
        }

        private void SeedUsers(KarmaContext karmaContext)
        {
            karmaContext.Users.AddRange(new User(1,1), new User(2), new User(3));
            karmaContext.SaveChanges();
        }

        [Fact]
        public async Task Test1()
        {
            var users = await _userRepository.GetUsers();

            users.Should().HaveCount(3);
        }
    }
}