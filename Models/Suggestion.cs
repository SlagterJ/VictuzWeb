﻿using VictuzWeb.AbstractModels;

namespace VictuzWeb.Models;

/// <summary>
/// A suggestion for a gathering.
/// </summary>
public class Suggestion : Entity
{
    /// <summary>
    /// The name of this gathering.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The description of this gathering.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// The user that suggested this suggestion.
    /// </summary>
    public required User SuggestedBy { get; set; }

    //TODO: ToGathering method
}
