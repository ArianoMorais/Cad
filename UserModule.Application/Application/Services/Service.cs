using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Domain.Entities;
using UserModule.Domain.Ports;

namespace UserModule.Application.Services
{
    public class Service<T> : ServiceBase<T> where T : BaseEntity, new()
    {
        public Service(IRepository<T> repository)
            : base(repository)
        {
        }

    }
}
