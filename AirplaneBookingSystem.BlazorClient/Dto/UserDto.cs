namespace AirplaneBookingSystem.BlazorClient.Dto;

public class UserDto
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Login
    /// </summary>
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Role
    /// </summary>
    public string Role { get; set; } = string.Empty;
}
