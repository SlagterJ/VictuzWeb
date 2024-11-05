namespace VictuzWeb.Models;

/// <summary>
/// Role that represents the permissions a user should have.
/// </summary>
public class Role : Entity
{
    /// <summary>
    /// The name of the role.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The users that have this role.
    /// </summary>
    public required IEnumerable<uint> UsersWithRoleIdentifiers { get; set; }

    /// <summary>
    /// The users that have this role.
    /// </summary>
    public IEnumerable<User>? UsersWithRole { get; set; }
}
