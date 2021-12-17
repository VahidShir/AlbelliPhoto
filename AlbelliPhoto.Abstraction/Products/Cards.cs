using System.Linq;

namespace AlbelliPhoto.Abstraction.Products
{
    public class Cards : IProduct
    {
        /// <summary>
        /// Each set of greeting cards occupies 4.7 mm
        /// </summary>
        public float WidthPerUnit { get; init; } = 4.7f;

        public float CalculateWidth(int count)
        {
            return Enumerable.Repeat(WidthPerUnit, count).Sum();
        }
    }
}