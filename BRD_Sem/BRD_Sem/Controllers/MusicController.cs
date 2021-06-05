using System.Linq;
using System.Text.RegularExpressions;
using BRD_Sem.Models;
using Microsoft.AspNetCore.Mvc;

namespace BRD_Sem.Controllers
{
    public class MusicController : Controller
    {
        private readonly ApplicationContext _dbContext;

        public MusicController(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Music()
        {
            return View();
        }

        [Route("~/GetList")]
        public IActionResult GetList()
        {
            return Json(_dbContext.Musics.ToList());
        }

        [HttpPost]
        public IActionResult Post(Music music)
        {
            Regex reg1 = new Regex(@"[A-ZА-Я]?[a-zа-я]+");
            Regex reg2 = new Regex(@"\d\d\.\d\d\.\d\d");
            if (reg1.IsMatch(music.Name))
                if (reg2.IsMatch(music.Date))
                    _dbContext.Musics.Add(music);
            return View("Music");
        }

        [HttpGet]
        public IActionResult SearchByAuthor(string author)
        {
            var musics = _dbContext.Musics.Where(e => e.Author == author).ToList();
            return new JsonResult(musics);
        }
    }
}