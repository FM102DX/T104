using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace T104.Store.Shared.Helpers
{
    public class JsonHelper
    {
        public static string Serialize(object data)
        {
            var result = JsonConvert.SerializeObject(data,
                Formatting.Indented,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            return result;
        }
    }
}
