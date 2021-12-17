using System.Linq;

namespace AlbelliPhoto.Abstraction.Products
{
    public class Canvas : IProduct
    {
        /// <summary>
        /// Each canvas occupies 16.0 mm
        /// </summary>
        public float WidthPerUnit { get; init; } = 16;

        public float CalculateWidth(int count)
        {
            return Enumerable.Repeat(WidthPerUnit, count).Sum();
        }
    }
}