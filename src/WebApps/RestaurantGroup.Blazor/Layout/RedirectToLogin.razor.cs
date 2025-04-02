using System;
using Microsoft.AspNetCore.Components;

namespace RestaurantGroup.Blazor.Layout
{
    public class RedirectToLoginBase : ComponentBase
    {
        [Inject] protected NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            NavigationManager.NavigateTo($"/login?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}", true);
        }
    }
}