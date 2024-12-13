using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AirplaneBookingSystem.Model.Authentication;

public class AuthOptions
{
    public const string Issuer = "AirplaneBookingSystem.Server";
    public const string Audience = "AirplaneBookingSystem.BlazorClient";
    private const string Key = "somesecretKeyThatShouldBeInAppsettingsOrSecrets";
    public const int Lifetime = 3600;

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }


}
