using Microsoft.EntityFrameworkCore;
using Validata.Data.Models;

namespace Validata.Data.Extensions
{
    internal static class ModelBuilderExtensions
    {
        internal static void BuildCustomers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(cust =>
            {
                cust.HasKey(x => x.Id);

                cust.Property(x => x.FirstName)
                .IsRequired();

                cust.Property(x => x.LastName)
                .IsRequired();

                cust.HasOne(x => x.Address)
                .WithOne(x => x.Customer)
                .HasForeignKey((Address x) => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }

        internal static void BuildAddresses(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(addr =>
            {
                addr.HasKey(x => x.Id);

                addr.Property(x => x.StreetAddress)
                .IsRequired();

                addr.Property(x => x.PostalCode)
                .IsRequired();

                addr.HasOne(x => x.Customer)
                .WithOne(x => x.Address)
                .HasForeignKey((Customer x) => x.AddressId)
                .OnDelete(DeleteBehavior.NoAction);
            });
        }

        internal static void BuildOrders(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(order =>
            {
                order.HasKey(x => x.Id);

                order.Property(x => x.OrderDate)
                .IsRequired();

                order.Property(x => x.TotalPrice)
                .IsRequired();

                order.HasMany(x => x.Items)
                .WithOne(x => x.Order)
                .HasForeignKey((x) => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

                order.HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }

        internal static void BuildItems(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(item =>
            {
                item.HasKey(x => x.Id);

                item.Property(x => x.Count)
                .IsRequired();

                item.HasOne(x => x.Product)
                .WithMany(x => x.Items)
                .OnDelete(DeleteBehavior.NoAction);

                item.HasOne(x => x.Order)
                .WithMany(x => x.Items)
                .OnDelete(DeleteBehavior.NoAction);
            });
        }

        internal static void BuildProducts(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(prod =>
            {
                prod.HasKey(x => x.Id);

                prod.Property(x => x.Price)
                .IsRequired();

                prod.Property(x => x.Name)
                .IsRequired();

                prod.HasMany(x => x.Items)
                .WithOne(x => x.Product)
                .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
