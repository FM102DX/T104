using System;

namespace T109.ActiveDive.EventCatalogue.EventCatalogueApi
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
