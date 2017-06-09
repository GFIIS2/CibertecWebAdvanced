using Microsoft.EntityFrameworkCore;

namespace Cibertec.Web.Models
{
    public class NorthwinddbContext: DbContext
    {
        public NorthwinddbContext(DbContextOptions<NorthwinddbContext> options): base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
            modelBuilder.Entity<Order>().ToTable("Order");
        }
    }
}
