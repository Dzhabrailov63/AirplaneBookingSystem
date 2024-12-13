using AirplaneBookingSystem.Model;
using AirplaneBookingSystem.Model.Authentication;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AirplaneBookingSystem.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IDbContextFactory<AirplaneBookingSystemDbContext> _contextFactory;
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger;

    public UserController(IDbContextFactory<AirplaneBookingSystemDbContext> contextFactory, IMapper mapper, ILogger<UserController> logger)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost("/token")]
    public async Task<IActionResult> Token(User user)
    {
        var identity = await GetIdentity(user.Login, user.Password);

        if (identity == null)
            return BadRequest("Invalid username or password");

        var now = DateTime.UtcNow;

        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var response = new
        {
            access_token = encodedJwt,
            username = identity.Name
        };

        return Json(response);
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] User newUser)
    {
        var ctx = await _contextFactory.CreateDbContextAsync();
        newUser.Role = "user";
        await ctx.Users.AddAsync(newUser);
        await ctx.SaveChangesAsync();

        return Ok();
    }

    private async Task<ClaimsIdentity?> GetIdentity(string username, string password)
    {
        var ctx = await _contextFactory.CreateDbContextAsync();
        var users = await ctx.Users.ToListAsync();

        var user = users.FirstOrDefault(x => x.Login == username && x.Password == password);

        if (user is null)
            return null;

        var claims = new List<Claim>
            {
                new(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };

        var claimsIdentity = new ClaimsIdentity(
            claims,
            "Token",
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);

        return claimsIdentity;
    }
}
