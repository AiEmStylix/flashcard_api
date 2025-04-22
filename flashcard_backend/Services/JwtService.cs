using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using flashcard_backend.DatabaseContext;
using flashcard_backend.DTOs;
using flashcard_backend.Handler;
using flashcard_backend.Interfaces;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace flashcard_backend.Services;

public class JwtService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    
    public JwtService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<LoginResponse?> Authenticate(LoginRequestDto request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        {
            return null;
        }

        var userAccount = await _userRepository.GetUserByEmail(request.Email);
        // if (userAccount is null || !PasswordHashHandler.verifyPassword(userAccount.PasswordHash, request.Password))
        // {
        //     Console.WriteLine("User not found");
        //     return null;
        // }
        
        if (userAccount == null)
        {
            Console.WriteLine("User not found");
            return null;
        }
        
        if (!PasswordHashHandler.verifyPassword(userAccount.PasswordHash, request.Password))
        {
            Console.WriteLine("Password is incorrect");
            return null;
        }



        var issuer = _configuration["JwtConfig:Issuer"];
        var audience = _configuration["JwtConfig:Audience"];
        var key = _configuration["JwtConfig:Key"];
        var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
        var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, request.Email)
            }),
            Expires = tokenExpiryTimeStamp,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(securityToken);
        return new LoginResponse
        {
            AccessToken = accessToken,
            Email = request.Email,
            ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds,
        };
    }
}