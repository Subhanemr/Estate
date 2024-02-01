using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class BlogReplyRepository : Repository<BlogReply>, IBlogReplyRepository
    {
        public BlogReplyRepository(AppDbContext context) : base(context) { }
    }
}
