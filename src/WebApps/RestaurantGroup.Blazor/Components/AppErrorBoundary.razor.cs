using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace RestaurantGroup.Blazor.Components
{
    public partial class AppErrorBoundary : Microsoft.AspNetCore.Components.Web.ErrorBoundary
    {
        [Inject] private ILogger<AppErrorBoundary> Logger { get; set; }
        
        protected override Task OnErrorAsync(Exception exception)
        {
            Logger.LogError(exception, "An unhandled error occurred");
            return base.OnErrorAsync(exception);
        }
    }
}