using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace RestaurantGroup.Blazor.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject] protected AuthenticationStateProvider AuthStateProvider { get; set; }
    }
}