using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BRD_Sem.Infrostructure;
using BRD_Sem.Models;
using BRD_Sem.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BRD_Sem.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminPanelController : Controller
    {
        private readonly CommandService _commandService;
        private readonly ApplicationContext _dbContext;
        private readonly IWebHostEnvironment _appEnvironment;

        public AdminPanelController(CommandService commandService, ApplicationContext dbContext,
            IWebHostEnvironment appEnvironment)
        {
            _commandService = commandService;
            _dbContext = dbContext;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            PostsViewModel model = new PostsViewModel()
            {
                Posts = _dbContext.Posts.ToList()
            };
            return View("AdminPage", model);
        }

        public async Task<IActionResult> Post(string authorName, string description, string category, IFormFile image)
        {
            var ms = new MemoryStream();
            await image.CopyToAsync(ms);
            _dbContext.Posts.Add(new Post()
            {
                AuthorName = authorName,
                Description = description,
                Category = category,
                Image = ms.ToArray()
            });
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult DeletePost(int postId)
        {
            var post = _dbContext.Posts.FirstOrDefault(p => p.Id == postId);
            if (post != null)
            {
                _dbContext.Posts.Remove(post);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}