using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace T104.Store.AdminClient.Shared
{
    public partial class WorkflowStepDataModal : ComponentBase
    {
        [Parameter]
        public string FormTitle { get; set; }

        [Parameter]
        public string JsonData { get; set; }

        [Parameter]
        public EventCallback Closed { get; set; }

        public async Task OnCloseClick()
        {
            await Closed.InvokeAsync(null);
        }
    }
}
