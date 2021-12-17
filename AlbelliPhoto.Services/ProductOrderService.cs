using AlbelliPhoto.Abstraction;
using AlbelliPhoto.Abstraction.Entities;
using AlbelliPhoto.Dto;

using Microsoft.Extensions.Logging;

using System;
using System.Linq;
using System.Text.Json;

namespace AlbelliPhoto.Services
{
    public class ProductOrderService : IProductOrderService
    {
        private readonly IOrderWidthCalculator orderWidthCalculator;
        private readonly ILogger<ProductOrderService> logger;

        public ProductOrderService(IOrderWidthCalculator orderWidthCalculator, ILogger<ProductOrderService> logger)
        {
            this.orderWidthCalculator = orderWidthCalculator;
            this.logger = logger;
        }

        public PlaceOrderResponse PlaceOrder(PlaceOrderRequest request)
        {


            //use autofactor
            var order = new Order
            {
                //OrderId = request.OrderId,
                OrderItems = request.OrderItems.Select(o => new OrderItem { ProductType = o.ProductType ?? ProductType.Calendar, Quantity = o.Quantity }).ToList()
            };

            try
            {
                int orderId = 0;

                var response = new PlaceOrderResponse
                {
                    OrderId = orderId,
                    Message = "Your order submitted Successfully.",
                    AlbelliPhotoStatusCode = 0
                };

                return response;
            }
            catch (Exception exception)
            {
                return new PlaceOrderResponse
                {
                    Message = "Failed register your order!",
                    AlbelliPhotoStatusCode = 0
                };
                //log
            }
        }

        public GetOrderResponse GetOrder(GetOrderRequest request)
        {
            logger.LogTrace("Order process started...!");

            logger.LogDebug($"Request content {JsonSerializer.Serialize(request)}");

            try
            {
                var order = new Order
                {
                    OrderItems = new()
                };

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
                logger.LogWarning($"Failed to fetch requested order with id: {request.OrderId}");

                return null;
            }
        }
    }
}