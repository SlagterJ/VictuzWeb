namespace VictuzWeb.Models;

/// <summary>
/// A suggestion for a gathering.
/// </summary>
public class Suggestion : Entity
{
    /// <summary>
    /// The name of this gathering.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The description of this gathering.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// The user that suggested this suggestion.
    /// </summary>
    public required uint SuggestedByIdentifier { get; set; }

    /// <summary>
    /// The user that suggested this suggestion.
    /// </summary>
    public User? SuggestedBy { get; set; }


    public string? Image { get; set; }


    public List<User>? Likes { get; set; } = new List<User>();

    //TODO: ToGathering method
}
