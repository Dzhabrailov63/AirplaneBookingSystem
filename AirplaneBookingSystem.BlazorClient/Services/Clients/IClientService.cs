using AirplaneBookingSystem.BlazorClient.Dto;

namespace ClientBookingSystem.BlazorClient.Services.Clients;

public interface IClientService
{
    public Task<IEnumerable<ClientGetDto>> GetList();
    public Task<ClientGetDto> Get(int id);
    public Task Put(int id, ClientPostDto airplanePostDto);
    public Task<int> Post(ClientPostDto airplanePostDto);
    public Task Delete(int id);
}
