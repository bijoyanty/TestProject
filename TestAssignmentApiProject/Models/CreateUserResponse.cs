namespace TestAssignmentApiProject.Models;

public class CreateUserResponse
{
    public required string Name { get; set; }

    public required string Job { get; set; }

    public required string Id { get; set; }

    public required DateTimeOffset CreatedAt { get; set; }
}