namespace VictuzWeb.ViewModels;

/// <summary>
/// Viewmodel for an error view.
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    /// Identifier for this request.
    /// </summary>
    public string? RequestIdentifier { get; set; }

    /// <summary>
    /// Should the request Identifier be shown?
    /// </summary>
    public bool ShowRequestIdentifier => !string.IsNullOrEmpty(RequestIdentifier);
}
