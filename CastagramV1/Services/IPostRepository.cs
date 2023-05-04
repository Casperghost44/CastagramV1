using CastagramV1.Models;

namespace CastagramV1.Services
{
    public interface IPostRepository
    {
        Task AddNewAsync(Post newPost);
        Task UpdateAsync(Post newPost);
        Task DeleteAsync(int? id);
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<IEnumerable<Post>> GetAllPostsByUser(string userid);
        Task<Post> GetPostAsync(int? id);
    }
}
