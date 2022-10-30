using System.Collections.Generic;
using T104.Store.Engine.Models;

namespace T104.Store.AssortmentWebApi.Models
{
    public class AssortmentDisplayViewModel
    {
        public List<StoreSku> StoreSkuList { get; set; }
        public string CssPath { get; set; }

        public string PicPath { get; set; }
    }
}
