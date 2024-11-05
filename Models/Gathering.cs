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
    public required uint MaxUsers { get; set; }

    /// <summary>
    /// The users that have been registered for this gathering.
    /// </summary>
    public IEnumerable<User>? RegisteredUsers { get; set; }

    /// <summary>
    /// The deadline to register for this gathering.
    /// </summary>
    public required DateOnly DeadlineDate { get; set; }

    /// <summary>
    /// The begin date and time of this gathering.
    /// </summary>
    public required DateTime BeginDateTime { get; set; }

    /// <summary>
    /// The end date and time of this gathering.
    /// </summary>
    public required DateTime EndDateTime { get; set; }
}
