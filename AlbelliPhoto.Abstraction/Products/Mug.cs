using System;
using System.Linq;

namespace AlbelliPhoto.Abstraction.Products
{
    public class Mug : IProduct
    {
        //The amount of mugs which can be stacked on per stack
        private int stackSize = 4;

        /// <summary>
        /// Each Mug occupies 94 mm
        /// </summary>
        public float WidthPerUnit { get; init; } = 94;

        public float CalculateWidth(int count)
        {
            var numberOfStacks = Math.Ceiling(count / (stackSize * 1.0));

            return Enumerable.Repeat(WidthPerUnit, (int)numberOfStacks).Sum();
        }
    }
}