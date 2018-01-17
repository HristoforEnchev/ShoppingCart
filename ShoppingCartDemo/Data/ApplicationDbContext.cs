namespace ShoppingCartDemo.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using ShoppingCartDemo.Data.Models;
    using ShoppingCartDemo.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            builder
                .Entity<Order>()
                .HasOne(o => o.ApplicationUser)
                .WithMany(au => au.Orders)
                .HasForeignKey(o => o.ApplicationUserId);

            base.OnModelCreating(builder);
        }
    }
}
