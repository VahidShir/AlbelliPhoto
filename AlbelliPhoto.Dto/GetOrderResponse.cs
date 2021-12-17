
using System.Collections.Generic;

namespace AlbelliPhoto.Dto
{
    public class GetOrderResponse
    {
        public int OrderId { get; set; }

        public IList<OrderItemDto> OrderItems { get; set; }

        public float RequiredBinWidth { get; set; }
    }
}