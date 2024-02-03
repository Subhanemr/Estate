using Estate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estate.Persistance.Configurations
{
    internal class FeaturesConfiguration : IEntityTypeConfiguration<Features>
    {
        public void Configure(EntityTypeBuilder<Features> builder)
        {
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
