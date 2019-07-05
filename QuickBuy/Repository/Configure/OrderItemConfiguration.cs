using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configure
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemDTO>
    {
        public void Configure(EntityTypeBuilder<OrderItemDTO> builder)
        {
            builder.HasKey(oi => oi.Id);

            builder.HasOne(oi => oi.ProductOrderItem);
            builder.HasOne(oi => oi.AddressOrderItem);

            builder.Property(oi => oi.Amount).HasColumnType("int").IsRequired();
        }
    }
}