using AlbelliPhoto.Abstraction.Entities;

namespace AlbelliPhoto.Abstraction
{
    /// <summary>
    /// This repository is responsible to store customer orders in db
    /// </summary>
    public interface IOrdersRepository
    {
        /// <summary>
        /// Save order in db
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        int PlaceOrder(Order order);

        /// <summary>
        /// Fetch order from db with requested id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Order GetOrder(int orderId);
    }
}