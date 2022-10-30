using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using T104.Store.AdminClient.Data;
using T104.Store.FrontEnd.BlazorWASM.Data;

namespace T104.Store.FrontEnd.BlazorWASM.Components.Search
{
    public partial class Search : ComponentBase
    {
        [Inject]
        public Serilog.ILogger Logger { get; set; }

        [Inject]
        public  StoreManager MyStoreManager { get; set; }

        [Inject]
        NavigationManager MyNavigationManager { get; set; }

        [Inject]
        ComponentHub MyComponentHub { get; set; }

        public string Value { get; set; }

        public void SearchClicked()
        {
            ProcessSearch(Value);
        }

        public void Enter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                ProcessSearch(Value);
            }
        }

        public void ProcessSearch(string SearchText)
        {
            Logger.Information("Navigating to search text="+SearchText);
            MyNavigationManager.NavigateTo(MyNavigationManager.BaseUri + $@"search\{SearchText}");
            MyComponentHub.Search(SearchText);
        }

    }
}
