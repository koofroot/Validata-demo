using Microsoft.EntityFrameworkCore;
using Validata.Data.Extensions;
using Validata.Data.Models;

namespace Validata.Data
{
    public class ValidataContext : DbContext
    {
        public ValidataContext(DbContextOptions<ValidataContext> opt)
            :base(opt) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items{ get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.BuildCustomers();
            modelBuilder.BuildAddresses();
            modelBuilder.BuildOrders();
            modelBuilder.BuildItems();
            modelBuilder.BuildProducts();

            base.OnModelCreating(modelBuilder);
        }
    }
}
