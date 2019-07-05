using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configure
{
    public class PaymentFormConfiguration : IEntityTypeConfiguration<PaymentFormDTO>
    {
        public void Configure(EntityTypeBuilder<PaymentFormDTO> builder)
        {
            builder.HasKey(pf => pf.Id);

            builder.Property(pf => pf.Name).HasMaxLength(30).HasColumnType("varchar(30)").IsRequired();
            builder.Property(pf => pf.Describe).HasMaxLength(800).HasColumnType("varchar(800)").IsRequired(false);
        }
    }
}