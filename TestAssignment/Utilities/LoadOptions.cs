using Microsoft.Extensions.Configuration;
using TestAssignmentUiProject.Models;

namespace TestAssignmentUiProject.Utilities;

/// <summary>
/// The LoadOptions class provides methods for retrieving the configuration from specified IConfiguration based on the class provided
/// </summary>
public class LoadOptions
{
    /// <summary>
    /// Returns a section from the IConfiguration based on the specified class
    /// </summary>
    public static T LoadRequiredOptions<T>(IConfiguration? configuration, string? name = null)
    {
        name ??= typeof(T).Name;

        return (configuration ?? throw new InvalidOperationException())
               .GetRequiredSection( name )
               .Get<T>() ??
               throw new InvalidOperationException( $"Section '{name}' expected in config holding type {typeof(T)}" );
    }

    /// <summary>
    /// Returns an instance of class UiConfiguration based on specified IConfiguration
    /// </summary>
    public static UiConfiguration ExtractDataFromAppSettingFiles(IConfiguration? configuration)
    {
        return new UiConfiguration()
        {
            Credentials = LoadRequiredOptions<Credentials>( configuration ),
            PlaywrightConfig = LoadRequiredOptions<PlaywrightConfig>( configuration ),
            TestUrls = LoadRequiredOptions<TestUrls>( configuration, "TestUrls")
        };
    }
}