namespace UserModule.Application.Application.Extensions
{
    using AutoMapper;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddCustomAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}