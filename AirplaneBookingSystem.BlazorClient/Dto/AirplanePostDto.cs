using System.Text.Json.Serialization;

namespace AirplaneBookingSystem.BlazorClient.Dto;

public class AirplanePostDto
{
    /// <summary>
    ///  Model of Airplane 
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;
}