using BRD_Sem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BRD_Sem.Models.ViewModels;
using BRD_Sem.Infrostructure;
using System.IO;

namespace BRD_Sem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _dbContext;
        private readonly GetPostImage _getPostImage;

        public HomeController(ApplicationContext dbContext, GetPostImage getPostImage, ILogger<HomeController> logger)
        {
            _dbContext = dbContext;
            _getPostImage = getPostImage;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var posts = _dbContext.Posts.ToList();
            var model = new PostsViewModel()
            {
                Posts = posts
            };

            return View("Index", model);
        }

        [HttpGet("~/{postId}/image")]
        public IActionResult GetImage(int postId)
        {
            var data = _getPostImage.GetImage(postId);
            return new FileStreamResult(new MemoryStream(data), "image/jpg");
        }

        public IActionResult ContentPage()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
