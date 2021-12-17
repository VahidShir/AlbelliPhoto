using AlbelliPhoto.Abstraction;
using AlbelliPhoto.Abstraction.Entities;
using AlbelliPhoto.Dto;

using Microsoft.Extensions.Logging;

using System;
using System.Linq;
using System.Text.Json;

namespace AlbelliPhoto.Services
{
    internal class ProductOrderService : IProductOrderService
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly IOrderWidthCalculator orderWidthCalculator;
        private readonly ILogger<ProductOrderService> logger;

        public ProductOrderService(IOrdersRepository ordersRepository, IOrderWidthCalculator orderWidthCalculator, ILogger<ProductOrderService> logger)
        {
            this.ordersRepository = ordersRepository;
            this.orderWidthCalculator = orderWidthCalculator;
            this.logger = logger;
        }

        public PlaceOrderResponse PlaceOrder(PlaceOrderRequest request)
        {
            logger.LogTrace("Order process started...!");

            logger.LogDebug($"Request content {JsonSerializer.Serialize(request)}");

            //We could use some third-party libraries such as AutoMapper to convert DTOs to entities and vice-versa in whole solution,
            // But for simplicity we didn't.
            var order = new Order
            {
                OrderItems = request.OrderItems.Select(o => new OrderItem { ProductType = o.ProductType ?? ProductType.Calendar, Quantity = o.Quantity }).ToList()
            };

            try
            {
                int orderId = ordersRepository.PlaceOrder(order);

                var response = new PlaceOrderResponse
                {
                    OrderId = orderId,
                    Message = "Your order submitted Successfully.",
                    AlbelliPhotoStatusCode = 0
                };

                logger.LogWarning("Successfully registered the order.");

                return response;
            }
            catch (Exception exception)
            {
                logger.LogWarning("Failed register your order!", exception);

                return new PlaceOrderResponse
                {
                    Message = "Failed register your order!",
                    AlbelliPhotoStatusCode = 0
                };
            }
        }

        public GetOrderResponse GetOrder(GetOrderRequest request)
        {
            logger.LogDebug($"Request content {JsonSerializer.Serialize(request)}");

            try
            {
                var order = ordersRepository.GetOrder(request.OrderId);

                if (order is null)
                    return null;

                var response = new GetOrderResponse
                {
                    OrderId = request.OrderId,

                    RequiredBinWidth = orderWidthCalculator.Calculate(order.OrderItems),

                    OrderItems = order.OrderItems.Select(o => new OrderItemDto { ProductType = o.ProductType, Quantity = o.Quantity }).ToList()
                };

                logger.LogInformation($"Successfully fetched requested order. Order content: {JsonSerializer.Serialize(response)}");

                return response;
            }
            catch (Exception exception)
            {
                logger.LogWarning($"Failed to fetch requested order with id: {request.OrderId}", exception);

                return null;
            }
        }
    }
}