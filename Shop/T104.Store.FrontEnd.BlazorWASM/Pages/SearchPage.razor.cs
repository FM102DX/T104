using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using T104.Store.AdminClient.Data;
using T104.Store.Engine.Models;
using static T104.Store.FrontEnd.BlazorWASM.Components.StoreSkuCollection.StoreSkuCollection;

namespace T104.Store.FrontEnd.BlazorWASM.Pages
{
    public partial class SearchPage : ComponentBase
    {

        [Inject]
        public Serilog.ILogger Logger { get; set; }

        [Parameter]
        public string searchText { get; set; }

        public StoreSkuCollectionUsageCaseEnum UsageCase { get; set; } = StoreSkuCollectionUsageCaseEnum.MainBarSearch;

        protected override async Task OnInitializedAsync()
        {
        }
    }
}
