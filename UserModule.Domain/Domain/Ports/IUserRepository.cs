using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Domain.Domain.Dtos;
using UserModule.Domain.Entities;

namespace UserModule.Domain.Ports
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByIdAsync(string id);
        Task AddUserAsync(User user);
        Task<User> FindByCpfOrEmailAsync(string cpf, string email);
        Task<User> GetUserByEmailAsync(string email);
    }
}
