using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using static T109.ActiveDive.FrontEnd.Blazor.Components.ShopItemCollection.ShopItemCollection;

namespace T109.ActiveDive.FrontEnd.Blazor.Pages
{
    public partial class SearchPage : ComponentBase
    {

        [Inject]
        public Serilog.ILogger Logger { get; set; }

        [Parameter]
        public string SearchText { get; set; }

        public ShopItemCollectionUsageCaseEnum UsageCase { get; set; } = ShopItemCollectionUsageCaseEnum.MainBarSearch;

        protected override async Task OnInitializedAsync()
        {

        }
    }
}
