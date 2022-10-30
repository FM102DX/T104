using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T104.Store.DataAccess.Models;

namespace T104.Store.Engine.Models
{
    public class ShopSettingDomain : BaseEntity
    {
        public string Alias { get; set; }

        public string Name { get; set; }

        public int DisplayOrder { get; set; }

    }
}
