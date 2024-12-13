using System.Text.Json.Serialization;

namespace AirplaneBookingSystem.BlazorClient.Dto;

public class AirplaneGetDto
{
    /// <summary>
    /// Unique Id of Airplane 
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    ///  Model of Airplane 
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;
}