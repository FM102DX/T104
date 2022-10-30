using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using T104.Store.AdminClient.Data;
using T104.Store.Engine.Models;
using T104.Store.FrontEnd.BlazorWASM.Components.Search;
using T104.Store.FrontEnd.BlazorWASM.Data;

namespace T104.Store.FrontEnd.BlazorWASM.Components.StoreSkuCollection
{
    public partial class StoreSkuCollection: ComponentBase
    {
        [Inject]
        public StoreSkuClientManager SkuManager { get; set; }

        [Inject]
        public Serilog.ILogger Logger { get; set; }

        [Inject]
        ComponentHub MyComponentHub { get; set; }

        [Parameter]
        public StoreSkuCollectionUsageCaseEnum UsageCase { get; set; }

        [Parameter]
        public string SearchText { get; set; }

        public List<StoreSku> ItemsNo { get; set; } = new List<StoreSku>();

        public string FullName { get; set; }

        public int Count { get; set; }

        protected override async Task OnInitializedAsync()
        {

            MyComponentHub.DoingSearch += MyComponentHub_DoingSearch;

            ItemsNo = await SkuManager.Repository.GetItemsListAsync();
            Count = ItemsNo.Count;


            /*
            if (UsageCase== StoreSkuCollectionUsageCaseEnum.MainPageAppearamce)
            {

            }
            else if (UsageCase == StoreSkuCollectionUsageCaseEnum.MainBarSearch)
            {

            }
            */
        }

        private async void MyComponentHub_DoingSearch(string SearchText)
        {
            Logger.Information("MainBarSearch: Searching " + SearchText);

            ItemsNo = await SkuManager.Repository.SearchList(SearchText);

            Count = ItemsNo.Count;

            ItemsNo.ForEach(x => Logger.Information("MyTest " + x.Alias));

            StateHasChanged();
        }

        public enum StoreSkuCollectionUsageCaseEnum
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
