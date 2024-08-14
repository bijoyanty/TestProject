using Microsoft.Extensions.Configuration;
using TestAssignmentUiProject.Models;

namespace TestAssignmentUiProject.Utilities;

/// <summary>
/// The GetTestConfiguration class is responsible for reading and providing configuration settings
/// from the appsettings file based on the current environment in which the test is being run.
/// </summary>
public class GetTestConfiguration
{
    public static UiConfiguration UiConfiguration = null!;
    public static IConfiguration? Configuration;

    // Property to determine the current environment, defaulting to "Local" if not set.
    public static string Environment => System.Environment.GetEnvironmentVariable( "ASPNETCORE_ENVIRONMENT" ) ?? "Local";

    /// <summary>
    /// Static constructor to initialize the Configuration object by reading the appropriate appsettings file
    /// based on the current environment.
    /// </summary>
    static GetTestConfiguration()
    {
        Configuration = GetConfiguration.ReadAppSettingFile( Environment );
    }

    /// <summary>
    /// Extracts the configuration from the IConfiguration object
    /// </summary>
    public void GetUiConfiguration()
    {
        UiConfiguration = LoadOptions.ExtractDataFromAppSettingFiles( Configuration );
    }
}