namespace TestAssignmentUiProject.Models;

public class PlaywrightConfig
{
    public required string BrowserName { get; set; }

    public required float? ExpectTimeout { get; set; }

    public required LaunchOptions LaunchOptions { get; set; }
}