using Estate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estate.Persistance.Configurations
{
    internal class ViewTypeConfiguration : IEntityTypeConfiguration<ViewType>
    {
        public void Configure(EntityTypeBuilder<ViewType> builder)
        {
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
