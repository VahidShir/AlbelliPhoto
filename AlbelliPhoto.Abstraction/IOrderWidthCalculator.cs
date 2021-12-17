using AlbelliPhoto.Abstraction.Entities;

using System.Collections.Generic;

namespace AlbelliPhoto.Abstraction
{
    public interface IOrderWidthCalculator
    {
        float Calculate(IEnumerable<OrderItem> orderItems);
    }
}