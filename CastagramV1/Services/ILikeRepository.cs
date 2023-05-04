using CastagramV1.Models;

namespace CastagramV1.Services
{
    public interface ILikeRepository
    {
        Task AddAsync(Like newLike);
        Task DeleteAsync(Like like);
        Task<IEnumerable<Like>> GetAllLikesAsync();
    }
}
