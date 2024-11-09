using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VictuzWeb.Persistence;

namespace VictuzWeb.Controllers;

public class UsersController(VictuzWebDatabaseContext context) : Controller
{
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Members()
    {
        return View(await context.Users
            .Include(u => u.Role)
            .Where(u => u.IsMember == true && u.Role != null && u.Role.Name != "Admin")
            .ToListAsync());
    }
}
