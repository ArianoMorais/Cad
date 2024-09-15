using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Domain.Entities;
using UserModule.Domain.Ports;
using UserModule.Domain.Services;

namespace UserModule.Application.Services
{
    public class UserService : Service<User>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {
        }
    }
}
