using System.ComponentModel.DataAnnotations;

namespace CastagramV1.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public User Author { get; set; }
        public string AuthorId { get; set; }
        public string Description { get; set; }
        public DateTime dateTime { get; set; }
        public ICollection<Like>? Likes { get; set; }
        public ICollection<Comment>? Comment { get; set; }

    }
}
