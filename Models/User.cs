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
    public string Firstname { get; set; }

    /// <summary>
    /// Surname of the user.
    /// </summary>
    public string Surname { get; set; }

    /// <summary>
    /// Birthdate of the user.
    /// </summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// Defines the role, and thus the permissions, of this user.
    /// </summary>
    public Role Role { get; set; }

    /// <summary>
    /// Suggestions suggested by this user.
    /// </summary>
    public IEnumerable<Suggestion> Suggestions { get; set; }

    /// <summary>
    /// The gatherings this user is registered for.
    /// </summary>
    public IEnumerable<Gathering> RegisteredForGatherings { get; set; }
}
