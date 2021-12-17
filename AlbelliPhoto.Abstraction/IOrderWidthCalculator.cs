using AlbelliPhoto.Abstraction.Entities;

using System.Collections.Generic;

namespace AlbelliPhoto.Abstraction
{
    /// <summary>
    /// This service is responsible to calculate the total width of a customer's order
    /// </summary>
    public interface IOrderWidthCalculator
    {
        float Calculate(IEnumerable<OrderItem> orderItems);
    }
}