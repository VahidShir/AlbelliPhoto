using AlbelliPhoto.Abstraction;
using AlbelliPhoto.Dto;

namespace AlbelliPhoto.Abstraction
{
    public interface IProductFactory
    {
        IProduct GetProduct(ProductType productType);
    }
}