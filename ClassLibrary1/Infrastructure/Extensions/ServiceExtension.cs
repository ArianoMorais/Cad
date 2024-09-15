using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Application.Services;
using UserModule.Domain.Ports;
using UserModule.Domain.Services;
using UserModule.Infrastructure.Repositories;
using UserModule.Infrastructure.Repositories.UserModule.Infrastructure.Repositories;

namespace UserModule.Infrastructure.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Registre os serviços aqui
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddTransient<IUserService, UserService>();

            // Adicione outros serviços se necessário
            return services;
        }
    }
}
