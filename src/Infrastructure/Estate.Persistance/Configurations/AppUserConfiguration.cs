using Estate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estate.Persistance.Configurations
{
    internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(u => u.Surname)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(u => u.Email)
                .HasMaxLength(255);

            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(25);

            builder.Property(u => u.PhoneSecond)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(u => u.Address)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(u => u.About)
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
