using AlbelliPhoto.Dto;

using System;

namespace AlbelliPhoto.Abstraction.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        public ProductType ProductType { get; set; }

        public int Quantity { get; set; }

        //public Order Order { get; set; }

        public override string ToString()
        {
            return $"Order Item content:{Environment.NewLine}" +
                $" id:{Id}{Environment.NewLine}" +
                $"Product type:{ProductType}" +
                $"Quantity:{Quantity}";
        }
    }
}