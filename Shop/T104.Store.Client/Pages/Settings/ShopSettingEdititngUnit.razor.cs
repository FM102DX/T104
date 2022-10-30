using Microsoft.AspNetCore.Components;
using T104.Store.Engine.Models;

namespace T104.Store.AdminClient.Pages.Settings
{
    public partial class ShopSettingEdititngUnit: ComponentBase
    {
        [Parameter] public ShopSetting shopSetting { get; set; }

    }
}
