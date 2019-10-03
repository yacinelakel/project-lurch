using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lurch.Karma.Core;
using Microsoft.EntityFrameworkCore;

namespace Lurch.Karma.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly KarmaContext _context;

        public UserRepository(KarmaContext context)
        {
            _context = context;
        }

        public Task<List<User>> GetUsers()
        {
            return _context.Users.ToListAsync();
        }

        public Task<User> GetUser(int id)
        {
            return _context.Users.SingleAsync(x => x.Id == id);
        }

        public Task AddUser(User user)
        {
            return _context.Users.AddAsync(user);
        }
    }

    public interface IUserRepository
    {
        Task<List<User>> GetUsers();

        Task<User> GetUser(int id);
    }
}
