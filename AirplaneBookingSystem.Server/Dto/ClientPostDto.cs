namespace AirplaneBookingSystem.Server.Dto;
public class ClientPostDto
{
    /// <summary>
    /// Client`s passport number 
    /// </summary>
    public string PassportNumber { get; set; } = string.Empty;
    /// <summary>
    /// Client`s birthday
    /// </summary>
    public DateTime BirthdayDate { get; set; }
    /// <summary>
    /// Client`s name
    /// </summary>
    public string Name { get; set; } = string.Empty;
}