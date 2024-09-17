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
    public interface IChangeLogService : IService<ChangeLog>
    {
        Task LogChangesAsync<T>(T oldEntity, T newEntity, long? userId);
    }
}
