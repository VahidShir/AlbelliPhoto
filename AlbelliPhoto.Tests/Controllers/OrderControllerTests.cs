using AlbelliPhoto.Abstraction;
using AlbelliPhoto.Dto;
using AlbelliPhoto.Server.Controllers;

using AutoFixture;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using NSubstitute;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

using Xunit;

namespace AlbelliPhoto.Tests.Controllers
{
    public class OrderControllerTests
    {
        private readonly Random random;
        private Fixture fixture;

        public OrderControllerTests()
        {
            random = new Random();
            fixture = new Fixture();
        }

        [Fact]
        public void GetOrder_MustReturnNotFound404_GivenNotAnyOrdersFoundInDB()
        {
            //Arrange
            var productOrderServiceMock = Substitute.For<IProductOrderService>();
            productOrderServiceMock.GetOrder(default).ReturnsForAnyArgs((GetOrderResponse)null);

            var request = new GetOrderRequest { OrderId = fixture.Create<int>() };

            var controller = new OrderController(productOrderServiceMock);
            int expectedStatusCode = (int)HttpStatusCode.NotFound;

            //Act
            var result = controller.GetOrder(request) as NotFoundObjectResult;


            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(expectedStatusCode);
        }

        [Fact]
        public void GetOrder_MustReturnAValidOrder_GivenAnyOrdersFoundInDB()
        {
            //Arrange

            int orderId = fixture.Create<int>();

            var request = new GetOrderRequest { OrderId = orderId };

            var orderResponse = new GetOrderResponse
            {
                OrderId = orderId,

                OrderItems = new List<OrderItemDto>
                {
                    new OrderItemDto{ ProductType = ProductType.Calendar, Quantity = 1},
                    new OrderItemDto{ ProductType = ProductType.Mug, Quantity = 3},
                    new OrderItemDto{ ProductType = ProductType.Canvas, Quantity = 1},
                    new OrderItemDto{ ProductType = ProductType.Cards, Quantity = 2}
                }
            };

            var productOrderServiceMock = Substitute.For<IProductOrderService>();
            productOrderServiceMock.GetOrder(request).Returns(orderResponse);

            var controller = new OrderController(productOrderServiceMock);
            int expectedStatusCode = (int)HttpStatusCode.OK;

            //Act
            var result = controller.GetOrder(request) as OkObjectResult;

            //Assert

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(expectedStatusCode);
            result.Value.Should().BeOfType<GetOrderResponse>();
            result.Value.As<GetOrderResponse>().OrderId.Should().Be(orderId);
        }

        [Fact]
        public void PlaceOrder_MustReturnALbelliStatusCodeNonZero_GivenRegisteringOrderFailes()
        {
            //Arrange

            var placeOrderResponse = new PlaceOrderResponse
            {
                AlbelliPhotoStatusCode = random.Next(1, 100)
            };

            var placeOrderRequest = new PlaceOrderRequest
            {
                OrderItems = new List<OrderItemDto> { new OrderItemDto { ProductType = ProductType.Calendar, Quantity = 1 }, }
            };

            var productOrderServiceMock = Substitute.For<IProductOrderService>();
            productOrderServiceMock.PlaceOrder(default).ReturnsForAnyArgs(placeOrderResponse);

            var controller = new OrderController(productOrderServiceMock);
            int expectedStatusCode = (int)HttpStatusCode.InternalServerError;


            //Act
            var result = controller.PlaceOrder(placeOrderRequest) as ObjectResult;


            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(expectedStatusCode);
            result.Value.As<PlaceOrderResponse>().AlbelliPhotoStatusCode.Should().Be(placeOrderResponse.AlbelliPhotoStatusCode);
        }

        //Note: The test below belongs to integration test, but for simplicity I chose to not write integration tests
        //[Theory]
        //[ClassData(typeof(PlaceOrderRequestClassData))]
        private void PlaceOrder_Must_Return_BadRequest400_Given_Customer_Submits_Invalid_Order_Data(PlaceOrderRequest request)
        {
            //Arrange
            var productOrderServiceMock = Substitute.For<IProductOrderService>();
            //productOrderServiceMock.GetOrder(default).ReturnsForAnyArgs((GetOrderResponse)null);

            var controller = new OrderController(productOrderServiceMock);
            int expectedStatusCode = (int)HttpStatusCode.BadRequest;

            //Act
            var result = controller.PlaceOrder(request) as BadRequestObjectResult;


            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(expectedStatusCode);
            result.Value.Should().BeNull();
        }

        private class PlaceOrderRequestClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new PlaceOrderRequest
                    {
                        OrderItems = new List<OrderItemDto>()
                        {
                            new OrderItemDto{ ProductType = null},
                        }
                    }
                };

                yield return new object[]
                {
                    new PlaceOrderRequest
                    {
                        OrderItems = new List<OrderItemDto>()
                        {
                            new OrderItemDto{ ProductType = ProductType.Mug, Quantity = 0},
                        }
                    }
                };

                yield return new object[]
                {
                    new PlaceOrderRequest
                    {
                        OrderItems = new List<OrderItemDto>()
                        {
                            new OrderItemDto{ ProductType = ProductType.Canvas, Quantity = -1},
                        }
                    }
                };

                yield return new object[]
                {
                    new PlaceOrderRequest
                    {
                        OrderItems = new List<OrderItemDto>()
                        {
                            new OrderItemDto{ ProductType = ProductType.Cards, Quantity = 999999999}
                        }
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}