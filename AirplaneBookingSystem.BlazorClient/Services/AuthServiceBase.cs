using AirplaneBookingSystem.BlazorClient.Dto.LocalStorage;
using Blazored.LocalStorage;
using System.Text.Json;

namespace AirplaneBookingSystem.BlazorClient.Services;

public abstract class AuthServiceBase(ILocalStorageService localStorageService)
{
    protected virtual async Task<LocalStorageTokenData> GetTokenFromLocalStorage()
    {
        var localStorageTokenDataString = await localStorageService.GetItemAsStringAsync("token") ??
            throw new Exception("Token not found");

        localStorageTokenDataString = localStorageTokenDataString.Replace(@"\u0022", "\"").Remove(0, 1);
        localStorageTokenDataString = localStorageTokenDataString.Remove(localStorageTokenDataString.Length - 1, 1);

        var localStorageTokenData = JsonSerializer.Deserialize<LocalStorageTokenData>(localStorageTokenDataString) ??
            throw new Exception("Failed to deserialize token");

        return localStorageTokenData;
    }
}
