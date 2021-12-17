using System.Linq;

namespace AlbelliPhoto.Abstraction.Products
{
    public class PhotoBook : IProduct
    {
        /// <summary>
        /// Each PhotoBook occupies 19 mm
        /// </summary>
        public float WidthPerUnit { get; init; } = 19;

        public float CalculateWidth(int count)
        {
            return Enumerable.Repeat(WidthPerUnit, count).Sum();
        }
    }
}