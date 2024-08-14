namespace TestAssignmentUiProject.Utilities;

/// <summary>
/// PathFinder class is responsible for finding the path to the solution file
/// </summary>
public class PathFinder
{

    /// <summary>
    /// Returns the location of solution file
    /// </summary>
    public static string? UpToSolutionPath(string? path)
    {
        if (path == null)
        {
            return null;
        }

        var maxIterations = 7;

        while (!Directory.EnumerateFiles(path, "*.sln").Any())
        {
            path = Path.GetFullPath(Path.Combine(path, ".."));

            if (maxIterations-- < 0)
            {
                return null;
            }
        }

        return path;
    }
}