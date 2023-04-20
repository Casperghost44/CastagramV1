using System.ComponentModel.DataAnnotations;

namespace CastagramV1.Models
{
    public class User {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set;}
        public ICollection<Like> Likes { get; set; }
        public ICollection<User> Subscribers { get; set; }
        
    }
}
