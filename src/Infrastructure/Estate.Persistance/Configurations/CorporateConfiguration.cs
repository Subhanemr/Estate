using Estate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estate.Persistance.Configurations
{
    internal class CorporateConfiguration : IEntityTypeConfiguration<Corporate>
    {
        public void Configure(EntityTypeBuilder<Corporate> builder)
        {
            builder.Property(u => u.CorporateLink)
                 .IsRequired();

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}
