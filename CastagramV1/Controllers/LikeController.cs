using CastagramV1.Models;
using CastagramV1.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CastagramV1.Controllers
{
    public class LikeController : Controller
    {
        private readonly ILikeRepository _likeRepository;
        private readonly UserManager<User> _userManager;
        public LikeController(ILikeRepository likeRepository, UserManager<User> userManager)
        {
            _likeRepository = likeRepository;
            _userManager = userManager;
        }
    
        public async Task<IActionResult> Index(int? id)
        {
            var likes = await _likeRepository.GetAllLikesAsync(id);
            return View(likes);
        }


        
        
        
    }
}
