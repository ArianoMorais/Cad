using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Domain.Domain.Dtos;
using UserModule.Domain.Entities;
using UserModule.Domain.Ports;

namespace UserModule.Domain.Services
{
    public interface IUserService : IService<User>
    {
        Task<LoginDto> AuthenticateAsync(string email, string password);
        Task CreateUserAsync(UserDto userDto);
        Task<UserDto> GetUserDtoByIdAsync(string id);
        Task UpdateUserAsync(UserDto userDto);
    }
}
