using AirplaneBookingSystem.BlazorClient.Dto;
using System.Net.Http.Json;
using System.Text.Json;

namespace ClientBookingSystem.BlazorClient.Services.Clients;

public class ClientService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IClientService
{
    private readonly string _apiServerUrl = configuration["ApiServerUrl"] ?? throw new ArgumentNullException("ApiServerUrl section not found");

    public async Task<IEnumerable<ClientGetDto>> GetList()
    {
        using var httpClient = httpClientFactory.CreateClient();
        var getResponse = await httpClient.GetAsync($"{_apiServerUrl}/api/client");

        if (!getResponse.IsSuccessStatusCode)
            throw new Exception("Failed to fetch list of clients");

        var getResponseContent = await getResponse.Content.ReadAsStringAsync();
        var clients = JsonSerializer.Deserialize<IEnumerable<ClientGetDto>>(getResponseContent) ??
            throw new Exception("Failed to deserialize response content into clients list");

        return clients;
    }

    public async Task<ClientGetDto> Get(int id)
    {
        using var httpClient = httpClientFactory.CreateClient();
        var getResponse = await httpClient.GetAsync($"{_apiServerUrl}/api/client/{id}");

        if (!getResponse.IsSuccessStatusCode)
            throw new Exception($"Failed to get the client with id={id}");

        var getResponseContent = await getResponse.Content.ReadAsStringAsync();
        var client = JsonSerializer.Deserialize<ClientGetDto>(getResponseContent) ??
            throw new Exception("Failed to deserialize response content into client object");

        return client;
    }

    public async Task Put(int id, ClientPostDto clientPostDto)
    {
        using var httpClient = httpClientFactory.CreateClient();

        var clientJsonContent = JsonContent.Create(clientPostDto);
        var putResponse = await httpClient.PutAsync($"{_apiServerUrl}/api/client/{id}", clientJsonContent);

        if (!putResponse.IsSuccessStatusCode)
            throw new Exception($"Failed to put the client with id={id}");
    }

    public async Task<int> Post(ClientPostDto clientPostDto)
    {
        using var httpClient = httpClientFactory.CreateClient();

        var clientJsonContent = JsonContent.Create(clientPostDto);
        var postResposne = await httpClient.PostAsync($"{_apiServerUrl}/api/client", clientJsonContent);

        if (!postResposne.IsSuccessStatusCode)
            throw new Exception($"Failed to post new client");

        var addedClientIdString = await postResposne.Content.ReadAsStringAsync();
        return int.Parse(addedClientIdString);
    }

    public async Task Delete(int id)
    {
        using var httpClient = httpClientFactory.CreateClient();

        var deleteResponse = await httpClient.DeleteAsync($"{_apiServerUrl}/api/client/{id}");

        if (!deleteResponse.IsSuccessStatusCode)
            throw new Exception($"Failed to delete the client with id={id}");
    }
}
