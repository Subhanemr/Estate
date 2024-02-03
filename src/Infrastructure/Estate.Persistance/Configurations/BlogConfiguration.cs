using Estate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estate.Persistance.Configurations
{
    internal class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Description)
                .IsRequired()
                .HasMaxLength(1500);

            builder.Property(u => u.FaceLink)
                .IsRequired();

            builder.Property(u => u.TwitLink)
                .IsRequired();

            builder.Property(u => u.InstaLink)
                .IsRequired();

            builder.Property(u => u.LinkedLink)
                .IsRequired();
        }
    }
}
