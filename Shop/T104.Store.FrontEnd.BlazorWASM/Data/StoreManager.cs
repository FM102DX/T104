using System;
using T104.Store.DataAccess.DataAccess;
using T104.Store.Engine.Models;

namespace T104.Store.AdminClient.Data
{
    public class StoreManager
    {
        //Класс который инкапсулирует управление магазином целиком

        public string StoreBaseUrl { get; }

        public StoreManager()
        {

        }

        public StoreManager(string storeBaseUrl)
        {
            StoreBaseUrl=storeBaseUrl;
        }

    }
}
