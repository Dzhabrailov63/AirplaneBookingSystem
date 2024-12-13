using AirplaneBookingSystem.BlazorClient.Dto;
using AirplaneBookingSystem.BlazorClient.Services.Tickets;
using System.Net.Http.Json;
using System.Text.Json;

namespace TicketBookingSystem.BlazorClient.Services.Tickets;

public class TicketService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : ITicketService
{
    private readonly string _apiServerUrl = configuration["ApiServerUrl"] ?? throw new ArgumentNullException("ApiServerUrl section not found");

    public async Task<IEnumerable<TicketGetDto>> GetList()
    {
        using var httpClient = httpClientFactory.CreateClient();
        var getResponse = await httpClient.GetAsync($"{_apiServerUrl}/api/ticket");

        if (!getResponse.IsSuccessStatusCode)
            throw new Exception("Failed to fetch list of tickets");

        var getResponseContent = await getResponse.Content.ReadAsStringAsync();
        var tickets = JsonSerializer.Deserialize<IEnumerable<TicketGetDto>>(getResponseContent) ??
            throw new Exception("Failed to deserialize response content into tickets list");

        return tickets;
    }

    public async Task<TicketGetDto> Get(int id)
    {
        using var httpClient = httpClientFactory.CreateClient();
        var getResponse = await httpClient.GetAsync($"{_apiServerUrl}/api/ticket/{id}");

        if (!getResponse.IsSuccessStatusCode)
            throw new Exception($"Failed to get the ticket with id={id}");

        var getResponseContent = await getResponse.Content.ReadAsStringAsync();
        var ticket = JsonSerializer.Deserialize<TicketGetDto>(getResponseContent) ??
            throw new Exception("Failed to deserialize response content into ticket object");

        return ticket;
    }

    public async Task Put(int id, TicketPostDto ticketPostDto)
    {
        using var httpClient = httpClientFactory.CreateClient();

        var ticketJsonContent = JsonContent.Create(ticketPostDto);
        var putResponse = await httpClient.PutAsync($"{_apiServerUrl}/api/ticket/{id}", ticketJsonContent);

        if (!putResponse.IsSuccessStatusCode)
            throw new Exception($"Failed to put the ticket with id={id}");
    }

    public async Task<int> Post(TicketPostDto ticketPostDto)
    {
        using var httpClient = httpClientFactory.CreateClient();

        var ticketJsonContent = JsonContent.Create(ticketPostDto);
        var postResposne = await httpClient.PostAsync($"{_apiServerUrl}/api/ticket", ticketJsonContent);

        if (!postResposne.IsSuccessStatusCode)
            throw new Exception($"Failed to post new ticket");

        var addedTicketIdString = await postResposne.Content.ReadAsStringAsync();
        return int.Parse(addedTicketIdString);
    }

    public async Task Delete(int id)
    {
        using var httpClient = httpClientFactory.CreateClient();

        var deleteResponse = await httpClient.DeleteAsync($"{_apiServerUrl}/api/ticket/{id}");

        if (!deleteResponse.IsSuccessStatusCode)
            throw new Exception($"Failed to delete the ticket with id={id}");
    }
}
