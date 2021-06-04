using BRD_Sem.Infrostructure;
using BRD_Sem.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BRD_Sem.Controllers
{
    [Authorize(Roles = "starosta")]
    public class StarostaController : Controller
    {
        private readonly StudentsService _studentsService;

        public StarostaController(StudentsService studentsService)
        {
            _studentsService = studentsService;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var s = _studentsService.Get();
            var model = new StudentsViewModel()
            {
                Students = s
            };
            return View("Index", model);
        }
    }
}