using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Domain.Entities;

namespace UserModule.Domain.Ports
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByIdAsync(long id);
        Task AddUserAsync(User user);
    }
}
