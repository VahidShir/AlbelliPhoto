using AlbelliPhoto.Abstraction;
using AlbelliPhoto.Dto;

using System;
using System.Linq;

namespace AlbelliPhoto.Services
{
    internal class ProductFactory : IProductFactory
    {
        public IProduct GetProduct(ProductType productType)
        {
            var productsTypes = typeof(IProduct).Assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract)
                .Where(t => t.GetInterfaces().Contains(typeof(IProduct)))
                .ToArray();

            Type theType = productsTypes.Single(t => t.Name == productType.ToString());

            var product = (IProduct)Activator.CreateInstance(theType);

            return product;
        }
    }
}