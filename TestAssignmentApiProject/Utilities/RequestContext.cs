using Microsoft.Playwright;

namespace TestAssignmentApiProject.Utilities;

public class RequestContext
{
    private Dictionary<string, string?> GetHeader()
    {
        var headers = new Dictionary<string, string?> { { "Content-Type", System.Net.Mime.MediaTypeNames.Application.Json } };


        return headers;
    }

    public async Task<IAPIRequestContext> CreateContext(string? baseUrl, int? timeOut)
    {
        var playwright = await Playwright.CreateAsync();
        var headers = GetHeader();

        var request = await playwright.APIRequest.NewContextAsync( new()
        {
            // All requests we send go to this API endpoint.
            BaseURL = baseUrl,
            ExtraHTTPHeaders = headers!,
            Timeout = timeOut
        } );

        return request;
    }
}