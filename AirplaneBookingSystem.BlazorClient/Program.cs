using AirplaneBookingSystem.BlazorClient;
using AirplaneBookingSystem.BlazorClient.Services;
using AirplaneBookingSystem.BlazorClient.Services.Airplanes;
using AirplaneBookingSystem.BlazorClient.Services.Analytics;
using AirplaneBookingSystem.BlazorClient.Services.Authorization;
using AirplaneBookingSystem.BlazorClient.Services.Tickets;
using AutoMapper;
using Blazor.SubtleCrypto;
using BlazorClient.Providers;
using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using ClientBookingSystem.BlazorClient.Services.Clients;
using FlightBookingSystem.BlazorClient.Services.Flights;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TicketBookingSystem.BlazorClient.Services.Tickets;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var mapperConfiguration = new MapperConfiguration(configuration =>
{
    configuration.AddProfile(new AutoMapperProfile());
});

var mapper = mapperConfiguration.CreateMapper();

builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IAirplaneService, AirplaneService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<UserService>();

builder.Services.AddSubtleCrypto(options =>
    options.Key = "somesecretKeyThatShouldBeInAppsettingsOrSecrets"
);

builder.Services.AddHttpClient();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();
builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();

await builder.Build().RunAsync();
