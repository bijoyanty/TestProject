namespace TestAssignmentUiProject.Models;

public class UiConfiguration
{
    public required Credentials Credentials { get; set; }

    public required PlaywrightConfig PlaywrightConfig { get; set; }

    public required TestUrls TestUrls { get; set; }
}