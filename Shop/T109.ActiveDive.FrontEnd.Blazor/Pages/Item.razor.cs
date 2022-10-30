﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Selector = T109.ActiveDive.FrontEnd.Blazor.Components.ObjectSelector.ObjectSelector;
using T109.ActiveDive.Core;
using T109.ActiveDive.FrontEnd.Blazor.Data;

namespace T109.ActiveDive.FrontEnd.Blazor.Pages
          
{
    public partial class Item : ComponentBase
    {

        [Inject]
        public ShopItemManager SkuManager { get; set; }

        [Inject]
        public Serilog.ILogger Logger { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public Guid ItemId { get; set; }

        public ActiveDiveEvent ActualItem { get; set; } = new ActiveDiveEvent();


        public Selector SmallImgSelector;

        //начальная позиция 
        public Selector.SmallImgSelectorPositionEnum SelectorPosition { get; set; } =
                                        Selector.SmallImgSelectorPositionEnum.Vertical;

        
        //список отображаемых объектов, которые рисунки 
        public List<Selector.SelectableObject> DisplayedObjects { get; set; } = new List<Selector.SelectableObject>();

        public ActiveDiveEvent Sku { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ActualItem = await SkuManager.Repository.GetByIdOrNullAsync(ItemId);
                SkuManager.SetBaseAddress(Navigation.BaseUri);
                //SetCurrentImage();

                DisplayedObjects.Add(new Selector.SelectableObject(Selector.SelectableObject.SelectableObjectTypeEnum.Image) { Id = 0, Logger = this.Logger, FullPathToPicture = SkuManager.GetResourceFullAddress(ActualItem.FirstPic) }.SetSelectionSilently(true));
                DisplayedObjects.Add(new Selector.SelectableObject(Selector.SelectableObject.SelectableObjectTypeEnum.Image) { Id = 1, Logger = this.Logger, FullPathToPicture = SkuManager.GetResourceFullAddress(ActualItem.SecondPic) }.SetSelectionSilently(true));
                DisplayedObjects.Add(new Selector.SelectableObject(Selector.SelectableObject.SelectableObjectTypeEnum.Image) { Id = 2, Logger = this.Logger, FullPathToPicture = SkuManager.GetResourceFullAddress(ActualItem.ThirdPic) }.SetSelectionSilently(true));

                SmallImgSelector.MySelectionChanged += SmallImgSelector_MySelectionChanged;

            }
            catch (Exception ex)
            {
                Logger.Error("GetItemError" + ex.Message);
            }
        }

        private void SmallImgSelector_MySelectionChanged()
        {
            
            CurrentImageFullPath = SmallImgSelector.Current.FullPathToPicture;

            StateHasChanged();

            Logger.Information($">>Selection changed path={CurrentImageFullPath }");
        }

        public string CurrentImageFullPath { get; set; }

    }
}
