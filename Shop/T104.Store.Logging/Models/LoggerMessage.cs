using T104.Store.DataAccess.Models;

namespace T104.Store.Logging.Models
{
    public class LoggerMessage:BaseEntity
    {
        public string Message { get; set; }
    }
}
