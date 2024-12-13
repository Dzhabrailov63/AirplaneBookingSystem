using System.Text.Json.Serialization;

namespace AirplaneBookingSystem.BlazorClient.Dto.LocalStorage;

public class LocalStorageTokenData
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("access_token")]
    public string Token { get; set; } = string.Empty;
}
