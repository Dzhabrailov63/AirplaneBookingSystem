using AirplaneBookingSystem.BlazorClient.Dto;

namespace FlightBookingSystem.BlazorClient.Services.Flights;

public interface IFlightService
{
    public Task<IEnumerable<FlightGetDto>> GetList();
    public Task<FlightGetDto> Get(int id);
    public Task Put(int id, FlightPostDto airplanePostDto);
    public Task<int> Post(FlightPostDto airplanePostDto);
    public Task Delete(int id);
}
