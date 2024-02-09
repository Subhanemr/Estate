using Estate.Domain.Entities;

namespace Estate.Application.Abstractions.Repositories
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task AddComment(BlogComment item);
        Task AddReply(BlogReply item);
    }
}
