using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using T109.ActiveDive.Core;
using T109.ActiveDive.FrontEnd.Blazor.Data;

namespace T109.ActiveDive.FrontEnd.Blazor.Components.ShopItemCollection
{
    public partial class ShopItemCollection: ComponentBase
    {
        [Inject]
        public ShopItemManager ItemManager { get; set; }

        [Inject]
        public Serilog.ILogger Logger { get; set; }

        [Inject]
        ComponentHub MyComponentHub { get; set; }

        [Parameter]
        public ShopItemCollectionUsageCaseEnum UsageCase { get; set; }

        [Parameter]
        public string SearchText { get; set; }

        public List<ActiveDiveEvent> ItemsNo { get; set; } = new List<ActiveDiveEvent>();

        public string FullName { get; set; }

        public int Count { get; set; }

        protected override async Task OnInitializedAsync()
        {
            MyComponentHub.DoingSearch += MyComponentHub_DoingSearch;
            ItemsNo = await ItemManager.Repository.GetItemsListAsync();
            Count = ItemsNo.Count;

            Logger.Information($"IncomingItemsCount= {ItemsNo.Count}");
            /*
            if (UsageCase== ShopItemCollectionUsageCaseEnum.MainPageAppearamce)
            {

            }
            else if (UsageCase == ShopItemCollectionUsageCaseEnum.MainBarSearch)
            {

            }
            */
        }

        private async void MyComponentHub_DoingSearch(string SearchText)
        {
            Logger.Information("MainBarSearch: Searching " + SearchText);

            ItemsNo = await ItemManager.Repository.SearchList(SearchText);

            Count = ItemsNo.Count;

            ItemsNo.ForEach(x => Logger.Information("MyTest " + x.Alias));

            StateHasChanged();
        }

        public enum ShopItemCollectionUsageCaseEnum
        {
            MainPageAppearamce=1,
            MainBarSearch = 2
        }

        public void DoLoggerAction()
        {
            Logger.Information("LoggerActionDone");
        }
    }
}
