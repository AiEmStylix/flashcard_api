// using System.Security.Claims;
// using System.Security.Cryptography;
// using flashcard_backend.DTOs;
// using flashcard_backend.Interfaces;
// using flashcard_backend.Models;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Authentication.Cookies;
//
// namespace flashcard_backend.Services;
//
// public class AuthService : IAuthService
// {
//     private IPasswordService _passwordService;
//     private IUserRepository _userRepository;
//
//     public AuthService(IPasswordService passwordService, IUserRepository userRepository)
//     {
//         _passwordService = passwordService;
//         _userRepository = userRepository;
//     }
//     
//     public async Task<(bool success, string message, UserModel user)> ValidateUser(LoginRequestDto loginDto)
//     {
//         var user = await _userRepository.GetUserByEmail(loginDto.Email);
//         if (user == null)
//         {
//             return (false, "Invalid user", null);
//         }
//
//         var isPasswordValid = _passwordService.VerifyPassword(user.PasswordHash, loginDto.Password);
//
//         if (!isPasswordValid)
//         {
//             return (false, "Invalid Password", null);
//         }
//
//         return (true, "Login successfully", user);
//     }
//
//     public async Task SignInUser(HttpContext httpContext, UserModel user, bool rememberme)
//     {
//         
//     }
// }