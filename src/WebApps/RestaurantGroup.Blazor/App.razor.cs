using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RestaurantGroup.Blazor.Services;

namespace RestaurantGroup.Blazor
{
    public class AppBase : ComponentBase
    {
        [Inject] protected IAuthService AuthService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await AuthService.Initialize();
        }
    }
}