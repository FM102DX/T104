using System;

namespace T104.Store.Logging.Models
{ 
    public class LogEvent
    {
        public DateTime Timestamp { get; set; }

        public string Level { get; set; }

        public string RenderedMessage { get; set; }
    }
}
