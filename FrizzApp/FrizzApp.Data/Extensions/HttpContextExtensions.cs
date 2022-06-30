using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace FrizzApp.Data.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetUserFromToken(this IHttpContextAccessor httpContextAccesor)
        {
            var jwtToken = httpContextAccesor.HttpContext.Request.Headers["Authorization"]
                            .ToString()
                            .Replace("Bearer ", string.Empty);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValues = tokenHandler.ReadJwtToken(jwtToken);
            var userEmail = tokenValues.Subject;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"The user requester is: {userEmail}");
            Console.ResetColor();

            return userEmail;
        }
    }
}
