using VictuzWeb.AbstractModels;

namespace VictuzWeb.Models;

/// <summary>
/// A suggestion for a gathering.
/// </summary>
public class Suggestion : Entity
{
    /// <summary>
    /// The name of this gathering.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The description of this gathering.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The user that suggested this suggestion.
    /// </summary>
    public User SuggestedBy { get; set; }

    //TODO: ToGathering method
}
