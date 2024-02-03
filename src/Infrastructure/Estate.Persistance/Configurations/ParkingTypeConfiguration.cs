using Estate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estate.Persistance.Configurations
{
    internal class ParkingTypeConfiguration : IEntityTypeConfiguration<ParkingType>
    {
        public void Configure(EntityTypeBuilder<ParkingType> builder)
        {
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
