using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Application.Services;
using UserModule.Domain.Ports;
using UserModule.Domain.Services;

namespace UserModule.Application.Application.Extensions
{
    public static class ServicesExtensios
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
