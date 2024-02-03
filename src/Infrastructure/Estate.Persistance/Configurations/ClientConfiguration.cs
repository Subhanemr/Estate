using Estate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estate.Persistance.Configurations
{
    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(u => u.Comment)
                .IsRequired()
                .HasMaxLength(1500);

            builder.Property(u => u.Country)
                .IsRequired();
        }
    }
}
