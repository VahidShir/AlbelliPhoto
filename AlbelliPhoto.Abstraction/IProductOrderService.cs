using AlbelliPhoto.Dto;

namespace AlbelliPhoto.Abstraction
{
    public interface IProductOrderService
    {
        PlaceOrderResponse PlaceOrder(PlaceOrderRequest request);

        GetOrderResponse GetOrder(GetOrderRequest request);
    }
}