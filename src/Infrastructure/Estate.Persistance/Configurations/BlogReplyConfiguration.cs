using Estate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estate.Persistance.Configurations
{
    internal class BlogReplyConfiguration : IEntityTypeConfiguration<BlogReply>
    {
        public void Configure(EntityTypeBuilder<BlogReply> builder)
        {
            builder.Property(u => u.ReplyComment)
                .IsRequired()
                .HasMaxLength(1500);
        }
    }
}
