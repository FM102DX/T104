using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using T109.ActiveDive.FrontEnd.Blazor.Data;

namespace T109.ActiveDive.FrontEnd.Blazor.Components.Search
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
        protected override void OnInitialized()
        {
            Value = "";
            MainSearchField
        }

    }
}
