using AlbelliPhoto.Abstraction.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlbelliPhoto.Data.Configurations
{
    public class OrdersMappings : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => new { x.OrderId });
        }
    }
}