using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace RestaurantGroup.Blazor.Components
{
    public class ComponentTemplateBase : ComponentBase
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public string Description { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        
        protected bool IsLoading { get; set; } = false;
        
        // Lifecycle methods
        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }
        
        protected async Task LoadDataAsync()
        {
            try
            {
                IsLoading = true;
                
                // Load data here
                await Task.Delay(100); // Placeholder for actual data loading
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}