using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RestaurantGroup.Blazor.Models.Authentication;
using RestaurantGroup.Blazor.Services;

namespace RestaurantGroup.Blazor.Pages
{
    public class RegisterBase : ComponentBase
    {
        [Inject] protected IAuthService AuthService { get; set; }
        [Inject] protected NavigationManager NavManager { get; set; }

        protected RegisterModel RegisterModel { get; set; } = new RegisterModel();
        protected bool IsLoading { get; set; }
        protected string ErrorMessage { get; set; } = string.Empty;

        protected async Task HandleRegister()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                var result = await AuthService.Register(RegisterModel);
                
                if (result.Success)
                {
                    // Redirect to login page
                    NavManager.NavigateTo("/login");
                }
                else
                {
                    ErrorMessage = result.Error;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Registration failed: " + ex.Message;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}