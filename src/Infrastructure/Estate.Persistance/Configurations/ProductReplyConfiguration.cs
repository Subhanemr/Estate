using Estate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estate.Persistance.Configurations
{
    internal class ProductReplyConfiguration : IEntityTypeConfiguration<ProductReply>
    {
        public void Configure(EntityTypeBuilder<ProductReply> builder)
        {
            throw new NotImplementedException();
        }
    }
}
