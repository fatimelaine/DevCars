using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevCars.API.Persistence.Configurations
{
    public class OrderDBConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
               .HasKey(o => o.Id);

            builder
                .HasMany(o => o.ExtraItems)
                .WithOne()
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(o => o.Car)
                .WithOne()
                .HasForeignKey<Order>(o => o.CarId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
