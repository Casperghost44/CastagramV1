using CastagramV1.Models;
using CastagramV1.Services;
using Microsoft.EntityFrameworkCore;

namespace CastagramV1.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly DBContext _db;
        public LikeRepository(DBContext db)
        {
            _db = db;
        }
        public async Task AddAsync(Like newLike)
        {
            var like = new Like()
            {
                Id = newLike.Id,
                Author = newLike.Author,
                AuthorId = newLike.AuthorId,
                dateTime = newLike.dateTime,
                Post = newLike.Post,
                PostId = newLike.PostId
            };
            await _db.Likes.AddAsync(like);
            await _db.SaveChangesAsync();
        }

        public Task DeleteAsync(Like like)
        {
            _db.Likes.Remove(like);
            _db.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Like>> GetAllLikesAsync()
        {
            return await _db.Likes.AsQueryable().ToListAsync();
        }
    }
}
