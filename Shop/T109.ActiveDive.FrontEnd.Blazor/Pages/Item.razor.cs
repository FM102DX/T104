using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using T109.ActiveDive.Core;
using T109.ActiveDive.FrontEnd.Blazor.Data;
using T109.ActiveDive.DataAccess.DataAccess;
using T109.ActiveDive.FrontEnd.Blazor.Components.ImageSelector;
using System.Collections;

namespace T109.ActiveDive.FrontEnd.Blazor.Pages
          
{
    public partial class Item : ComponentBase
    {

        [Inject]
        public StoreManager Manager { get; set; }

        [Inject]
        public WebApiAsyncRepository<ActiveDiveEvent> Repository { get; set; }

        [Inject]
        public Serilog.ILogger Logger { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public Guid ItemId { get; set; }

        public List<SelectableImage> ImgList { get; set; }= new List<SelectableImage>();

        public ActiveDiveEvent ActualItem { get; set; } = new ActiveDiveEvent();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ActualItem = await Repository.GetByIdOrNullAsync(ItemId);

                ImgList.Add(new SelectableImage() { Id = 0, FullSizePath = ActualItem.FirstPic, MidSizePath = ActualItem.FirstPic, ThumbPath = ActualItem.FirstPic });
                ImgList.Add(new SelectableImage() { Id = 1, FullSizePath = ActualItem.SecondPic, MidSizePath = ActualItem.SecondPic, ThumbPath = ActualItem.SecondPic });
                ImgList.Add(new SelectableImage() { Id = 2, FullSizePath = ActualItem.ThirdPic, MidSizePath = ActualItem.ThirdPic, ThumbPath = ActualItem.ThirdPic });
                
                //SetCurrentImage();

                //SmallImgSelector.MySelectionChanged += SmallImgSelector_MySelectionChanged;


            }
            catch (Exception ex)
            {
                Logger.Error("GetItemError" + ex.Message);
            }
        }

        public string CurrentImageFullPath { get; set; }

    }
}
