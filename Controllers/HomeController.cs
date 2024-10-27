using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VictuzWeb.ViewModels;

namespace VictuzWeb.Controllers;

/// <summary>
/// Controller for home views.
/// </summary>
/// <param name="logger">Logger for this controller.</param>
public class HomeController(ILogger<HomeController> logger) : Controller
{
    /// <summary>
    /// Index view call.
    /// </summary>
    /// <returns>Index view.</returns>
    public IActionResult Index() => View();

    /// <summary>
    /// Privacy view call.
    /// </summary>
    /// <returns>Privacy view.</returns>

    public IActionResult Privacy() => View();

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
