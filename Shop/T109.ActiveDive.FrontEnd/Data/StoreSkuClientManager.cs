using System;
using T104.Store.DataAccess.DataAccess;
using T104.Store.Engine.Models;

namespace T104.Store.AdminClient.Data
{
    public class StoreSkuClientManager
    {
        //Класс который занимается чтением и отображением ассортимента на клиенте (читает, сортируте, фильтрует, группирует и т.д.)

        public string Alias { get; set; } = "SkuManagerAlias";

        public string BaseAddress { get; set; } = "";

        public string GetResourceFullAddress (string ResourceLocalPath)
        {
            return @"https://t104assort.ricompany.info/ShopContent/" + ResourceLocalPath;
        }

        public string GetItemPageFullAddress(Guid itemId)
        {
            return $"{BaseAddress}/Item/" + itemId.ToString();
        }

        public StoreSkuClientManager SetBaseAddress(string address)
        {
            BaseAddress = address;
            return this;
        }

        public WebApiAsyncRepository<StoreSku> Repository { get; set; }

        Serilog.ILogger Logger { get; set; }

        public StoreSkuClientManager(WebApiAsyncRepository<StoreSku> repository, Serilog.ILogger logger)
        {
            Repository = repository;
            Logger = logger;
        }

        public StoreSkuClientManager(string baseAddress, string prefix, Serilog.ILogger logger)
        {
            Logger = logger;
            Repository = new WebApiAsyncRepository<StoreSku>(baseAddress, prefix).SetLogger(logger);
            
        }

    }
}
