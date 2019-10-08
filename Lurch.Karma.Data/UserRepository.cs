using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Dapper;
using Lurch.Karma.Core;
using Lurch.Karma.Core.Repositories;

namespace Lurch.Karma.Data
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<List<User>> GetUsers()
        {
            var sql = "select * from Users";
            var result = await Connection.QueryAsync<UserDo>(sql, transaction: Transaction);
            return result.Select(UserDo.ToModel).ToList();
        }

        public async Task<Maybe<User>> GetUser(int id)
        {
            var sql = "select * from Users";
            var result = await Connection.QueryFirstOrDefaultAsync<UserDo>(sql, transaction: Transaction);
            return result == null ? Maybe<User>.None : Maybe<User>.From(UserDo.ToModel(result));
        }

        public async Task<int> AddUser(User user)
        {
            var userDo = UserDo.ToDo(user);
            var sql = "insert into Users(KarmaAmount) output INSERTED.ID values(@KarmaAmount)";

            var id = await Connection.QuerySingleAsync<int>(sql, userDo, Transaction);

            return id;
        }
    }
}