using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configure
{
    public class AddressConfiguration : IEntityTypeConfiguration<AddressDTO>
    {
        public void Configure(EntityTypeBuilder<AddressDTO> builder)
        {
            builder.HasKey(ads => ads.Id);

            builder.Property(ads => ads.CEP).HasColumnType("int").IsRequired();
            builder.Property(ads => ads.Country).HasMaxLength(30).HasColumnType("varchar(30)").IsRequired();
            builder.Property(ads => ads.State).HasMaxLength(100).HasColumnType("varchar(100)").IsRequired();
            builder.Property(ads => ads.City).HasMaxLength(130).HasColumnType("varchar(130)").IsRequired();
            builder.Property(ads => ads.Neighborhood).HasMaxLength(100).HasColumnType("varchar(100)").IsRequired();
            builder.Property(ads => ads.Street).HasMaxLength(150).HasColumnType("varchar(150)").IsRequired();
            builder.Property(ads => ads.Number).HasMaxLength(6).HasColumnType("varchar(6)").IsRequired();
            builder.Property(ads => ads.Complement).HasMaxLength(6).HasColumnType("varchar(6)").IsRequired(false);
        }
    }
}