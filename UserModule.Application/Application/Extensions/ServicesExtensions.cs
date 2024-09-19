using Microsoft.Extensions.DependencyInjection;
using UserModule.Application.Services;
using UserModule.Domain.Services;

namespace UserModule.Application.Application.Extensions
{
    public static class ServicesExtensios
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IChangeLogService, ChangeLogService>();

            return services;
        }
    }
}
