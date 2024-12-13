using AirplaneBookingSystem.BlazorClient.Dto;
using Blazored.LocalStorage;
using Blazorise.Extensions;
using System.Text.Json;

namespace AirplaneBookingSystem.BlazorClient.Services.Analytics;

public class AnalyticsService(
    ILocalStorageService localStorageService,
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration) : AuthServiceBase(localStorageService), IAnalyticsService
{
    private readonly string _apiServerUrl = configuration["ApiServerUrl"] ?? throw new ArgumentNullException("ApiServerUrl section not found");

    public async Task<List<FlightGetDto>> FlightsWithMaxCountOfClients()
    {
        var tokenData = await base.GetTokenFromLocalStorage();

        using var httpClient = httpClientFactory.CreateClient();
        var getRequest = new HttpRequestMessage(HttpMethod.Get, $"{_apiServerUrl}/api/analytics/flight-with-max-amount-of-clients");
        getRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenData.Token);

        using var getResponse = await httpClient.SendAsync(getRequest);
        if (!getResponse.IsSuccessStatusCode)
            throw new Exception("Failed to get flights with max count of clients");

        var getResponseContent = await getResponse.Content.ReadAsStringAsync();
        var flights = JsonSerializer.Deserialize<List<FlightGetDto>>(getResponseContent) ??
            throw new Exception("Failed to deserialize response into flights with max count of clients");

        return flights;
    }

    public async Task<List<FlightGetDto>> GetAllFlights()
    {
        var tokenData = await base.GetTokenFromLocalStorage();

        using var httpClient = httpClientFactory.CreateClient();

        var getRequest = new HttpRequestMessage(HttpMethod.Get, $"{_apiServerUrl}/api/analytics/all-flight");
        getRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenData.Token);

        using var getResponse = await httpClient.SendAsync(getRequest);

        if (!getResponse.IsSuccessStatusCode)
            throw new Exception("Failed to get flights with max count of clients");

        var getResponseContent = await getResponse.Content.ReadAsStringAsync();
        var flights = JsonSerializer.Deserialize<List<FlightGetDto>>(getResponseContent) ??
            throw new Exception("Failed to deserialize response into flights with max count of clients");

        return flights;
    }

    public async Task<Dictionary<string, double>> GetClientsStatisticsFromSpecifiedDepartureCity(string departureCity)
    {
        var tokenData = await base.GetTokenFromLocalStorage();

        using var httpClient = httpClientFactory.CreateClient();

        var getRequest = new HttpRequestMessage(HttpMethod.Get, $"{_apiServerUrl}/api/Analytics/clients-statistics-from-flights-with-specific-departure-city?" +
            $"departureCity={departureCity}");
        getRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenData.Token);

        using var getResponse = await httpClient.SendAsync(getRequest);

        if (!getResponse.IsSuccessStatusCode)
            throw new Exception("Failed to get flights with max count of clients");

        var getResponseContent = await getResponse.Content.ReadAsStringAsync();
        var statistics = JsonSerializer.Deserialize<Dictionary<string, double>>(getResponseContent) ??
            throw new Exception("Failed to deserialize clients statistics response into dictionary object");

        return statistics;
    }

    public async Task<List<ClientGetDto>> GetClientsWithSpecificFlight(uint numberOfFlight)
    {
        var tokenData = await base.GetTokenFromLocalStorage();

        using var httpClient = httpClientFactory.CreateClient();

        var getRequest = new HttpRequestMessage(HttpMethod.Get, $"{_apiServerUrl}/api/analytics/" +
            $"clients-with-specific-flight?numberOfFlight={numberOfFlight}");
        getRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenData.Token);

        using var getResponse = await httpClient.SendAsync(getRequest);

        if (!getResponse.IsSuccessStatusCode)
            throw new Exception("Failed to get flights with max count of clients");

        var getResponseContent = await getResponse.Content.ReadAsStringAsync();
        var clients = JsonSerializer.Deserialize<List<ClientGetDto>>(getResponseContent) ??
            throw new Exception("Failed to deserialize clients statistics response into dictionary object");

        return clients;
    }

    public async Task<List<FlightGetDto>> GetFlightsWithSpecificDepartureCityAndDate(string departureCity, DateTime departureDate)
    {
        var localDepartureDate = departureDate.ToCultureInvariantString();
        var tokenData = await base.GetTokenFromLocalStorage();

        using var httpClient = httpClientFactory.CreateClient();
        var getRequest = new HttpRequestMessage(HttpMethod.Get, $"{_apiServerUrl}" +
            $"/api/Analytics/flight-with-specific-departure-city-and-data?" +
            $"departureCity={departureCity}&" +
            $"departureData={localDepartureDate}");
        getRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenData.Token);

        using var getResponse = await httpClient.SendAsync(getRequest);

        if (!getResponse.IsSuccessStatusCode)
            throw new Exception("Failed to get flights with specific departure city and date");

        var getResponseContent = await getResponse.Content.ReadAsStringAsync();
        var flights = JsonSerializer.Deserialize<List<FlightGetDto>>(getResponseContent) ??
            throw new Exception("Failed to deserialize response into flights with specific departure city and date");

        return flights;
    }

    public async Task<List<FlightGetDto>> GetTopFiveFlights()
    {
        var tokenData = await base.GetTokenFromLocalStorage();

        using var httpClient = httpClientFactory.CreateClient();
        var getRequest = new HttpRequestMessage(HttpMethod.Get, $"{_apiServerUrl}/api/analytics/top-five-flight");
        getRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenData.Token);

        using var getResponse = await httpClient.SendAsync(getRequest);

        if (!getResponse.IsSuccessStatusCode)
            throw new Exception("Failed to get flights with specific departure city and date");

        var getResponseContent = await getResponse.Content.ReadAsStringAsync();
        var flights = JsonSerializer.Deserialize<List<FlightGetDto>>(getResponseContent) ??
            throw new Exception("Failed to deserialize response into flights with specific departure city and date");

        return flights;
    }
}
