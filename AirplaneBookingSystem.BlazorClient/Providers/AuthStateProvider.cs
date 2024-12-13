using AirplaneBookingSystem.BlazorClient.Dto.LocalStorage;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace BlazorClient.Providers;

public class AuthStateProvider(
    ILocalStorageService localStorageService,
    HttpClient httpClient) : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await localStorageService.GetItemAsStringAsync("token");

        var identity = new ClaimsIdentity();
        httpClient.DefaultRequestHeaders.Authorization = null;

        if (!string.IsNullOrEmpty(token))
        {
            identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
        }

        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);

        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }

    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        jwt = jwt.Replace(@"\u0022", "\"").Remove(0, 1);
        jwt = jwt.Remove(jwt.Length - 1, 1);
        var payload = JsonSerializer.Deserialize<LocalStorageTokenData>(jwt) ??
            throw new Exception("Failed to deserialize token data from local storage");

        var token = payload.Token.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(token);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        return keyValuePairs is null
            ? throw new Exception("KeyValuePairs are null")
            : keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value?.ToString()));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}