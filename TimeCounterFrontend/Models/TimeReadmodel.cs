namespace TimeCounterFrontend.Models;

/// <summary>
/// Model to return the time and number of request the server has handled
/// </summary>
public class TimeReadmodel
{
    /// <summary>
    /// Current server time
    /// </summary>
    public DateTime ServerTime { get; set; }

    /// <summary>
    /// The number of request made to the server
    /// </summary>
    public int RequestCounter { get; set; }
}