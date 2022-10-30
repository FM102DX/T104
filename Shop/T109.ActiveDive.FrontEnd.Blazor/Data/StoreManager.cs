using System;


namespace T109.ActiveDive.FrontEnd.Blazor.Data
{
    public class StoreManager
    {
        //Класс который инкапсулирует управление магазином целиком

        public string StoreBaseUrl { get; }

        public StoreManager()  {  }

        public StoreManager(string storeBaseUrl)
        {
            StoreBaseUrl=storeBaseUrl;
        }

    }
}
