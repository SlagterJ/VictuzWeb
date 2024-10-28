using VictuzWeb.AbstractModels;

namespace VictuzWeb.Models;

/// <summary>
/// Role that represents the permissions a user should have.
/// </summary>
public class Role : Entity
{
    /// <summary>
    /// The name of the role.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The users that have this role.
    /// </summary>
    public required IEnumerable<User> UsersWithRole { get; init; }
}
