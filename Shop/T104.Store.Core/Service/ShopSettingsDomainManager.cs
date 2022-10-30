using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T104.Store.DataAccess.DataAccess;
using T104.Store.Engine.Models;

namespace T104.Store.Engine
{
    public class ShopSettingsDomainManager
    {
        private InMemoryRepository<ShopSettingDomain> ShopSettingDomains = new InMemoryRepository<ShopSettingDomain>();

        public ShopSettingsDomainManager()
        {

        }

        public ShopSettingDomain GetByAliasOrNull(string alias)
        {
            return ShopSettingDomains.GetAll().ToList().FirstOrDefault(x => x.Alias == alias);
        }

        public void Add(ShopSettingDomain domain)
        {
            ShopSettingDomains.Add(domain);
        }

        public List<ShopSettingDomain> GetItemsList()
        {
            return ShopSettingDomains.GetItemsList();
        }
    }
}
