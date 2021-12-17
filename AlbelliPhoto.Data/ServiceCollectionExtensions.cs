using AlbelliPhoto.Abstraction;
using AlbelliPhoto.Data.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace AlbelliPhoto.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrdersRepository, OrdersRepository>();

            return services;
        }
    }
}