using CastagramV1.Models;
using CastagramV1.Repositories;
using CastagramV1.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CastagramV1.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<User> _userManager;
        private readonly IPostRepository _postRepository;
        public CommentController(ICommentRepository commentRepository, UserManager<User> userManager, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _userManager = userManager;
            _postRepository = postRepository;
        }

        public async Task<IActionResult> Create(int? postid)
        {
            ViewBag.PostId = postid;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Comment comm)
        {
            var CurrUser = await _userManager.GetUserAsync(User);
            var post = await _postRepository.GetPostAsync(comm.PostId);
            var comment = new Comment()
            {
                Author = CurrUser,
                AuthorId = CurrUser.Id,
                PostId = comm.PostId,
                dateTime = DateTime.Now,
                Comments = comm.Comments,
                Post = post

            };
            await _commentRepository.AddAsync(comment);
            return RedirectToAction("Details", "Post", new {id = comment.PostId});

        }

    }
}
