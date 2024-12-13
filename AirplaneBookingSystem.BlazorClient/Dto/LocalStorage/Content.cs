using System.Text.Json.Serialization;

namespace AirplaneBookingSystem.BlazorClient.Dto.LocalStorage;

public class Content
{
    /// <summary>
    /// Token from local storage
    /// </summary>
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
}
