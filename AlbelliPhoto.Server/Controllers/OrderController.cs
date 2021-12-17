using AlbelliPhoto.Abstraction;
using AlbelliPhoto.Dto;
using AlbelliPhoto.Services;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Net;

namespace AlbelliPhoto.Server.Controllers
{
    [ApiController]
    [Route("Products/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IProductOrderService productOrderService;

        public OrderController(IProductOrderService productOrderService)
        {
            this.productOrderService = productOrderService;
        }

        [HttpGet("{OrderId}")]
        public IActionResult GetOrder([FromRoute] GetOrderRequest request)
        {
            GetOrderResponse response = productOrderService.GetOrder(request);

            if (response is null)
                return NotFound($"Thr order with the id:{request.OrderId} was not found");

            return Ok(response);
        }

        [HttpPost()]
        public IActionResult PlaceOrder(PlaceOrderRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            PlaceOrderResponse response = productOrderService.PlaceOrder(request);

            if (response.AlbelliPhotoStatusCode != 0)
                return StatusCode((int)HttpStatusCode.InternalServerError, response);

            return Ok(response);
        }
    }
}