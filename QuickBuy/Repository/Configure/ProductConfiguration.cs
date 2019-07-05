using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configure
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductDTO>
    {
        public void Configure(EntityTypeBuilder<ProductDTO> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(pf => pf.Name).HasMaxLength(240).HasColumnType("varchar(240)").IsRequired();
            builder.Property(pf => pf.Describe).HasMaxLength(800).HasColumnType("varchar(800)").IsRequired(false);
            builder.Property(pf => pf.Price).HasColumnType("decimal").IsRequired();
        }
    }
}