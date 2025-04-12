using Microsoft.AspNetCore.Components;
using RestaurantGroup.Blazor.Services;

namespace RestaurantGroup.Blazor
{
    public class AppBase : ComponentBase
    {
        [Inject] protected IAuthService AuthService { get; set; }

        // Remove or comment out the OnInitialized method for now
        /*
        protected override async Task OnInitializedAsync()
        {
            await AuthService.Initialize();
        }
        */
    }
}