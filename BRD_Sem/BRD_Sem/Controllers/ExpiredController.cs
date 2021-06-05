using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BRD_Sem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BRD_Sem.Controllers
{
    public class ExpiredController: Controller
    {
        private readonly ApplicationContext _dbContext;

        public ExpiredController(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Expired()
        {
            return View();
        }

        [Route("~/GetList")]
        [HttpGet]
        public IActionResult GetList()
        {
            var expiredList = _dbContext.Expireds.ToList();
            return new JsonResult(expiredList);
        }

        [HttpPost]
        public IActionResult Post(Expired expired)
        {
            Regex reg = new Regex(@"[A-ZА-Я]?[a-zа-я]+");
            expired.Id = _dbContext.Expireds.ToList().Count + 1;
            if (reg.IsMatch(expired.Name))
                _dbContext.Expireds.Add(expired);
            return View("Expired");
        }
        
        [HttpGet]
        public IActionResult SearchByProfessor(string professor)
        {
            var expireds = _dbContext.Expireds.Where(e => e.ProfessorName == professor).ToList();
            return new JsonResult(expireds);
        }
    }
}