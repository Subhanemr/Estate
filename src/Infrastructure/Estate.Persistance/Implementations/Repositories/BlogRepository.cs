using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Estate.Persistance.Implementations.Repositories
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        private readonly DbSet<BlogImage> _dbImage;
        private readonly DbSet<BlogComment> _dbComment;
        private readonly DbSet<BlogReply> _dbReply;

        public BlogRepository(AppDbContext context) : base(context) 
        {
            _dbImage = context.Set<BlogImage>();
            _dbComment = context.Set<BlogComment>();
            _dbReply = context.Set<BlogReply>();
        }

        public void DeleteImage(BlogImage image)
        {
            _dbImage.Remove(image);
        }
        public async Task AddComment(BlogComment comment)
        {
            await _dbComment.AddAsync(comment);
        }
        public void UpdateComment(BlogComment comment)
        {
            _dbComment.Update(comment);
        }
        public void DeleteComment(BlogComment comment)
        {
            _dbComment.Remove(comment);
        }
        public async Task AddReply(BlogReply comment)
        {
            await _dbReply.AddAsync(comment);
        }
        public void UpdateReply(BlogReply comment)
        {
            _dbReply.Update(comment);
        }
        public void DeleteReply(BlogReply comment)
        {
            _dbReply.Remove(comment);
        }
    }
}
