using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T104.Store.Service.Metamodel
{
    public class MetaModelBase : IMetaModel
    {

        public Serilog.ILogger Logger { get; set; }

        private List<IEntityInfo> _entities = new List<IEntityInfo>();

        public IMetaModel AddEntityInfo(IEntityInfo entityInfo)
        {
            entityInfo.Parent = this;
            _entities.Add(entityInfo);
            return this;
        }
        public IEntityInfo GetEntityInfo(Type typeName)
        {
            return _entities.FirstOrDefault(x => x.TypeName == typeName.Name);
        }

        void IMetaModel.LoadFromFile(string filePath)
        {
            throw new NotImplementedException();
        }
        public IMetaModel SetLogger(Serilog.ILogger logger)
        {
            Logger = logger;
            return this;
        }
    }
}
