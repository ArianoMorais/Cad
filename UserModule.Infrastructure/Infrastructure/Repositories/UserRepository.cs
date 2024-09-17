using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using UserModule.Domain.Domain.Dtos;
using UserModule.Domain.Entities;
using UserModule.Domain.Ports;
using UserModule.Domain.Services;
using UserModule.Infrastructure.Infrastructure.Configuration;

namespace UserModule.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IMongoContext context)
            : base(context, "Users")
        {
        }

        public async Task<User> GetUserByIdAsync(long id)
        {
            return await _collection.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> FindByCpfOrEmailAsync(string cpf, string email)
        {
            var filter = Builders<User>.Filter.Or(
                Builders<User>.Filter.Eq(u => u.Cpf, cpf),
                Builders<User>.Filter.Eq(u => u.Email, email)
            );

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<bool> HasDuplicateAddressAsync(UserDto userDto)
        {
            var existingUser = await GetUserByIdAsync(userDto.Id);

            if (existingUser == null || existingUser.Addresses == null || !existingUser.Addresses.Any())
            {
                return false;
            }

            foreach (var newAddress in userDto.Addresses)
            {
                var isDuplicate = existingUser.Addresses.Any(existingAddress =>
                    existingAddress.Street == newAddress.Street &&
                    existingAddress.City == newAddress.City &&
                    existingAddress.State == newAddress.State &&
                    existingAddress.ZipCode == newAddress.ZipCode);

                if (isDuplicate)
                {
                    return true;
                }
            }

            return false;
        }
        public async Task AddUserAsync(User user)
        {
            await _collection.InsertOneAsync(user);
        }
    }
}
