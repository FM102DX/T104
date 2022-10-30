using System;
using T109.ActiveDive;
using T109.ActiveDive.Core;
using T109.ActiveDive.DataAccess.DataAccess;

namespace T109.ActiveDive.FrontEnd.Blazor.Data
{
    public class ShopItemManager
    {
        //Класс который занимается чтением и отображением ассортимента на клиенте (читает, сортируте, фильтрует, группирует и т.д.)

        public string Alias { get; set; } = "SkuManagerAlias";

        public string BaseAddress { get; set; } = "";

        public string GetResourceFullAddress (string ResourceLocalPath)
        {
            return @$"{BaseAddress}{ResourceLocalPath}";
        }

        public string GetItemPageFullAddress(Guid itemId)
        {
            return $"{BaseAddress}/Item/" + itemId.ToString();
        }

        public ShopItemManager SetBaseAddress(string address)
        {
            BaseAddress = address;
            return this;
        }

        public WebApiAsyncRepository<ActiveDiveEvent> Repository { get; set; }

        Serilog.ILogger Logger { get; set; }

        public ShopItemManager(WebApiAsyncRepository<ActiveDiveEvent> repository, Serilog.ILogger logger)
        {
            Repository = repository;
            Logger = logger;
        }

        public ShopItemManager(string baseAddress, string prefix, Serilog.ILogger logger)
        {
            Logger = logger;
            Repository = new WebApiAsyncRepository<ActiveDiveEvent>(baseAddress, prefix).SetLogger(logger);
        }
    }
}
