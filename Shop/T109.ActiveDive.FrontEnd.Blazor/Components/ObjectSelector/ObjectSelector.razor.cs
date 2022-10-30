using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace T109.ActiveDive.FrontEnd.Blazor.Components.ObjectSelector
{
    public partial class ObjectSelector : ComponentBase
    {
        // component that give pissibility to select one object from multiple - image selectors, menu items, etc

        [Parameter]
        public List<SelectableObject> Objects { get; set; } = new List<SelectableObject>();

        [Parameter]
        public string SelectedObjectDivClass { get; set; }

        [Parameter]
        public string RegularObjectDivClass { get; set; }
        
        [Parameter]
        public string HolderDivStyle { get; set; }
        
        [Parameter]
        public SmallImgSelectorPositionEnum SelectorPosition { get; set; }

        [Parameter]
        public int ObjectWidth { get; set; }

        [Parameter]
        public int ObjectHeight { get; set; }

        [Parameter]
        public string HolderDivClass { get; set; }

        [Inject]
        public Serilog.ILogger Logger { get; set; }

        public SelectableObject Current 
        {
            get
            {
                return Objects[SelectedObjectIndex];
            }
        }

        public void ImClicked (SelectableObject selectableObject)
        {
           
            SelectedObjectIndex = selectableObject.Id;

            foreach (SelectableObject obj in Objects)
            {
                obj.Selected = (obj.Id == selectableObject.Id);
            }

            MySelectionChanged();
        }

        public ObjectSelector Selector { get => this; }

        public enum SmallImgSelectorPositionEnum
        {
            Horizontal=1,
            Vertical=2
        }

        protected override void OnInitialized()
        {

            if (Objects == null) { Logger.Information($"Images==Null"); }

            //на вход подается коллекция этих IMG
            Objects.ForEach(x=> Logger.Information ($"{x.Id}  {x.FullPathToPicture}"));

        }

        public class SelectableObject
        {
            public SelectableObjectTypeEnum SelectableObjectType { get; set; }

            public SelectableObject(SelectableObjectTypeEnum selectableObjectType)
            {
                SelectableObjectType = selectableObjectType;
            }

            public int Id { get; set; } = -1;

            private bool _selected = false;
            public Serilog.ILogger Logger { get; set; }
            public string FullPathToPicture { get; set; } = "";
            public SelectableObject SetSelectionSilently(bool selection)
            {
                _selected = selection;
                return this;
            }

            public bool Selected
            {
                get => _selected;

                set
                {
                    if (value != _selected)
                    {
                        if (value == true)
                        {
                            MySelectionChanged();
                        }
                        else
                        {
                            MySelectionChanged();
                        }

                        _selected = value;
                    }
                }
            }

            public event Action MySelectionChanged;

            public enum SelectableObjectTypeEnum
            {
                Image =1
            }
        }

        private int SelectedObjectIndex { get; set; } = -1;

        public event Action MySelectionChanged;

    }
}
