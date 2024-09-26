using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    /// <summary>
    /// The date and time that this entity was created at.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }
}
