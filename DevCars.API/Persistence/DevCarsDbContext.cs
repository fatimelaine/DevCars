using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Persistence
{
    public class DevCarsDbContext : DbContext
    {
        public DevCarsDbContext(DbContextOptions<DevCarsDbContext> options) : base(options)
        {      
        }
       
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ExtraOrderItem> ExtraOrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Car>()
                .Property(c => c.Brand)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Car>()
                .Property(c => c.Brand)
                .IsRequired()
                .HasColumnName("Marca")
                .HasColumnType("VARCHAR(30)")
                .HasDefaultValue("PADRÃO")
                .HasMaxLength(30);

            //modelBuilder.Entity<Car>()
            //    .ToTable("db_Car");

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer) // propriedade de navegação
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.ExtraItems)
                .WithOne()
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Car)
                .WithOne()
                .HasForeignKey<Order>(o => o.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExtraOrderItem>()
                .HasKey(e => e.Id);
        }
    }
}
