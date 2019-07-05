using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configure
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderDTO>
    {
        public void Configure(EntityTypeBuilder<OrderDTO> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.UserOrder);
            builder.HasOne(o => o.PaymentFormOrder);

            builder.Property(o => o.OrderDate).HasColumnType("date").IsRequired();
            builder.Property(o => o.DeliveryForecastDate).HasColumnType("datetime").IsRequired(false);

            builder.HasMany(o => o.OrderItems).WithOne(oi => oi.OrderOrderItem);
        }
    }
}