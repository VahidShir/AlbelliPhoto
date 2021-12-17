using AlbelliPhoto.Abstraction;
using AlbelliPhoto.Dto;
using AlbelliPhoto.Services;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;

namespace AlbelliPhoto.Server.Controllers
{
    [ApiController]
    [Route("Products/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IProductFactory productFactory;

        public OrderController(IProductFactory productFactory)
        {
            this.productFactory = productFactory;
        }

        [HttpGet("{OrderId}")]
        public GetOrderResponse GetOrder([FromRoute] GetOrderRequest request)
        {
            var test = productFactory.GetProduct(ProductType.Calendar);

            var sampleResponse = new GetOrderResponse
            {
                RequiredBinWidth = 1,
                OrderItems = new List<OrderItemDto>()
                {
                    new OrderItemDto{ ProductType = ProductType.Calendar, Quantity = 1},
                    new OrderItemDto{ ProductType = ProductType.Mug, Quantity = 3},
                    new OrderItemDto{ ProductType = ProductType.Canvas, Quantity = 1},
                    new OrderItemDto{ ProductType = ProductType.Cards, Quantity = 2}
                }
            };

            return sampleResponse;
        }

        [HttpPost()]
        public PlaceOrderResponse PlaceOrder(PlaceOrderRequest request)
        {
            var sampleResponse = new PlaceOrderResponse
            {
                OrderId = 5,
                Message = "Successfully placed your order."
            };

            return sampleResponse;
        }
    }
}