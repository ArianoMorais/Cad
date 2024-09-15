using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using UserModule.Domain.Entities;
using UserModule.Domain.Ports;
using UserModule.Infrastructure.Repositories.UserModule.Infrastructure.Repositories;

namespace UserModule.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IMongoContext context)
            : base(context, "User")
        {
        }

        public async Task<User> GetUserByIdAsync(long id)
        {
            return await _collection.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _collection.InsertOneAsync(user);
        }
    }
}
