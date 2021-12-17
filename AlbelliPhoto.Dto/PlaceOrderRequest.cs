using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlbelliPhoto.Dto
{
    public class PlaceOrderRequest
    {
        //public int OrderId { get; set; }

        [Required]
        public IList<OrderItemDto> OrderItems { get; set; }
    }
}