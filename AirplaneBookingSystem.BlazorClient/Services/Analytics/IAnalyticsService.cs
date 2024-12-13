using AirplaneBookingSystem.BlazorClient.Dto;

namespace AirplaneBookingSystem.BlazorClient.Services.Analytics;

public interface IAnalyticsService
{
    public Task<List<FlightGetDto>> GetAllFlights();
    public Task<List<ClientGetDto>> GetClientsWithSpecificFlight(uint numberOfFlight);
    public Task<List<FlightGetDto>> GetFlightsWithSpecificDepartureCityAndDate(string departureCity, DateTime departureDate);
    public Task<List<FlightGetDto>> GetTopFiveFlights();
    public Task<List<FlightGetDto>> FlightsWithMaxCountOfClients();
    public Task<Dictionary<string, double>> GetClientsStatisticsFromSpecifiedDepartureCity(string departureCity);

}
