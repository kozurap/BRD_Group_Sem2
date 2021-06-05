using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BRD_Sem.Infrostructure;
using BRD_Sem.Models;
using BRD_Sem.Models.ViewModels.AccountModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BRD_Sem.Controllers
{

    public class AccountController : Controller
    {
        private readonly ApplicationContext _dbContext;
        private readonly IEmailSender _sender;
        private readonly EmailConfirmationService _confirmationService;
        private readonly AuthenticationService _authenticationService;

        public AccountController(ILogger<AccountController> logger, ApplicationContext dbContext,
            IEmailSender sender, EmailConfirmationService confirmationService, 
            AuthenticationService authenticationService)
        {
            _dbContext = dbContext;
            _sender = sender;
            _confirmationService = confirmationService;
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _dbContext.Users.FirstOrDefaultAsync(u =>
                    u.Email == model.Email && u.HashedPassword == HashPassword(model.Password));
                if (user == null)
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
                else if (!user.IsConfirmed)
                {
                    ModelState.AddModelError("", "Email не подтвержден");
                }
                else
                {
                    await _authenticationService.Authenticate(user,model.RememberMe != null);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                {
                    
                }
                User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    user = new User
                    {
                        Email = model.Email,
                        Name = model.Name,
                        Surname = model.Surname,
                        HashedPassword = HashPassword(model.Password),
                        IsConfirmed = false,
                        Role = "user",
                        Image = System.IO.File.ReadAllBytes("wwwroot/DefaultImage.jpg")
                    };
                    _dbContext.Users.Add(user);
                    await _dbContext.SaveChangesAsync();

                    return Redirect($"{Url.Action("ConfirmEmail")}?userId={user.Id}");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }

            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return RedirectToAction("Register");

            if (user.IsConfirmed)
                return RedirectToAction("Index", "Home"); 
            
            var key = _confirmationService.GenerateEmailConfirmationToken(user.Id);
            var success = await _sender.SendEmailAsync(user.Email, "Подтверждение Email",
                $"Перейдите по ссылке для окончания регистрации: \n {Url.Action("EmailConfirmationEnd", "Account", null, Request.Scheme)}?key={key}&userId={userId}");
            if (!success)
                ModelState.AddModelError("", $"Письмо не может быть отправлено, т.к оно заблокированно по подозрению в спаме.\n {Url.Action("EmailConfirmationEnd", "Account", null, Request.Scheme)}?key={key}&userId={userId}");
            
            return View(model: user.Email);
        }

        public async Task<IActionResult> EmailConfirmationEnd(string key, int userId)
        {
            var actualUserId = _confirmationService.ConfirmEmail(key);
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null && actualUserId == userId)
            {
                user.IsConfirmed = true;
                await _dbContext.SaveChangesAsync();
                await _authenticationService.Authenticate(user,false);
            }
            else ModelState.AddModelError("","Your token is expired. Try again");

            return View(userId);
        }

        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout();
            return RedirectToAction("Login");
        }
        

        public static string HashPassword(string password)
        {
            var hashBuilder = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                var result = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                foreach (var b in result)
                    hashBuilder.Append(b.ToString("x2"));
            }

            return hashBuilder.ToString();
        }
    }

}