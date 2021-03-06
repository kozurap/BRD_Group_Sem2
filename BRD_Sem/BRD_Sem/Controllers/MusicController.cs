﻿using System.Linq;
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

        [Route("~/Music/GetList")]
        [HttpGet]
        public IActionResult GetList()
        {
            var list = _dbContext.Musics.ToList();
            return new JsonResult(list);
        }

        [HttpPost]
        public IActionResult Post(Music music)
        {
            Regex reg1 = new Regex(@"[A-ZА-Я]?[a-zа-я]+");
            Regex reg2 = new Regex(@"\d\d\.\d\d\.\d\d");
            var id = _dbContext.Musics.ToList().Count + 1;
            if (reg1.IsMatch(music.Name))
                if (reg2.IsMatch(music.Date))
                    _dbContext.Musics.Add(music);
            _dbContext.SaveChanges();
            return View("Music");
        }

        [HttpGet]
        public IActionResult SearchByAuthor([FromQuery]string author)
        {
            var musics = _dbContext.Musics.Where(e => e.Author == author).ToList();
            return new JsonResult(musics);
        }
    }
}