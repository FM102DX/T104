using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T104.Store.DataAccess.Abstract;
using T104.Store.DataAccess.DataAccess;
using T104.Store.Engine.Models;
using T104.Store.Service;

namespace T104.Store.Engine
{
    public class ShopSettingsManager
    {
        //класс, управляющий настройками
        private IAsyncRepository<ShopSetting> _repo;

        public List<ShopSetting> ShopSettingsList;

        public ShopSettingsDomainManager Domains = new ShopSettingsDomainManager();

        public Serilog.ILogger Logger;

        public ShopSettingsManager(IAsyncRepository<ShopSetting> repo)
        {
            _repo = repo;

            //пока так
            Domains.Add(new ShopSettingDomain { Alias = "Shop.Core", Name = "Главные настройки", DisplayOrder = 0 });
            Domains.Add(new ShopSettingDomain { Alias = "Shop.Core.MyCat", Name = "MyCat", DisplayOrder = 1 });
        }

        public async Task<CommonOperationResult> SaveSettingAsync(ShopSetting newShopSetting)
        {
            Logger.Information($"SHA saving {newShopSetting.Name} {newShopSetting.Value}");

            CommonOperationResult rez = await _repo.UpdateAsync(newShopSetting);

            if (rez.success)
            {
                ShopSetting nsh = ShopSettingsList.FirstOrDefault(x=>x.Id== newShopSetting.Id);

                int targetIndex = ShopSettingsList.IndexOf(nsh);

                ShopSettingsList[targetIndex] = newShopSetting.Clone(); 

                return CommonOperationResult.sayOk(rez.msg);
            }
            else
            {
                return CommonOperationResult.sayFail(rez.msg);
            }
        }

        public ShopSettingsManager SetLogger(Serilog.ILogger logger)
        {
            Logger = logger;
            return this;
        }

        public async Task<CommonOperationResult> RemoveSettingAsync(Guid id)
        {
            CommonOperationResult rez = await _repo.DeleteAsync(id);

            if (rez.success)
            {
                return CommonOperationResult.sayOk(rez.msg);
            }
            else
            {
                return CommonOperationResult.sayFail(rez.msg);
            }

        }

        private CommonOperationResult Validate (ShopSetting newShopSetting)
        {
            return CommonOperationResult.sayOk();
        }

        public ShopSetting GetSettingOrNull (string name, string domainAlias)
        {
            ReadSettings();
            var x = ShopSettingsList.Where(x => x.Name == name && x.DomainAlias == domainAlias).ToList().FirstOrDefault();
            return x;
        }

        public void ReadSettings ()
        {
            ShopSettingsList = _repo.GetItemsListAsync().Result;
            SetMagager();
        }

        public async Task ReadSettingsAsync()
        {
            ShopSettingsList =  await _repo.GetItemsListAsync();
            SetMagager();

        }
        private void SetMagager()
        {
            foreach (ShopSetting shopSetting in ShopSettingsList)
            {
                shopSetting.Manager = this;
            }
        }

        public List<ShopSetting> GetSettingsList()
        {
            Console.WriteLine($"Дымыч 0 ");
            List<ShopSetting> rez = _repo.GetItemsListAsync().GetAwaiter().GetResult();
            Console.WriteLine($"Дымыч 1 rez.Count={rez.Count}");
            return rez;
        }

        public async Task<List<ShopSetting>> GetSettingsListAsync()
        {
            List<ShopSetting> rez = await _repo.GetItemsListAsync();
            return rez;
        }

        public void CreateIfNotExist(ShopSetting setting)
        {
            //если настройки с таким именем и доменом нет, создает ее
            //если есть, ничего не делает

            //ReadSettings();
            var x = GetSettingOrNull(setting.Name, setting.DomainAlias);
            if (x==null)
            {
                _repo.AddAsync(setting);
            }
        }

        public ShopSetting GetByFullName(string fullName)
        {
            foreach (var setting in ShopSettingsList)
            {
                if (setting.FullName == fullName) return setting;
            }
            return null;
        }

        public void Init(bool removeExistingSettingsFromDb=false)
        {
            if (removeExistingSettingsFromDb)
            {
                ReadSettings();
                ShopSettingsList.ForEach(x => RemoveSettingAsync(x.Id));
            }

            CreateIfNotExist(new ShopSetting() { DomainAlias = "Shop.Core", Name = "ShopLongTitle", Title = "Длинное название магазина", InnerType = ShopSetting.SettingTypeEnum.String, Value = "OTUS T104 group Online Store project" });
            CreateIfNotExist(new ShopSetting() { DomainAlias = "Shop.Core", Name = "ShopShortTitle", Title = "Краткое название магазина", InnerType = ShopSetting.SettingTypeEnum.String, Value = "OTUS.T104.Store" });
            CreateIfNotExist(new ShopSetting() { DomainAlias = "Shop.Core", Name = "SkuPerPage", Title = "Количество товаров на страницу", InnerType = ShopSetting.SettingTypeEnum.Int, Value = "20" });
            CreateIfNotExist(new ShopSetting() { DomainAlias = "Shop.Core.MyCat", Name = "MyCatBirthday", Title = "День рождения кота", InnerType = ShopSetting.SettingTypeEnum.DateTime, Value = "10.05.2022"});
            CreateIfNotExist(new ShopSetting() { DomainAlias = "Shop.Core.MyCat", Name = "MyCatsName", Title = "Имя кота", InnerType = ShopSetting.SettingTypeEnum.String, Value = "Barsik" });
            CreateIfNotExist(new ShopSetting() { DomainAlias = "Shop.Core.MyCat", Name = "MyCatsWeight", Title = "Вес кота, кг.", InnerType = ShopSetting.SettingTypeEnum.Double, Value = "2,73" });
            CreateIfNotExist(new ShopSetting() { DomainAlias = "Shop.Core.MyCat", Name = "MyCatsColor", Title = "Окрас кота", InnerType = ShopSetting.SettingTypeEnum.FixedEnum, Value = "" }
            .AddFixedEnumValueOption(1,"Рыжий")
            .AddFixedEnumValueOption(2, "Черный")
            .AddFixedEnumValueOption(3, "Белый")
            .AddFixedEnumValueOption(4, "Дымчатый")
            .AddFixedEnumValueOption(5, "Бирманский")
            .AddFixedEnumValueOption(6, "Сиамский")
            .AddFixedEnumValueOption(7, "Другой")
            );
            CreateIfNotExist(new ShopSetting() { DomainAlias = "Shop.Core.MyCat", Name = "IsMyCatBlack", Title = "Мой кот черный?", InnerType = ShopSetting.SettingTypeEnum.Bool, Value = "false" });
        }

        
    }
}
