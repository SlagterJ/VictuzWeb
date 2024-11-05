using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VictuzWeb.Models;

/// <summary>
/// Global base entity for the application, use this to extend every other model.
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Globally unique identifier for this entity.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Identifier { get; set; }

    /// <summary>
    /// The date and time that this entity was created at.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }
}
