using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using static T109.ActiveDive.FrontEnd.Blazor.Components.ShopItemCollection.ShopItemCollection;

namespace T109.ActiveDive.FrontEnd.Blazor.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public Serilog.ILogger Logger { get; set; }

        public ShopItemCollectionUsageCaseEnum UsageCase { get=> ShopItemCollectionUsageCaseEnum.MainPageAppearamce; }

        protected override async Task OnInitializedAsync()
        {
            Logger.Information("index page opened");
        }
    }
}
