using AlbelliPhoto.Abstraction;
using AlbelliPhoto.Dto;

namespace AlbelliPhoto.Services
{
    public interface IProductFactory
    {
        IProduct GetProduct(ProductType productType);
    }
}