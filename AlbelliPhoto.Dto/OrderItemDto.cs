using System.ComponentModel.DataAnnotations;

namespace AlbelliPhoto.Dto
{
    public class OrderItemDto
    {
        //We make ProductType nullable so that customers can't submit empty ProductType (which would fallback to a default type)
        [Required]
        public ProductType? ProductType { get; set; }

        [Required]
        [Range(1, 999)]
        public int Quantity { get; set; }
    }
}