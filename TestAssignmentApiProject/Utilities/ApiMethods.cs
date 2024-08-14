using System.Net;
using System.Text.Json;
using Microsoft.Playwright;

namespace TestAssignmentApiProject.Utilities;

public class ApiMethods 
{
    private const int RequestTimeoutInMs = 3000;

    private static readonly JsonSerializerOptions JsonSerializerOptions =
        new() { PropertyNameCaseInsensitive = true };

    public static async Task<T> Get<T>(IAPIRequestContext requestContext, string endPoint, APIRequestContextOptions? payLoad = null, int timeout = RequestTimeoutInMs)
    {
        var jsonData = GetJsonStringFromDataObject(payLoad);
        var response = await requestContext.GetAsync(endPoint, new() { DataString = jsonData, Timeout = timeout });

        return await GetDeserializedResponse<T>(response);
    }

    public static async Task<IAPIResponse> Get(IAPIRequestContext requestContext, string endPoint,
        APIRequestContextOptions? payLoad = null, int timeout = RequestTimeoutInMs)
    {
        var jsonData = GetJsonStringFromDataObject(payLoad);
        var response = await requestContext.GetAsync(endPoint, new() { DataString = jsonData, Timeout = timeout });

    

        await LogResponseForNegativeResponses(response);
        return response;
    }

    public static async Task<IAPIResponse> Post(IAPIRequestContext requestContext, string endPoint,
        APIRequestContextOptions? payLoad = null)
    {
        var jsonData = GetJsonStringFromDataObject(payLoad);
        var response = await requestContext.PostAsync(endPoint, new() { DataString = jsonData });

        await LogResponseForNegativeResponses(response);
        return response;
    }

    public static async Task<IAPIResponse> PostForAccessToken(IAPIRequestContext? requestContext, string endPoint,
       IFormData? payLoad = null)
    {
        var response = await requestContext!.PostAsync(endPoint, new() { Form = payLoad });
        await LogResponseForNegativeResponses(response);
        return response;
    }

    public static async Task<T> Post<T>(IAPIRequestContext requestContext, string endPoint,
        APIRequestContextOptions? payLoad = null)
    {
        var jsonData = GetJsonStringFromDataObject(payLoad);
        var response = await requestContext.PostAsync(endPoint, new() { DataString = jsonData });


        return await GetDeserializedResponse<T>(response);
    }

    public static async Task<IAPIResponse> Put(IAPIRequestContext requestContext, string endPoint, APIRequestContextOptions? payLoad = null)
    {
        var jsonData = GetJsonStringFromDataObject(payLoad);
        var response = await requestContext.PutAsync(endPoint, new() { DataString = jsonData });

     

        await LogResponseForNegativeResponses(response);
        return response;
    }

    public static async Task<T> Put<T>(IAPIRequestContext requestContext, string endPoint,
        APIRequestContextOptions? payLoad = null)
    {
        var jsonData = GetJsonStringFromDataObject(payLoad);
        var response = await requestContext.PutAsync(endPoint, new() { DataString = jsonData });

    

        return await GetDeserializedResponse<T>(response);
    }

    public static async Task<IAPIResponse> Delete(IAPIRequestContext requestContext, string endPoint,
        APIRequestContextOptions? payLoad = null)
    {
        var jsonData = GetJsonStringFromDataObject(payLoad);
        var response = await requestContext.DeleteAsync(endPoint, new() { DataString = jsonData });

       

        await LogResponseForNegativeResponses(response);
        return response;
    }

    public static async Task<T> Delete<T>(IAPIRequestContext requestContext, string endPoint,
        APIRequestContextOptions? payLoad = null)
    {
        var jsonData = GetJsonStringFromDataObject(payLoad);
        var response = await requestContext.DeleteAsync(endPoint, new() { DataString = jsonData });

   

        return await GetDeserializedResponse<T>(response);
    }

    public static async Task<T> Patch<T>(IAPIRequestContext requestContext, string endPoint,
        APIRequestContextOptions? payLoad = null)
    {
        var jsonData = GetJsonStringFromDataObject(payLoad);
        var response = await requestContext.PatchAsync(endPoint, new() { DataString = jsonData });

   

        return await GetDeserializedResponse<T>(response);
    }

    public static async Task<IAPIResponse> Patch(IAPIRequestContext requestContext, string endPoint,
        APIRequestContextOptions? payLoad = null)
    {
        var jsonData = GetJsonStringFromDataObject(payLoad);
        var response = await requestContext.PatchAsync(endPoint, new() { DataString = jsonData });


        await LogResponseForNegativeResponses(response);
        return response;
    }

   

    private static string? GetJsonStringFromDataObject(APIRequestContextOptions? data)
    {
        if (data?.DataObject is not null)
        {
            return JsonSerializer.Serialize(data.DataObject, JsonSerializerOptions);
        }

        else if (data?.DataString is not null)
        {
            return data.DataString;
        }

        else if (data?.Data is not null)
        {
            return JsonSerializer.Serialize(data.Data, JsonSerializerOptions);
        }
        else if (data?.DataByte is not null)
        {
            return JsonSerializer.Serialize(data.DataByte, JsonSerializerOptions);
        }

        return null;
    }

    private static async Task<T> GetDeserializedResponse<T>(IAPIResponse response)
    {
        var responseText = await response.TextAsync();
        try { return JsonSerializer.Deserialize<T>(responseText, JsonSerializerOptions)!; }

        catch
        {
            throw new InvalidOperationException(responseText);
        }

    }

    private static async Task LogResponseForNegativeResponses(IAPIResponse response)
    {
        if (response.Status is (int)HttpStatusCode.ServiceUnavailable or (int)HttpStatusCode.BadRequest)
        {
            Console.WriteLine(await response.TextAsync());
        }

    }
}
