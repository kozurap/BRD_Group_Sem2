using System.IO;
using System.Linq;
using BRD_Sem.Infrostructure;
using BRD_Sem.Models;
using BRD_Sem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BRD_Sem.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationContext _dbContext;
        private readonly GetPostImage _getPostImage;

        public NewsController(ApplicationContext dbContext, GetPostImage getPostImage)
        {
            _dbContext = dbContext;
            _getPostImage = getPostImage;
        }

        [Route("~/News")]
        public IActionResult News()
        {
            var posts = _dbContext.Posts.Where(p => p.Category == "News").ToList();
            var model = new PostsViewModel()
            {
                Posts = posts
            };

            return View("News", model);
        }

        [HttpGet("~/{postId}/image")]
        public IActionResult GetImage(int postId)
        {
            var data = _getPostImage.GetImage(postId);
            return new FileStreamResult(new MemoryStream(data), "image/jpg");
        }

        //Ебать, если все одинаково, то просто въеби сам контроллер на каждую такую страницу(я хуй знает какие у нас там),
        //меняя все Sports на другую категорию 
    }
}
