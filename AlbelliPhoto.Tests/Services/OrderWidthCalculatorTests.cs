using AlbelliPhoto.Abstraction;
using AlbelliPhoto.Abstraction.Entities;
using AlbelliPhoto.Abstraction.Products;
using AlbelliPhoto.Dto;
using AlbelliPhoto.Services;

using AutoFixture;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using NSubstitute;

using System;
using System.Collections;
using System.Collections.Generic;

using Xunit;

namespace AlbelliPhoto.Tests.Services
{
    public class OrderWidthCalculatorTests
    {
        private readonly Random random;
        private Fixture fixture;

        public OrderWidthCalculatorTests()
        {
            random = new Random();
            fixture = new Fixture();
        }

        /// <param name="orderItems"></param>
        /// <param name="expectedTotalWidth"></param>
        [Theory]
        [ClassData(typeof(CalculateWidthClassData))]
        public void Calculate_MustReturnCorrectOrderTotalWidth_GivenDifferentSelectOfSubmittedOrders(IEnumerable<OrderItem> orderItems, float expectedTotalWidth)
        {
            //Arrange
            var productFactoryMock = Substitute.For<IProductFactory>();
            productFactoryMock.GetProduct(ProductType.PhotoBook).Returns(new PhotoBook());
            productFactoryMock.GetProduct(ProductType.Calendar).Returns(new Calendar());
            productFactoryMock.GetProduct(ProductType.Canvas).Returns(new Canvas());
            productFactoryMock.GetProduct(ProductType.Cards).Returns(new Cards());
            productFactoryMock.GetProduct(ProductType.Mug).Returns(new Mug());

            var loggerMock = Substitute.For<ILogger<OrderWidthCalculator>>();

            var orderWidthCalculator = new OrderWidthCalculator(productFactoryMock, loggerMock);

            //Act
            float actualTotalWidth = orderWidthCalculator.Calculate(orderItems);


            //Assert
            actualTotalWidth.Should().BeApproximately(expectedTotalWidth, 0.1F);
        }

        private class CalculateWidthClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new List<OrderItem>()
                    {
                        new OrderItem{ ProductType = ProductType.PhotoBook, Quantity = 1},
                    },
                    19
                };

                yield return new object[]
                 {
                    new List<OrderItem>()
                    {
                        new OrderItem{ ProductType = ProductType.Calendar, Quantity = 1},
                    },
                    10
                 };

                yield return new object[]
                {
                    new List<OrderItem>()
                    {
                        new OrderItem{ ProductType = ProductType.Canvas, Quantity = 1},
                    },
                    16
                };

                yield return new object[]
                 {
                    new List<OrderItem>()
                    {
                        new OrderItem{ ProductType = ProductType.Cards, Quantity = 1},
                    },
                    4.7
                 };

                yield return new object[]
                 {
                    new List<OrderItem>()
                    {
                        new OrderItem{ ProductType = ProductType.PhotoBook, Quantity = 5},
                        new OrderItem{ ProductType = ProductType.Calendar, Quantity = 4},
                        new OrderItem{ ProductType = ProductType.Canvas, Quantity = 1},
                        new OrderItem{ ProductType = ProductType.Cards, Quantity = 3},
                        new OrderItem{ ProductType = ProductType.Mug, Quantity = 1},
                    },
                    259.1
                 };

                //Here are differant variants for Mug product since it has special business
                yield return new object[]
                 {
                    new List<OrderItem>()
                    {
                        new OrderItem{ ProductType = ProductType.Mug, Quantity = 1},
                    },
                    94
                 };

                yield return new object[]
                 {
                    new List<OrderItem>()
                    {
                        new OrderItem{ ProductType = ProductType.Mug, Quantity = 4},
                    },
                    94
                 };

                yield return new object[]
                 {
                    new List<OrderItem>()
                    {
                        new OrderItem{ ProductType = ProductType.Mug, Quantity = 5},
                    },
                    188
                 };

                yield return new object[]
                 {
                    new List<OrderItem>()
                    {
                        new OrderItem{ ProductType = ProductType.Mug, Quantity = 50},
                    },
                    1222
                 };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}