using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace RestaurantGroup.Blazor.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        private ClaimsPrincipal _user;

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (_user != null)
            {
                return Task.FromResult(new AuthenticationState(_user));
            }
            
            return Task.FromResult(new AuthenticationState(_anonymous));
        }

        public void MarkUserAsAuthenticated(string token)
        {
            var parsedToken = ParseJwt(token);
            
            // Extract claims from the token
            var claims = new List<Claim>();
            
            if (parsedToken.TryGetProperty("sub", out var sub))
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, sub.GetString()));
            }
            
            if (parsedToken.TryGetProperty("email", out var email))
            {
                claims.Add(new Claim(ClaimTypes.Email, email.GetString()));
            }
            
            if (parsedToken.TryGetProperty("given_name", out var givenName))
            {
                claims.Add(new Claim(ClaimTypes.GivenName, givenName.GetString()));
            }
            
            if (parsedToken.TryGetProperty("family_name", out var familyName))
            {
                claims.Add(new Claim(ClaimTypes.Surname, familyName.GetString()));
            }
            
            // Handle roles which might be an array
            if (parsedToken.TryGetProperty("role", out var roleElement))
            {
                if (roleElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (var role in roleElement.EnumerateArray())
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.GetString()));
                    }
                }
                else if (roleElement.ValueKind == JsonValueKind.String)
                {
                    claims.Add(new Claim(ClaimTypes.Role, roleElement.GetString()));
                }
            }
            
            _user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void MarkUserAsLoggedOut()
        {
            _user = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private JsonElement ParseJwt(string token)
        {
            // Get the payload part of the JWT (second part)
            var payload = token.Split('.')[1];
            
            // Add padding if needed
            while (payload.Length % 4 != 0)
            {
                payload += "=";
            }
            
            // Decode the Base64Url encoded string
            var bytes = Convert.FromBase64String(payload.Replace('-', '+').Replace('_', '/'));
            var jsonString = System.Text.Encoding.UTF8.GetString(bytes);
            
            return JsonDocument.Parse(jsonString).RootElement;
        }
    }
}