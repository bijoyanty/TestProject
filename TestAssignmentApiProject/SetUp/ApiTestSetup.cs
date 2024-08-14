using Microsoft.Playwright;
using NUnit.Framework;
using TestAssignmentApiProject.Utilities;

namespace TestAssignmentApiProject.SetUp;

public class ApiTestSetup 
{
    public static string? BaseUrl = "https://reqres.in/";
    private readonly RequestContext _requestContext = new();
    public static IAPIRequestContext ReqresRequestContext { get; private set; } = null!;

    [OneTimeSetUp]
    public async Task TestSetUp()
    {
        ReqresRequestContext = await _requestContext.CreateContext(BaseUrl,null);
    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
        await ReqresRequestContext.DisposeAsync();
    }
}