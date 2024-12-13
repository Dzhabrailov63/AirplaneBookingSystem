using System.Text.Json.Serialization;

namespace AirplaneBookingSystem.BlazorClient.Dto;

public class TicketPostDto
{
    /// <summary>
    /// Number of ticket
    /// </summary>
    [JsonPropertyName("ticketNumber")]
    public int TicketNumber { get; set; } = 0;

    /// <summary>
    /// Сlient who owns the ticket
    /// </summary>
    [JsonPropertyName("clientId")]
    public int ClientId { get; set; }

    /// <summary>
    /// Flight for which the ticket is registered
    /// </summary>
    [JsonPropertyName("flightId")]
    public int FlightId { get; set; }
}