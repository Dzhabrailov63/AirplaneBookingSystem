using AirplaneBookingSystem.BlazorClient.Dto;
using System.Net.Http.Json;
using System.Text.Json;

namespace FlightBookingSystem.BlazorClient.Services.Flights;

public class FlightService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IFlightService
{
    private readonly string _apiServerUrl = configuration["ApiServerUrl"] ?? throw new ArgumentNullException("ApiServerUrl section not found");

    public async Task<IEnumerable<FlightGetDto>> GetList()
    {
        using var httpClient = httpClientFactory.CreateClient();
        var getResponse = await httpClient.GetAsync($"{_apiServerUrl}/api/flight");

        if (!getResponse.IsSuccessStatusCode)
            throw new Exception("Failed to fetch list of flights");

        var getResponseContent = await getResponse.Content.ReadAsStringAsync();
        var flights = JsonSerializer.Deserialize<IEnumerable<FlightGetDto>>(getResponseContent) ??
            throw new Exception("Failed to deserialize response content into flights list");

        return flights;
    }

    public async Task<FlightGetDto> Get(int id)
    {
        using var httpClient = httpClientFactory.CreateClient();
        var getResponse = await httpClient.GetAsync($"{_apiServerUrl}/api/flight/{id}");

        if (!getResponse.IsSuccessStatusCode)
            throw new Exception($"Failed to get the flight with id={id}");

        var getResponseContent = await getResponse.Content.ReadAsStringAsync();
        var flight = JsonSerializer.Deserialize<FlightGetDto>(getResponseContent) ??
            throw new Exception("Failed to deserialize response content into flight object");

        return flight;
    }

    public async Task Put(int id, FlightPostDto flightPostDto)
    {
        using var httpClient = httpClientFactory.CreateClient();

        var flightJsonContent = JsonContent.Create(flightPostDto);
        var putResponse = await httpClient.PutAsync($"{_apiServerUrl}/api/flight/{id}", flightJsonContent);

        if (!putResponse.IsSuccessStatusCode)
            throw new Exception($"Failed to put the flight with id={id}");
    }

    public async Task<int> Post(FlightPostDto flightPostDto)
    {
        using var httpClient = httpClientFactory.CreateClient();

        var flightJsonContent = JsonContent.Create(flightPostDto);
        var postResposne = await httpClient.PostAsync($"{_apiServerUrl}/api/flight", flightJsonContent);

        if (!postResposne.IsSuccessStatusCode)
            throw new Exception($"Failed to post new flight");

        var addedFlightIdString = await postResposne.Content.ReadAsStringAsync();
        return int.Parse(addedFlightIdString);
    }

    public async Task Delete(int id)
    {
        using var httpClient = httpClientFactory.CreateClient();

        var deleteResponse = await httpClient.DeleteAsync($"{_apiServerUrl}/api/flight/{id}");

        if (!deleteResponse.IsSuccessStatusCode)
            throw new Exception($"Failed to delete the flight with id={id}");
    }
}
