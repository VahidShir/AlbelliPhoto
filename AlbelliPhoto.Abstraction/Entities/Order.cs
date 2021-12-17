using System;
using System.Collections.Generic;

namespace AlbelliPhoto.Abstraction.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public override string ToString()
        {
            return $"Order content:{Environment.NewLine}" +
                $" order id:{OrderId}{Environment.NewLine}" +
                $"Order items:{OrderItems}";
        }
    }
}