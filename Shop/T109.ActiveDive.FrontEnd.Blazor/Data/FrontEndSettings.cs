using System;

namespace T109.ActiveDive.FrontEnd.Blazor.Data
{
    public class FrontEndSettings
    {
        public bool DisplayTopHorizontalMenu { get; set; } = true;
        public bool DisplayMainHorizontalMenu { get; set; } = true;
        public bool DisplayNavBar { get; set; } = true;
        public FrontEndSettings()
        {

        }

        public FrontEndSettings(bool displayTopHorizontalMenu, bool displayMainHorizontalMenu, bool displayNavBar)
        {
            DisplayTopHorizontalMenu = displayTopHorizontalMenu;
            DisplayMainHorizontalMenu = displayMainHorizontalMenu;
            DisplayNavBar = displayNavBar;

        }
    }
}
