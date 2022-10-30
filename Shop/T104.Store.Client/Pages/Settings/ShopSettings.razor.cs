using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using T104.Store.DataAccess.DataAccess;
using T104.Store.Engine;
using T104.Store.Engine.Models;
using T104.Store.Service;
using T104.Store.Service.Metamodel;
using Serilog;
using T104.Store.DataAccess.Abstract;
using BBComponents;
using BBComponents.Enums;

namespace T104.Store.AdminClient.Pages.Settings
{
    public partial class ShopSettings: ComponentBase
    {
        [Inject]
        public HttpClient _httpClient { get; set; }

        [Inject]
        public Serilog.ILogger _logger { get; set; }

        [Inject]
        public IMetaModel _metaModel { get; set; }

        [Inject]
        public BBComponents.Services.IAlertService AlertService { get; set; }

        public ShopSettings()
        {
        
        }

        private string div1Contents;
        private string div2Contents;

        private bool saving = false;

        private int col = 0;

        private ShopSettingsManager shopSettingsManager;

        //private List<ShopSetting> settingsList=new List<ShopSetting>();

        private List<ShopSetting> settingsListTmp = new List<ShopSetting>();

        private List<ShopSettingDomain> settingDomains = new List<ShopSettingDomain>();

        protected override async Task OnInitializedAsync()
        {
            //его не получилось инжектнуть, т.к. там надо сразу чтобы был http-клинет, который создается там же


            IAsyncRepository<ShopSetting> repo = new WebApiAsyncRepository<ShopSetting>(_httpClient, "/api/v1/ShopSettings")
                .SetLogger(_logger);

            shopSettingsManager = new ShopSettingsManager(repo);
            
            await shopSettingsManager.ReadSettingsAsync();

            shopSettingsManager.ShopSettingsList.ForEach(x=>settingsListTmp.Add(x.Clone()));

            //col = shopSettingsManager.ShopSettingsList.Count;


            settingDomains = shopSettingsManager.Domains.GetItemsList();

            _metaModel.SetLogger(_logger);

            shopSettingsManager.SetLogger(_logger);
        }

        private async void OnShDumpClick()
        {
            //  _logger.Information($"OnShDumpClick");

            shopSettingsManager.ShopSettingsList.ForEach(x => x.SetLogger(_logger));

            settingsListTmp.ForEach(x => x.SetLogger(_logger));

            div1Contents = string.Join("|", shopSettingsManager.ShopSettingsList.Select(x => $"{x.FullName} {x.Value}"));
            
            //  div2Contents = string.Join("|", settingsListTmp.FirstOrDefault(x => x.InnerType == ShopSetting.SettingTypeEnum.FixedEnum).FixedEnumValueOptions);
            
            div2Contents = string.Join("|", settingsListTmp.Select(x => $"{x.FullName} {x.Value}"));
        }
        
        private async void OnSaveClick()
        {
            
            if (saving) return;

            saving = true;

            IEntityInfo entityInfo = _metaModel.GetEntityInfo(typeof(ShopSetting));

            List<ShopSetting> modifiedSettings = new List<ShopSetting>();

            //вбираем те, что изменились

            foreach (var tmpSetting in settingsListTmp)
            {
                
                ShopSetting setting = shopSettingsManager.ShopSettingsList.Where(actualSetting => actualSetting.FullName == tmpSetting.FullName
                                                            && actualSetting.Value.Trim() != tmpSetting.Value.Trim())
                                                                .FirstOrDefault();
                if (setting != null) modifiedSettings.Add(tmpSetting);
            }

            if (settingsListTmp.Count==0) return;

            modifiedSettings.ForEach(x => Console.WriteLine($"Chaged: {x.Name} {x.Value}"));

            //валидация

            foreach (ShopSetting tmpSetting in modifiedSettings)
            {
                ValidationResult vr = entityInfo.Validate(ValidationTypeEnum.Business, tmpSetting);
                if (!vr.success)
                {
                    //ошибка валидации
                    AlertService.Add($"{vr.msg}", BBComponents.Enums.BootstrapColors.Warning);
                    saving = false;
                    return;
                }
            }

            Console.WriteLine($"Validation passed");

            //сохранение
            foreach (ShopSetting tmpSetting in modifiedSettings)
            {

                Console.WriteLine($"Saving: {tmpSetting.Name} {tmpSetting.Value}");

                CommonOperationResult rez = await shopSettingsManager.SaveSettingAsync(tmpSetting);

                if (rez.success)
                {
                    //сохранение успешно
                    AlertService.Add($"{tmpSetting.Title}: изменения успешно сохранены", BBComponents.Enums.BootstrapColors.Success);
                }
                else
                {
                    AlertService.Add($"{tmpSetting.Title}: Ошибка сохранения", BBComponents.Enums.BootstrapColors.Danger);
                }
            }

                    saving = false;
         }

       }
}
