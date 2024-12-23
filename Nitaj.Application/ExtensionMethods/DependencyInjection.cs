using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Nitaj.Application.ExtensionMethods
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationCore(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
