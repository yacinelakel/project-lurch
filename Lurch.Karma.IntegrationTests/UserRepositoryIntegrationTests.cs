using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Lurch.Karma.Core;
using Lurch.Karma.Data;
using Xunit;

namespace Lurch.Karma.IntegrationTests
{
    public class UserRepositoryIntegrationTests : DatabaseIntegrationFixture
    {
        public UserRepositoryIntegrationTests()
        {
            _userRepository = new UserRepository(Transaction);
        }

        private readonly UserRepository _userRepository;

        [Fact]
        public async Task Add_adds_a_new_user()
        {
            var user = User.Create(30);

            var id = await _userRepository.AddUser(user);

            var result = await _userRepository.GetUser(id);
            result.HasValue.Should().BeTrue();

            var createdUser = result.Value;
            createdUser.Id.Should().Be(id);
            createdUser.Karma.Amount.Should().Be(user.Karma.Amount);
        }

        [Fact]
        public async Task GetUsers_gets_all_users()
        {
            var id1 = await _userRepository.AddUser(User.Create(20));
            var id2 = await _userRepository.AddUser(User.Create());
            var id3 = await _userRepository.AddUser(User.Create(-20));

            var users = await _userRepository.GetUsers();

            users.Should().HaveCount(3);
            users.Select(x => x.Id).Should().BeEquivalentTo(id1, id2, id3);
        }

        [Fact]
        public async Task GetUser_returns_a_user_with_id()
        {
            var userToCreate = User.Create(20);
            var id = await _userRepository.AddUser(userToCreate);

            var result = await _userRepository.GetUser(id);
            result.HasValue.Should().BeTrue();

            var user = result.Value;
            user.Id.Should().Be(id);
            user.Karma.Should().Be(user.Karma);
        }

        [Fact]
        public async Task GetUser_returns_none_if_user_is_not_found()
        {
            var result = await _userRepository.GetUser(42);

            result.Should().Be(Maybe<User>.None);
        }

        [Fact]
        public async Task GetUsers_returns_empty_list_if_there_are_no_users()
        {
            var users = await _userRepository.GetUsers();

            users.Should().NotBeNull();
            users.Should().BeEmpty();
        }
    }
}