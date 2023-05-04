using CastagramV1.Models;

namespace CastagramV1.Services
{
    public interface ILikeRepository
    {
        Task AddAsync(Like newLike);
        Task DeleteAsync(int? id);
        Task<IEnumerable<Like>> GetAllLikesAsync();
        Task<Like> GetLikeAsync(int? id);
    }
}
