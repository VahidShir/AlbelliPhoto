using AlbelliPhoto.Abstraction;
using AlbelliPhoto.Abstraction.Entities;

using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Text.Json;

namespace AlbelliPhoto.Services
{
    internal class OrderWidthCalculator : IOrderWidthCalculator
    {
        private readonly IProductFactory productFactory;
        private readonly ILogger<OrderWidthCalculator> logger;

        public OrderWidthCalculator(IProductFactory productFactory, ILogger<OrderWidthCalculator> logger)
        {
            this.productFactory = productFactory;
            this.logger = logger;
        }

        public float Calculate(IEnumerable<OrderItem> orderItems)
        {
            logger.LogInformation("Calculating total order width");
            logger.LogInformation($"Order items: {JsonSerializer.Serialize(orderItems)}");

            float totalWidth = 0;

            foreach (var item in orderItems)
            {
                IProduct product = productFactory.GetProduct(item.ProductType);

                totalWidth += product.CalculateWidth(item.Quantity);
            }

            logger.LogInformation($"Total order width: {totalWidth}");

            return totalWidth;
        }
    }
}