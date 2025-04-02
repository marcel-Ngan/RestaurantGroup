using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using RestaurantGroup.Blazor.Models.Authentication;
using RestaurantGroup.Blazor.Models.Users;

namespace RestaurantGroup.Blazor.Services
{
    public interface IAuthService
    {
        Task<AuthResult> Login(LoginModel model);
        Task<RegisterResult> Register(RegisterModel model);
        Task Logout();
        Task<UserModel> GetCurrentUser();
        Task Initialize();
    }

    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly IJSRuntime _jsRuntime;

        public AuthService(
            HttpClient httpClient,
            AuthenticationStateProvider authStateProvider,
            IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
            _jsRuntime = jsRuntime;
        }

        public async Task Initialize()
        {
            // Check if we have a token in local storage
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                ((CustomAuthStateProvider)_authStateProvider).MarkUserAsAuthenticated(token);
            }
        }

        public async Task<AuthResult> Login(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", model);
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResult>();
                if (result != null)
                {
                    // Store token in local storage
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", result.Token);
                    
                    // Set auth header for future requests
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
                    
                    // Update authentication state
                    ((CustomAuthStateProvider)_authStateProvider).MarkUserAsAuthenticated(result.Token);
                    
                    return result;
                }
            }
            
            // Handle error
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception(errorContent);
        }

        public async Task<RegisterResult> Register(RegisterModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", model);
            
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<UserModel>();
                return new RegisterResult { Success = true, User = user };
            }
            
            // Handle error
            var errorContent = await response.Content.ReadAsStringAsync();
            return new RegisterResult { Success = false, Error = errorContent };
        }

        public async Task Logout()
        {
            // Remove token from local storage
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
            
            // Clear auth header
            _httpClient.DefaultRequestHeaders.Authorization = null;
            
            // Update authentication state
            ((CustomAuthStateProvider)_authStateProvider).MarkUserAsLoggedOut();
        }

        public async Task<UserModel> GetCurrentUser()
        {
            var response = await _httpClient.GetAsync("api/users/me");
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserModel>();
            }
            
            return null;
        }
    }
}