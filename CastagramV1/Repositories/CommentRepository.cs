using CastagramV1.Models;
using CastagramV1.Services;
using Microsoft.EntityFrameworkCore;

namespace CastagramV1.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DBContext _db;
        public CommentRepository(DBContext dB)
        {
            _db = dB;
        }
        public async Task AddAsync(Comment newComment)
        {
            var comment = new Comment()
            {
                Author = newComment.Author,
                AuthorId = newComment.AuthorId,
                Comments = newComment.Comments,
                dateTime = newComment.dateTime,
                Post = newComment.Post,
                PostId = newComment.PostId

            };
            await _db.Comments.AddAsync(comment);
            await _db.SaveChangesAsync();
        }

        public Task DeleteAsync(Comment comment)
        {
            _db.Comments.Remove(comment);
            _db.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync(int? postid)
        {
            return await _db.Comments.AsQueryable()
                .Where(e => e.PostId == postid)
                .ToListAsync();
        }

        public async Task UpdateAsync(Comment comment)
        {
            var com = await _db.Comments.FirstOrDefaultAsync(e => e.Id == comment.Id);
            if (com != null)
            {
                com.Comments = comment.Comments;
                await _db.SaveChangesAsync();
            }
        }
    }
}
