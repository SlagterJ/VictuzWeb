namespace VictuzWeb.Models;

/// <summary>
/// A club.
/// </summary>
public class Club : Entity
{
    /// <summary>
    /// Is this club accepted by an admin?
    /// </summary>
    public required bool Accepted { get; set; }

    /// <summary>
    /// The name of the club.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The owner of this club.
    /// </summary>
    public required uint OwnerIdentifier { get; set; }


    public string? Image { get; set; }

    /// <summary>
    /// The owner of this club.
    /// </summary>
    public User? Owner { get; set; }
}
