using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Domain.Ports;
using UserModule.Infrastructure.Repositories;
using UserModule.Infrastructure;
using Microsoft.Extensions.Configuration;
using UserModule.Infrastructure.Infrastructure.Configuration;
using UserModule.Infrastructure.Configuration;

namespace UserModule.Configuration.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositorys(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoSettings = new MongoDbSettings();

            mongoSettings.ConnectionString = configuration.GetConnectionString("MongoDbConnection");
            var mongoSettingsSection = configuration.GetSection("MongoDbSettings");
            mongoSettings.DatabaseName = mongoSettingsSection["DatabaseName"];

            services.AddSingleton<IMongoContext>(new MongoContext(mongoSettings));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChangeLogRepository, ChangeLogRepository>();

            return services;
        }
    }
}
