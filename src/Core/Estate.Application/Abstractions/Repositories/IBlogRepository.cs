using Estate.Domain.Entities;

namespace Estate.Application.Abstractions.Repositories
{
    public interface IBlogRepository : IRepository<Blog>
    {
        void DeleteImage(BlogImage image);
        Task AddComment(BlogComment comment);
        void UpdateComment(BlogComment comment);
        void DeleteComment(BlogComment comment);
        Task AddReply(BlogReply comment);
        void UpdateReply(BlogReply comment);
        void DeleteReply(BlogReply comment);
    }
}
