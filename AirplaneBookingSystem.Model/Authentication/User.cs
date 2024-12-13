using System.Security.Cryptography;
using System.Text;

namespace AirplaneBookingSystem.Model.Authentication;

/// <summary>
/// User of AirplaneBookingSystem
/// </summary>
public class User
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Login
    /// </summary>
    public string Login { get; set; } = string.Empty;

    private string _password = string.Empty;
    /// <summary>
    /// Password
    /// </summary>
    public string Password
    {
        get
        {
            return _password;
        }
        set
        {
            var valueBytes = Encoding.UTF8.GetBytes(value);

            var md5 = MD5.Create();
            var valueHashBytes = md5.ComputeHash(valueBytes);

            var valueHashString = Encoding.UTF8.GetString(valueHashBytes);
            _password = valueHashString;
        }
    }

    /// <summary>
    /// Role
    /// </summary>
    public string Role { get; set; } = string.Empty;
}
