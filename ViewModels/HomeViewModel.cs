using VictuzWeb.Models;

namespace VictuzWeb.ViewModels;

public class HomeViewModel
{
    public required IEnumerable<Gathering> Gatherings { get; set; }
}
