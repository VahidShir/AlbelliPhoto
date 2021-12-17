using AlbelliPhoto.Dto;

using System.Collections.Generic;

namespace AlbelliPhoto.Abstraction
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public IEnumerable<OrderItemDto> OrderItems { get; set; }
    }
}