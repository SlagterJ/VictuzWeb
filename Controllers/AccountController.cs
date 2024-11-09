using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.CodeAnalysis.Scripting;
using VictuzWeb.ViewModels;
using VictuzWeb.Persistence;
using Org.BouncyCastle.Crypto.Generators;
using Microsoft.EntityFrameworkCore;
using VictuzWeb.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using LanguageExt;

public class AccountController : Controller
{
    private readonly VictuzWebDatabaseContext _context;

    public AccountController(VictuzWebDatabaseContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _context.Users.Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.Username == model.Username);

            if (user != null && model.Password == user.PasswordHash) // moet nog hash later toevoegen
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Identifier.ToString()),
                    new Claim(CustomClaimTypes.Member, user.IsMember.ToString())
                };


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
        }
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }


    public async Task<IActionResult> Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([Bind("Firstname,Surname,BirthDate,Username,PasswordHash")] User User)
    {
        var role = await _context.Roles.FirstAsync(x => x.Identifier == 1);
        User.Role = role;
        User.IsMember = false;

        if (ModelState.IsValid)
        {
            _context.Add(User);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Login));
        }
        return View(nameof(Register));
    }
}
