using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CastagramV1.Models
{
    public class User : IdentityUser {


        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        public string? ProfileImage { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Comment>? Comments { get; set;}
        public ICollection<Like>? Likes { get; set; }
        
    }
}
