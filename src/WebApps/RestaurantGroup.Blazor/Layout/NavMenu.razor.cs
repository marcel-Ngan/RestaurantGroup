using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RestaurantGroup.Blazor.Services;

namespace RestaurantGroup.Blazor.Layout
{
    public class NavMenuBase : ComponentBase
    {
        [Inject] protected AuthenticationStateProvider AuthStateProvider { get; set; }
        [Inject] protected IAuthService AuthService { get; set; }
        [Inject] protected NavigationManager NavManager { get; set; }

        protected async Task Logout()
        {
            await AuthService.Logout();
            NavManager.NavigateTo("/login");
        }
    }
}