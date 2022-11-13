using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;


namespace T109.ActiveDive.FrontEnd.Blazor.Data
{
    public class StoreManager
    {
        //Класс который инкапсулирует управление магазином целиком

        public string StoreBaseUrl { get; set; }

        public StoreManager(NavigationManager Navi)
        {
            
            StoreBaseUrl = Navi.BaseUri;
        }
        public string GetItemPageFullAddress(Guid itemId)
        {
            return $"{StoreBaseUrl}Item/" + itemId.ToString();
        }

    }
}
