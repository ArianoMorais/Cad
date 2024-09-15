using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Domain.Entities;
using UserModule.Domain.Ports;

namespace UserModule.Infrastructure.Repositories
{
    namespace UserModule.Infrastructure.Repositories
    {
        public class Repository<T> : RepositoryBase<T>, IRepository<T>
            where T : BaseEntity, new()
        {
            public Repository(IMongoContext context, string collectionName)
                : base(context, collectionName)
            {
            }

            // Implementações específicas do repositório, se houver
        }
    }
}
