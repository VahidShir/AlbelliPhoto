using AlbelliPhoto.Abstraction;

using Microsoft.Extensions.DependencyInjection;

namespace AlbelliPhoto.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProductServices(this IServiceCollection services)
        {

            services.AddSingleton<IProductFactory, ProductFactory>();

            return services;
        }
    }
}