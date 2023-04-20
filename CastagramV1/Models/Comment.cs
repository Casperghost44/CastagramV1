using System.ComponentModel.DataAnnotations;

namespace CastagramV1.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public User Author { get; set; }
        public int AuthorId { get; set; }
        public string Comments { get; set; }
        public DateTime dateTime { get; set; }
    }
}
