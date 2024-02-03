using Estate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estate.Persistance.Configurations
{
    internal class ExteriorTypeConfiguration : IEntityTypeConfiguration<ExteriorType>
    {
        public void Configure(EntityTypeBuilder<ExteriorType> builder)
        {
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
