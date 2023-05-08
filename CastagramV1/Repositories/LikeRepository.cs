using CastagramV1.Models;
using CastagramV1.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
                Author = newLike.Author,
                AuthorId = newLike.AuthorId,
                dateTime = newLike.dateTime,
                Post = newLike.Post,
                PostId = newLike.PostId
            };
            await _db.Likes.AddAsync(like);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var entity = await _db.Set<Like>().FirstOrDefaultAsync(e => e.Id == id);
            EntityEntry entityEntry = _db.Entry<Like>(entity);
            entityEntry.State = EntityState.Deleted;

            await _db.SaveChangesAsync(); 
        }

        public async Task<IEnumerable<Like>> GetAllLikesAsync(int? id)
        {
            return await _db.Likes.AsQueryable()
                .Where(e => e.PostId == id)
                .ToListAsync();
        }
        public async Task<Like> GetLikeAsync(int? postid, string? userid)
        {
            return await _db.Likes.AsQueryable()
                .Where(e => e.PostId == postid)
                .Where(o => o.AuthorId == userid)
                .SingleOrDefaultAsync();

        }
    }
}
