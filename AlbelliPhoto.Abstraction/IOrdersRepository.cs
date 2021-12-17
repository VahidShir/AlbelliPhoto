using AlbelliPhoto.Abstraction.Entities;

namespace AlbelliPhoto.Abstraction
{
    public interface IOrdersRepository
    {
        int PlaceOrder(Order order);

        Order GetOrder(int orderId);
    }
}