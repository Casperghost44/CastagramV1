using CastagramV1.Models;
using CastagramV1.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CastagramV1.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DBContext _db;
        public PostRepository(DBContext db)
        {
            _db = db;
        }
        public async Task AddNewAsync(Post newPost)
        {
            var post = new Post()
            {
                ImagePath = newPost.ImagePath,
                Author = newPost.Author,
                AuthorId = newPost.AuthorId,
                Description = newPost.Description,
                dateTime = newPost.dateTime,
                Likes = newPost.Likes,
                Comment = newPost.Comment
            };
            await _db.Posts.AddAsync(post);
            await _db.SaveChangesAsync();
            
        }

        public async Task DeleteAsync(int? id)
        {
            var entity = await _db.Set<Post>().FirstOrDefaultAsync(e => e.Id == id);
            EntityEntry entityEntry = _db.Entry<Post>(entity);
            entityEntry.State = EntityState.Deleted;

            await _db.SaveChangesAsync(); 
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _db.Posts.AsQueryable()
                .Include(o => o.Likes)
                .ToListAsync();
        }


        public async Task<IEnumerable<Post>> GetAllPostsByUser(string userid)
        {
            return await _db.Posts.AsQueryable()
                .Include(o => o.Likes)
                .Where(e => e.AuthorId == userid)
                .ToListAsync();
        }
        public async Task<Post> GetPostAsync(int? id)
        {
            return await _db.Posts.AsQueryable()
                .Where(e => e.Id == id)
                .Include(o => o.Comment)
                .Include(b => b.Likes)
                .SingleAsync();
        }

        public async Task UpdateAsync(Post newPost)
        {
            var post = await _db.Posts.FirstOrDefaultAsync(e => e.Id == newPost.Id);
            if (post != null)
            {
                post.Description = newPost.Description;
                post.Author = newPost.Author;
                post.AuthorId = newPost.AuthorId;
                post.Comment = newPost.Comment;
                post.dateTime = DateTime.Now;
                await _db.SaveChangesAsync();
            }

        }
    }
}
