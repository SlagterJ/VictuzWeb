namespace VictuzWeb.Models;

/// <summary>
/// Viewmodel for an error view.
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    /// Identifier for this request.
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// Should the request Id be shown?
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
