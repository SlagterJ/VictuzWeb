using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VictuzWeb.Persistence;
using VictuzWeb.ViewModels;

namespace VictuzWeb.Controllers;

/// <summary>
/// Controller for home views.
/// </summary>
/// <param name="logger">Logger for this controller.</param>
public class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger;

    private readonly VictuzWebDatabaseContext _context;



    public HomeController(ILogger<HomeController> logger, VictuzWebDatabaseContext context)
    {
        _logger = logger;
        _context = context;
    }


    /// <summary>
    /// Index view call.
    /// </summary>
    /// <returns>Index view.</returns>
    public IActionResult Index() => View();

    /// <summary>
    /// Error view call.
    /// </summary>
    /// <returns>Error view.</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() =>
        View(
            new ErrorViewModel { RequestIdentifier = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
}
