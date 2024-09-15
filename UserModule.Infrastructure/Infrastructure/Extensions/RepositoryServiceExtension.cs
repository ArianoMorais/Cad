using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Domain.Ports;
using UserModule.Infrastructure.Repositories;

namespace UserModule.Infrastructure.Extensions
{
    public static class RepositoryServiceExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
