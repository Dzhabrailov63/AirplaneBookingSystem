using AirplaneBookingSystem.BlazorClient.Dto;

namespace AirplaneBookingSystem.BlazorClient.Services.Airplanes;

public interface IAirplaneService
{
    public Task<IEnumerable<AirplaneGetDto>> GetList();
    public Task<AirplaneGetDto> Get(int id);
    public Task Put(int id, AirplanePostDto airplanePostDto);
    public Task<int> Post(AirplanePostDto airplanePostDto);
    public Task Delete(int id);
}
