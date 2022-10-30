using System.Collections.Generic;
using T109.ActiveDive.Core;

namespace T109.ActiveDive.EventCatalogue.EventCatalogueApi
{
    public class EventDisplayViewModel
    {
        public List<ActiveDiveEvent> ItemList { get; set; }
        public string CssPath { get; set; }
    }
}
