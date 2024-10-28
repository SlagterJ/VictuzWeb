using VictuzWeb.AbstractModels;

namespace VictuzWeb.Models;

/// <summary>
/// Class representing a user.
/// </summary>
public class User : Entity
{
    /// <summary>
    /// Firstname of the user.
    /// </summary>
    public required string Firstname { get; init; }

    /// <summary>
    /// Surname of the user.
    /// </summary>
    public required string Surname { get; init; }

    /// <summary>
    /// Birthdate of the user.
    /// </summary>
    public required DateOnly BirthDate { get; init; }

    /// <summary>
    /// Defines the role, and thus the permissions, of this user.
    /// </summary>
    public required Role Role { get; init; }

    /// <summary>
    /// Suggestions suggested by this user.
    /// </summary>
    public required IEnumerable<Suggestion> Suggestions { get; init; }

    /// <summary>
    /// The gatherings this user is registered for.
    /// </summary>
    public required IEnumerable<Gathering> RegisteredForGatherings { get; init; }
}
