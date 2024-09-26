using System.ComponentModel.DataAnnotations;

namespace VictuzWeb.AbstractModels;

/// <summary>
/// Global base entity for the application, use this to extend every other model.
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Globally unique identifier for this entity. Uses UUIDv4.
    /// </summary>
    [Key]
    public Guid Identifier { get; set; }
}
