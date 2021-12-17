namespace AlbelliPhoto.Abstraction
{
    public interface IProduct
    {
        /// <summary>
        /// The width amount each product unit occupies
        /// </summary>
        public float WidthPerUnit { get; init; }


        /// <summary>
        /// Calculate total amount of width for given count of the product
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        float CalculateWidth(int count);
    }
}