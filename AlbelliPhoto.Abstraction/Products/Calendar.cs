using System.Linq;

namespace AlbelliPhoto.Abstraction.Products
{
    public class Calendar : IProduct
    {
        /// <summary>
        /// Each Calendar occupies 10.0 mm
        /// </summary>
        public float WidthPerUnit { get; init; } = 10.0f;

        public float CalculateWidth(int count)
        {
            return Enumerable.Repeat(WidthPerUnit, count).Sum();
        }
    }
}