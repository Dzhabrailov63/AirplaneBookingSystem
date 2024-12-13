using System.Text.Json.Serialization;

namespace AirplaneBookingSystem.BlazorClient.Dto;

public class FlightGetDto
{
    /// <summary>
    /// Unique Id of flight
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; } = 0;

    /// <summary>
    /// Number of flight
    /// </summary>
    [JsonPropertyName("numberOfFlight")]
    public int NumberOfFlight { get; set; } = 0;

    /// <summary>
    /// Departure city
    /// </summary>
    [JsonPropertyName("departureCity")]
    public string DepartureCity { get; set; } = string.Empty;

    /// <summary>
    /// Arrival city
    /// </summary>
    [JsonPropertyName("arrivalCity")]
    public string ArrivalCity { get; set; } = string.Empty;

    /// <summary>
    /// Departure date
    /// </summary>
    [JsonPropertyName("departureDate")]
    public DateTime DepartureDate { get; set; }

    /// <summary>
    /// Arrival date
    /// </summary>
    [JsonPropertyName("arrivalDate")]
    public DateTime ArrivalDate { get; set; }

    /// <summary>
    /// Airplane`s id of flight
    /// </summary>
    [JsonPropertyName("airplaneId")]
    public int AirplaneId { get; set; }
}