using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RestaurantGroup.Blazor.Models.Authentication;
using RestaurantGroup.Blazor.Services;

namespace RestaurantGroup.Blazor.Pages
{
    public class LoginBase : ComponentBase
    {
        [Inject] protected IAuthService AuthService { get; set; }
        [Inject] protected NavigationManager NavManager { get; set; }

        protected LoginModel LoginModel { get; set; } = new LoginModel();
        protected bool IsLoading { get; set; }
        protected string ErrorMessage { get; set; } = string.Empty;

        protected async Task HandleLogin()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                var result = await AuthService.Login(LoginModel);
                
                // Redirect to home page
                NavManager.NavigateTo("/");
            }
            catch (Exception ex)
            {
                ErrorMessage = "Login failed: " + ex.Message;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}