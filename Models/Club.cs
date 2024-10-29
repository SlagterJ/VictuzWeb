using VictuzWeb.AbstractModels;

namespace VictuzWeb.Models;

/// <summary>
/// A club.
/// </summary>
public class Club : Entity
{
    /// <summary>
    /// Is this club accepted by an admin?
    /// </summary>
    public required bool Accepted { get; init; }

    /// <summary>
    /// The name of the club.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The owner of this club.
    /// </summary>
    public required User Owner { get; init; }
}
