using AirplaneBookingSystem.BlazorClient.Dto;

namespace AirplaneBookingSystem.BlazorClient.Services.Tickets;

public interface ITicketService
{
    public Task<IEnumerable<TicketGetDto>> GetList();
    public Task<TicketGetDto> Get(int id);
    public Task Put(int id, TicketPostDto airplanePostDto);
    public Task<int> Post(TicketPostDto airplanePostDto);
    public Task Delete(int id);
}
