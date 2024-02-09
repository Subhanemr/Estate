using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Estate.Persistance.Implementations.Repositories
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        private readonly DbSet<BlogComment> _dbComment;
        private readonly DbSet<BlogReply> _dbReply;

        public BlogRepository(AppDbContext context) : base(context)
        {

            _dbComment = context.Set<BlogComment>();
            _dbReply = context.Set<BlogReply>();
        }

        public async Task AddComment(BlogComment item)
        {
            await _dbComment.AddAsync(item);
        }
        public async Task AddReply(BlogReply item)
        {
            await _dbReply.AddAsync(item);
        }
    }
}
