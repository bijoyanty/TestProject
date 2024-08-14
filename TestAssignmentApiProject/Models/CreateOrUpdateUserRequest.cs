namespace TestAssignmentApiProject.Models;

public class CreateOrUpdateUserRequest
{
    public required string Name { get; set; }

    public required string Job { get; set; }
}