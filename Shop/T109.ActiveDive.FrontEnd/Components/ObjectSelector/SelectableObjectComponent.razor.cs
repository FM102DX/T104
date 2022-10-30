using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using T104.Store.AdminClient.Data;
using T104.Store.Engine.Models;
using Microsoft.JSInterop;

namespace T104.Store.FrontEnd.BlazorWASM.Components.ObjectSelector
{
    public partial class SelectableObjectComponent : ComponentBase
    {
        // компонент, который 


        //это для того, чтобы отслеживать клики в  ObjectSelector-e
        [Parameter]
        public ObjectSelector Parent { get; set; }


        [Parameter]
        public ObjectSelector.SelectableObject SourceObject { get; set; }


        [Inject]
        public Serilog.ILogger Logger { get; set; }


        public bool ImAnImage
        {
            get
            {
                return SourceObject.SelectableObjectType == ObjectSelector.SelectableObject.SelectableObjectTypeEnum.Image;
            }
        }

        public string ObjectWidth { get => (Parent.ObjectWidth > 0) ? $"{Parent.ObjectWidth}px; " : ""; }
        public string ObjectHeight { get => (Parent.ObjectHeight > 0) ? $"{Parent.ObjectHeight}px; " : ""; }
        public string ObjectDivClass { get => SourceObject.Selected ? Parent.SelectedObjectDivClass : Parent.RegularObjectDivClass; }
        public string FullPathToPicture { get => SourceObject.FullPathToPicture; }
        public string ObjectDivStyle 
        { 
            get 
            {
                bool isVertical = (Parent.SelectorPosition == ObjectSelector.SmallImgSelectorPositionEnum.Vertical);
                string clear = isVertical ? "clear: both;" : "";
                return $"display: block; position: relative; float: left; {clear}";
            } 
        }

        protected override void OnInitialized()
        {
            SourceObject.MySelectionChanged += ()=> StateHasChanged();
        }

        public void ImClicked()
        {
            Logger.Information($"IMG: ImClicked!");
            Parent.ImClicked(SourceObject);
        }
    }
}
