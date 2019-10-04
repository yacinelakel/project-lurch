using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Lurch.Karma.Core.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();

        Task<Maybe<User>> GetUser(int id);

        Task<int> AddUser(User user);
    }
}
