using AlbelliPhoto.Abstraction;
using AlbelliPhoto.Abstraction.Entities;

using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace AlbelliPhoto.Data.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ProductsDbContext dbContext;

        public OrdersRepository(ProductsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Order GetOrder(int orderId)
        {
            return dbContext.Orders.Include(x => x.OrderItems).FirstOrDefault(o => o.OrderId == orderId);
        }

        public int PlaceOrder(Order order)
        {
            var result = dbContext.Add(order);

            dbContext.SaveChanges();

            return result.Entity.OrderId;
        }
    }
}