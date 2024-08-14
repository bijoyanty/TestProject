using Microsoft.Extensions.Configuration;

namespace TestAssignmentUiProject.Utilities;

/// <summary>
/// The GetConfiguration class provides methods to read configuration settings from appsettings files.
/// It supports loading different configurations based on the environment (e.g., Development, Test).
/// </summary>
public class GetConfiguration
{
    /// <summary>
    /// Reads the appsettings files based on the specified environment and returns the configuration as an IConfigurationRoot object.
    /// </summary>
    /// <param name="environment">The current environment (e.g., "Development", "Test").</param>
    /// <returns>IConfigurationRoot object containing the merged configuration settings.</returns>
    /// <exception cref="Exception">
    /// Thrown if the main appsettings.json file is not found
    /// </exception>
    public static IConfigurationRoot ReadAppSettingFile(string environment)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath( Directory.GetCurrentDirectory() )
            .AddJsonFile( "appsettings.json", optional: false, reloadOnChange: true )
            .AddJsonFile( $"appsettings.{environment}.json", optional: true, reloadOnChange: true );
        try
        {
            var configuration = builder.Build();
            return configuration;
        }
        catch (FileNotFoundException ex)
        {
            throw new Exception( "No appsettings.json found! Make sure a file called 'appsettings.json' is present in the " +
                                 "same folder as your project's csproj file, NOT the solution file. Optionally you can add " +
                                 "appsettings.YourPreferedEnvironment.json, for example Development or Test.", ex );
        }
    }
}