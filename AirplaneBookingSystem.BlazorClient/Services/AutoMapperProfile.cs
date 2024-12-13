using AirplaneBookingSystem.BlazorClient.Dto;

namespace AirplaneBookingSystem.BlazorClient.Services;

public class AutoMapperProfile : AutoMapper.Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AirplaneGetDto, AirplanePostDto>().ReverseMap();
        CreateMap<TicketGetDto, TicketPostDto>().ReverseMap();
        CreateMap<FlightGetDto, FlightPostDto>().ReverseMap();
        CreateMap<ClientGetDto, ClientPostDto>().ReverseMap();
    }
}
