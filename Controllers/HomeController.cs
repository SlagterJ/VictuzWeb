using System.Diagnostics;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using VictuzWeb.Models;
using VictuzWeb.Persistence;
using VictuzWeb.ViewModels;

namespace VictuzWeb.Controllers;

/// <summary>
/// Controller for home views.
/// </summary>
public class HomeController(VictuzWebDatabaseContext context) : Controller
{
    /// <summary>
    /// Index view call.
    /// </summary>
    /// <returns>Index view.</returns>
    public IActionResult Index()
    {
        var gatheringsUnsorted = context.Gatherings.ToList();

        var gatheringsUnfiltered = Gathering.OrderyByDateTimeAscending(gatheringsUnsorted);

        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        var gatheringsFull = Gathering.EliminateDeadlinePassed(gatheringsUnfiltered, currentDate);

        // discard elements after the sixth
        var gatherings = gatheringsFull.Count >= 6 ? gatheringsFull.GetRange(0, 6) : gatheringsFull;

        var viewModel = new HomeViewModel() { Gatherings = gatherings };
        return View(viewModel);
    }

    /// <summary>
    /// Error view call.
    /// </summary>
    /// <returns>Error view.</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() =>
        View(
            new ErrorViewModel
            {
                RequestIdentifier = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            }
        );
}
