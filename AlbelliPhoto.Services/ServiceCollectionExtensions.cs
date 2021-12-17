using AlbelliPhoto.Abstraction;

using Microsoft.Extensions.DependencyInjection;

namespace AlbelliPhoto.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProductServices(this IServiceCollection services)
        {
            services.AddScoped<IProductOrderService, ProductOrderService>();
            services.AddSingleton<IProductFactory, ProductFactory>();
            services.AddSingleton<IOrderWidthCalculator, OrderWidthCalculator>();

            return services;
        }
    }
}