using Microsoft.Extensions.DependencyInjection;
using Nitaj.Application.ExtensionMethods;
using Nitaj.Application.Interfaces;
using Nitaj.Application.Services;
using Nitaj.Infrastructure.UnitOfWorks;

namespace Nitaj.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IToDoService, ToDoService>();

            services.AddApplicationCore();
        }
    }
}
