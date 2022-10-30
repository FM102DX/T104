using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T104.Store.Service.Metamodel
{
    public interface IMetaModel
    {
        public IMetaModel AddEntityInfo(IEntityInfo entityInfo);
        public IEntityInfo GetEntityInfo(Type typeName);

        public void LoadFromFile(string filePath);
        public IMetaModel SetLogger(Serilog.ILogger logger);
        public Serilog.ILogger Logger { get; set; }

    }
}
