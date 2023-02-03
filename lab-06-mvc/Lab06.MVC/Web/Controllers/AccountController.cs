using Lab06.MVC.Core.DTO;
using Lab06.MVC.Core.Interfaces;
using Lab06.MVC.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lab06.MVC.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAuthService _authService;

        public AccountController(ILogger<AccountController> logger, 
            IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        public async Task<ActionResult> Logout()
        {
            await LogoutInternal();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            _logger.LogInformation("The login page is open!");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userAuth = _authService.Authenticate(model.UserName, model.Password);
            if (userAuth == null)
            {
                ModelState.AddModelError("", "There are no user with such username and password");
                _logger.LogInformation("An error occurred during registration. There are no user with such username and password");
                return View(model);
            }
            await Authenticate(model.UserName, userAuth.Role);
            _logger.LogInformation($"A user with an Username: {model.UserName} is logged in!");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            _logger.LogInformation("The register page is open!");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (_authService.Exists(model.UserName))
            {
                ModelState.AddModelError(nameof(model.UserName), "The user with such name alredy exist");
                _logger.LogInformation($"An error occurred during registration. The user with such name {model.UserName} alredy exist");
                return View(model);
            }
            await _authService.AddUser(model.UserName, model.Password);
            await _authService.Add(new UserDTO { DateOfBirth = model.DateOfBirth, PhoneNumber = model.PhoneNumber });
            _logger.LogInformation($"A user with an Username: {model.UserName} has registered!");
            return RedirectToAction("Login");
        }

        public async Task Authenticate(string username, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authPropenties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authPropenties);
        }

        public async Task LogoutInternal()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _logger.LogInformation("The user log out of the site!");
        }
    }
}
