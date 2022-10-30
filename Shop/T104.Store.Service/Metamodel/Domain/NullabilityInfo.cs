using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T104.Store.Service.Metamodel
{
    public class NullabilityInfo
    {
        public bool allowNull = true;
        public object defaultValue = null;
        public bool considerNull = false;  //это способ присвоить null объекту  // Это опасная галка, лучше бы определять NUll как нибудь это вычисляя
    }
}
