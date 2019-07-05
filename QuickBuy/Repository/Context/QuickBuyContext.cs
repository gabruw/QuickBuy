using Domain.DTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configure;

namespace Repository.Context
{
    public class QuickBuyContext : IdentityDbContext<AccountIDTO>
    {
        public QuickBuyContext(DbContextOptions<QuickBuyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentFormConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AddressDTO> Address { get; set; }

        public DbSet<OrderDTO> Order { get; set; }

        public DbSet<OrderItemDTO> OrderItem { get; set; }

        public DbSet<PaymentFormDTO> PaymentForm { get; set; }

        public DbSet<ProductDTO> Product { get; set; }

        public DbSet<UserDTO> User { get; set; }
    }
}