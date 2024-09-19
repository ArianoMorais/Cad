using UserModule.Domain.Entities;
using UserModule.Domain.Ports;
using UserModule.Infrastructure.Infrastructure.Configuration;

namespace UserModule.Infrastructure.Repositories
{
    public class ChangeLogRepository : Repository<ChangeLog>, IChangeLogRepository
    {
        public ChangeLogRepository(IMongoContext context)
            : base(context, "ChangeLogs")
        {

        }

    }
}
