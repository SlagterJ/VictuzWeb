namespace VictuzWeb.Models;

/// <summary>
/// A gathering that is going to happen or has happened. Promoted from suggestion or
/// made on its own.
/// </summary>
public class Gathering : Suggestion
{

    public required bool IsMemberOnly { get; set; }


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

    /// <summary>
    /// Filter a list of deadlines by their begin date, ascending.
    /// </summary>
    /// <param name="gatherings">The list of gatherings to filter</param>
    /// <returns>Filtered list of gatherings</returns>
    public static List<Gathering> OrderyByDateTimeAscending(List<Gathering> gatherings) =>
        gatherings.OrderBy(gathering => gathering.BeginDateTime).ToList();

    /// <summary>
    /// Has the deadline for this gathering passed?
    /// </summary>
    /// <param name="gathering">The gathering to check</param>
    /// <param name="currentDate">The current date to check against</param>
    /// <returns>If the deadline has passed (true) or not (false)</returns>
    public static bool HasDeadlinePassed(Gathering gathering, DateOnly currentDate) =>
        gathering.DeadlineDate <= currentDate;

    /// <summary>
    /// Eliminate gatherings of which the deadline to sign up for has passed.
    /// </summary>
    /// <param name="gatherings">The list of gatherings to filter</param>
    /// <param name="currentDate">The current date to check against</param>
    /// <returns>Filtered list of gatherings</returns>
    public static List<Gathering> EliminateDeadlinePassed(
        List<Gathering> gatherings,
        DateOnly currentDate
    ) => gatherings.Where((gathering) => !HasDeadlinePassed(gathering, currentDate)).ToList();
}
