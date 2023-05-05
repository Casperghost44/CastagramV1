using CastagramV1.Models;
using CastagramV1.Repositories;
using CastagramV1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace CastagramV1.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly DBContext _db;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILikeRepository _likeRepository;
        private readonly ICommentRepository _commentRepository;

        public PostController(IPostRepository postRepository, DBContext db, UserManager<User> userManager, IWebHostEnvironment webHostEnvironment, ILikeRepository likeRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _db = db;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _likeRepository = likeRepository;
            _commentRepository = commentRepository;

        }

        [AllowAnonymous]

        public async Task<IActionResult> Index()
        {
            var posts = await _postRepository.GetAllPostsAsync();
           
            return View(posts);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Search(string searchString)
        {
            var Posts = await _postRepository.GetAllPostsAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResultNew = Posts.Where(p => p.Description.Contains("#" + searchString)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Post post, IFormFile file)
        {
            var CurrUser = await _userManager.GetUserAsync(User);
            var imagePath = await SaveImageAsync(file);

            var newPost = new Post()
            {
                ImagePath = imagePath,
                Author = CurrUser,
                AuthorId = CurrUser.Id,
                dateTime = DateTime.Now,
                Description = post.Description
            };

            await _postRepository.AddNewAsync(newPost);

            return RedirectToAction(nameof(Index));
        }

        private async Task<string> SaveImageAsync(IFormFile image)
        {
            var uploadsFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return "/uploads/" + uniqueFileName;
        }

        public async Task<IActionResult> Posts()
        {
            var CurrUser = await _userManager.GetUserAsync(User);
            var list = await _postRepository.GetAllPostsByUser(CurrUser.Id);
            return View(list);
        }

        public async Task<IActionResult> OtherPosts(string userid)
        {
            var list = await _postRepository.GetAllPostsByUser(userid);
            return View(list);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            var post = await _postRepository.GetPostAsync(id);
            var comments = await _commentRepository.GetAllCommentsAsync(id);
            
            return View(post);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var post = await _postRepository.GetPostAsync(id);
            var CurrUser = await _userManager.GetUserAsync(User);
            var editPost = new Post()
            {
                ImagePath = post.ImagePath,
                Author = CurrUser,
                AuthorId = CurrUser.Id,
                dateTime = post.dateTime,
                Comment = post.Comment,
                Description = post.Description,
                Likes = post.Likes,
            };
            return View(editPost);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            var Editpost = await _postRepository.GetPostAsync(id);
            Editpost.Description = post.Description;
            await _postRepository.UpdateAsync(Editpost);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var post = await _postRepository.GetPostAsync(id);
            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            await _postRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddLike(int? id)
        {
            var post = await _postRepository.GetPostAsync(id);
            var CurrUser = await _userManager.GetUserAsync(User);
            var CurrLike = await _likeRepository.GetLikeAsync(id, CurrUser.Id);
            if(CurrLike == null)
            {
                var like = new Like()
                {
                    Post = post,
                    PostId = post.Id,
                    dateTime = DateTime.Now,
                    Author = CurrUser,
                    AuthorId = CurrUser.Id
                };
                await _likeRepository.AddAsync(like);
            }
            else
            {
                await _likeRepository.DeleteAsync(CurrLike.Id);
            }
            
            return RedirectToAction(nameof(Index));
        }


    }
}
