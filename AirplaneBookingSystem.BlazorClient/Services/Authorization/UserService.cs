using AirplaneBookingSystem.BlazorClient.Dto;
using AirplaneBookingSystem.BlazorClient.Dto.LocalStorage;
using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace AirplaneBookingSystem.BlazorClient.Services.Authorization;

public class UserService(
    ILocalStorageService localStorageService,
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration)
{
    private readonly string _apiServerUri = configuration["ApiServer:Url"] ?? throw new Exception("Not found ApiServer:Url section");

    public async Task<Guid> GetCurrentUserIdFromJwtToken()
    {
        var localStorageTokenData = await GetTokenFromLocalStorage();
        var token = localStorageTokenData.Token;
        Console.WriteLine(token);

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value ??
            throw new Exception("Failed to get userId claim from jwtToken");

        return Guid.Parse(userIdClaim);
    }

    public async Task<UserDto> GetUser(Guid userId)
    {
        var localStorageTokenData = await GetTokenFromLocalStorage();
        var token = localStorageTokenData.Token;

        using var httpClient = httpClientFactory.CreateClient();
        using var getUserRequest = new HttpRequestMessage(HttpMethod.Get, $"{configuration["ApiServer:Url"]}/api/user/{userId}");
        getUserRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        using var response = await httpClient.SendAsync(getUserRequest);

        var responseContent = await response.Content.ReadAsStringAsync();
        var user = GetContentFromResponseString<UserDto>(responseContent);

        return user;
    }

    public async Task<HttpStatusCode> UpdateUser(UserDto userToUpdate)
    {
        var localStorageTokenData = await GetTokenFromLocalStorage();
        var token = localStorageTokenData.Token;

        using var httpClient = httpClientFactory.CreateClient();

        var userToUpdateJsonContent = JsonContent.Create(userToUpdate);
        using var putUserRequest = new HttpRequestMessage(HttpMethod.Put, $"{configuration["ApiServer:Url"]}/test-service/user");
        putUserRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        putUserRequest.Content = userToUpdateJsonContent;

        using var response = await httpClient.SendAsync(putUserRequest);

        return response.StatusCode;
    }

    protected async Task<LocalStorageTokenData> GetTokenFromLocalStorage()
    {
        var localStorageTokenDataString = await localStorageService.GetItemAsStringAsync("token") ??
            throw new Exception("Token not found");

        localStorageTokenDataString = localStorageTokenDataString.Replace(@"\u0022", "\"").Remove(0, 1);
        localStorageTokenDataString = localStorageTokenDataString.Remove(localStorageTokenDataString.Length - 1, 1);

        var localStorageTokenData = JsonSerializer.Deserialize<LocalStorageTokenData>(localStorageTokenDataString) ??
            throw new Exception("Failed to deserialize token");

        return localStorageTokenData;
    }

    private ContentType GetContentFromResponseString<ContentType>(string responseString) where ContentType : class, new()
    {
        var responseObjectAsJson = JsonDocument.Parse(responseString) ??
            throw new Exception("Failed to deserialize response into json");

        var contentJsonStringRepresentation = responseObjectAsJson.RootElement.GetProperty("content").ToString();

        var content = JsonSerializer.Deserialize<ContentType>(contentJsonStringRepresentation)
            ?? throw new Exception($"Failed to deserialize content section of response into {nameof(ContentType)} object");

        return content;
    }
}
