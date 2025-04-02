using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RestaurantGroup.Blazor.Models.Users;
using RestaurantGroup.Blazor.Services;

namespace RestaurantGroup.Blazor.Pages
{
    public class ProfileBase : ComponentBase
    {
        [Inject] protected IAuthService AuthService { get; set; }
        [Inject] protected IJSRuntime JSRuntime { get; set; }

        protected UserModel User { get; set; }
        protected bool IsLoading { get; set; } = true;
        protected string ErrorMessage { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                User = await AuthService.GetCurrentUser();
            }
            catch (Exception ex)
            {
                ErrorMessage = "Failed to load profile: " + ex.Message;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}