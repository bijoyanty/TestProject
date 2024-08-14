namespace TestAssignmentApiProject.Models;

public class UpdateUserResponse
{
    public required string Name { get; set; }

    public required string Job { get; set; }

    public required DateTimeOffset UpdatedAt { get; set; }
}