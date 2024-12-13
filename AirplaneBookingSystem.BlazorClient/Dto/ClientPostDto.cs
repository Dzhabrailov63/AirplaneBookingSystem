using System.Text.Json.Serialization;

namespace AirplaneBookingSystem.BlazorClient.Dto;

public class ClientPostDto
{
    /// <summary>
    /// Client`s passport number 
    /// </summary>
    [JsonPropertyName("passportNumber")]
    public string PassportNumber { get; set; } = string.Empty;

    /// <summary>
    /// Client`s birthday
    /// </summary>
    [JsonPropertyName("birthdayDate")]
    public DateTime BirthdayDate { get; set; }

    /// <summary>
    /// Client`s name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}