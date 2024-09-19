using MongoDB.Driver;
using UserModule.Domain.Entities;
using UserModule.Domain.Ports;
using UserModule.Infrastructure.Infrastructure.Configuration;

namespace UserModule.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IMongoContext context)
            : base(context, "Users")
        {
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _collection.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _collection.Find(user => user.Email == email).FirstOrDefaultAsync();
        }
        public async Task<User> FindByCpfOrEmailAsync(string cpf, string email)
        {
            var filter = Builders<User>.Filter.Or(
                Builders<User>.Filter.Eq(u => u.Cpf, cpf),
                Builders<User>.Filter.Eq(u => u.Email, email)
            );

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task AddUserAsync(User user)
        {
            await _collection.InsertOneAsync(user);
        }
    }
}
