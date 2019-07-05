using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configure
{
    public class UserConfiguration : IEntityTypeConfiguration<UserDTO>
    {
        public void Configure(EntityTypeBuilder<UserDTO> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(u => u.AccountUser);
            builder.HasOne(u => u.AddressUser);

            builder.Property(u => u.Name).HasMaxLength(240).HasColumnType("varchar(240)").IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(500).HasColumnType("varchar(500)").IsRequired(false);

            builder.HasMany(u => u.Orders).WithOne(o => o.UserOrder);
        }
    }
}