using CastagramV1.Models;

namespace CastagramV1.Services
{
    public interface ICommentRepository
    {
        Task AddAsync(Comment newComment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(Comment comment);
        Task<IEnumerable<Comment>> GetAllCommentsAsync(int? postid);
    
    }
}
