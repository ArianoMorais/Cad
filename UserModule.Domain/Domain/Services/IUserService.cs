using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Domain.Entities;
using UserModule.Domain.Ports;

namespace UserModule.Domain.Services
{
    public interface IUserService : IService<User>
    {
    }
}
