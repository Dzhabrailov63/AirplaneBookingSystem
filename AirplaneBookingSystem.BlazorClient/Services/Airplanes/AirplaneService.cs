using AirplaneBookingSystem.BlazorClient.Dto;
using System.Net.Http.Json;
using System.Text.Json;

namespace AirplaneBookingSystem.BlazorClient.Services.Airplanes;

public class AirplaneService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IAirplaneService
{
    private readonly string _apiServerUrl = configuration["ApiServerUrl"] ?? throw new ArgumentNullException("ApiServerUrl section not found");

    public async Task<IEnumerable<AirplaneGetDto>> GetList()
    {
        using var httpClient = httpClientFactory.CreateClient();
        var getResponse = await httpClient.GetAsync($"{_apiServerUrl}/api/airplane");

        if (!getResponse.IsSuccessStatusCode)
            throw new Exception("Failed to fetch list of airplanes");

        var getResponseContent = await getResponse.Content.ReadAsStringAsync();
        var airplanes = JsonSerializer.Deserialize<IEnumerable<AirplaneGetDto>>(getResponseContent) ??
            throw new Exception("Failed to deserialize response content into airplanes list");

        return airplanes;
    }

    public async Task<AirplaneGetDto> Get(int id)
    {
        using var httpClient = httpClientFactory.CreateClient();
        var getResponse = await httpClient.GetAsync($"{_apiServerUrl}/api/airplane/{id}");

        if (!getResponse.IsSuccessStatusCode)
            throw new Exception($"Failed to get the airplane with id={id}");

        var getResponseContent = await getResponse.Content.ReadAsStringAsync();
        var airplane = JsonSerializer.Deserialize<AirplaneGetDto>(getResponseContent) ??
            throw new Exception("Failed to deserialize response content into airplane object");

        return airplane;
    }

    public async Task Put(int id, AirplanePostDto airplanePostDto)
    {
        using var httpClient = httpClientFactory.CreateClient();

        var airplaneJsonContent = JsonContent.Create(airplanePostDto);
        var putResponse = await httpClient.PutAsync($"{_apiServerUrl}/api/airplane/{id}", airplaneJsonContent);

        if (!putResponse.IsSuccessStatusCode)
            throw new Exception($"Failed to put the airplane with id={id}");
    }

    public async Task<int> Post(AirplanePostDto airplanePostDto)
    {
        using var httpClient = httpClientFactory.CreateClient();

        var airplaneJsonContent = JsonContent.Create(airplanePostDto);
        var postResposne = await httpClient.PostAsync($"{_apiServerUrl}/api/airplane", airplaneJsonContent);

        if (!postResposne.IsSuccessStatusCode)
            throw new Exception($"Failed to post new airplane");

        var addedAirplaneIdString = await postResposne.Content.ReadAsStringAsync();
        return int.Parse(addedAirplaneIdString);
    }

    public async Task Delete(int id)
    {
        using var httpClient = httpClientFactory.CreateClient();

        var deleteResponse = await httpClient.DeleteAsync($"{_apiServerUrl}/api/airplane/{id}");

        if (!deleteResponse.IsSuccessStatusCode)
            throw new Exception($"Failed to delete the airplane with id={id}");
    }
}
