using System.Collections;

namespace VictuzWeb.Models;

/// <summary>
/// Class representing a user.
/// </summary>
public class User : Entity
{
    /// <summary>
    /// Firstname of the user.
    /// </summary>
    public required string Firstname { get; set; }

    /// <summary>
    /// Surname of the user.
    /// </summary>
    public required string Surname { get; set; }

    /// <summary>
    /// Birthdate of the user.
    /// </summary>
    public required DateOnly BirthDate { get; set; }


    /// <summary>
    /// The username of the user, used for login.
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// The hashed password of the user.
    /// </summary>
    public required string PasswordHash { get; set; }

    /// <summary>
    /// Defines the role, and thus the permissions, of this user.
    /// </summary>
    public required uint RoleIdentifier { get; set; }

    /// <summary>
    /// Defines the role, and thus the permissions, of this user.
    /// </summary>
    public Role? Role { get; set; }

    /// <summary>
    /// Suggestions suggested by this user.
    /// </summary>
    public required IEnumerable<uint> SuggestionsIdentifiers { get; set; }

    /// <summary>
    /// Suggestions suggested by this user.
    /// </summary>
    public IEnumerable<Suggestion>? Suggestions { get; set; }

    /// <summary>
    /// The clubs that this user is the owner of.
    /// </summary>
    public required IEnumerable<uint> OwnerOfIdentifiers { get; set; }

    /// <summary>
    /// The clubs that this user is the owner of.
    /// </summary>
    public IEnumerable<Club>? OwnerOf { get; set; }

    /// <summary>
    /// The gatherings this user is registered for.
    /// </summary>
    public required IEnumerable<uint> RegisteredForGatheringsIdentifiers { get; set; }

    /// <summary>
    /// The gatherings this user is registered for.
    /// </summary>
    public IEnumerable<Gathering>? RegisteredForGatherings { get; set; }
}
