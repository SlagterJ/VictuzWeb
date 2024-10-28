namespace VictuzWeb.Models;

/// <summary>
/// A gathering that is going to happen or has happened. Promoted from suggestion or
/// made on its own.
/// </summary>
public class Gathering : Suggestion
{
    /// <summary>
    /// The maximum amount of users that are allowed to attend this gathering.
    /// </summary>
    public required int MaxUsers { get; init; }

    /// <summary>
    /// The users that have been registered for this gathering.
    /// </summary>
    public required IEnumerable<User> RegisteredUsers { get; init; }

    /// <summary>
    /// The deadline to register for this gathering.
    /// </summary>
    public required DateOnly DeadlineDate { get; init; }

    /// <summary>
    /// The begin date and time of this gathering.
    /// </summary>
    public required DateTime BeginDateTime { get; init; }

    /// <summary>
    /// The end date and time of this gathering.
    /// </summary>
    public required DateTime EndDateTime { get; init; }
}
