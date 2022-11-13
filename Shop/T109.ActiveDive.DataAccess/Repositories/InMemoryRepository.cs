using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T109.ActiveDive.DataAccess.Abstract;
using T109.ActiveDive.Service;

namespace T109.ActiveDive.DataAccess
{
    public class InMemoryRepository<T> : IRepository<T> where T :IBaseEntity, new ()
    {
        public List<T> Data { get; set; } = new List<T>();

        public InMemoryRepository(){}

        public IEnumerable<T> GetAll() => Data;

        public T GetByIdOrNull(Guid id)=> Data.FirstOrDefault(x => x.Id == id);

        public int Count => Data.Count;

        public bool Exists(Guid id)=> GetByIdOrNull(id) != null; 

        public CommonOperationResult Add(T t)
        {
            Data.Add(t);
            return CommonOperationResult.SayOk();
        }

        public CommonOperationResult Update(T t)
        {
            var i = Data.IndexOf(t);
            if (i == -1)
            {
                return CommonOperationResult.SayFail();
            }
            else
            {
                Data[i]=t;
                return CommonOperationResult.SayOk();
            }
        }

        public CommonOperationResult Delete(Guid id)
        {
            T t = GetByIdOrNull(id);
            if (t==null)
            {
                return CommonOperationResult.SayFail();
            }
            else
            {
                Data.Remove(t);
                return CommonOperationResult.SayOk();
            }
        }

        public CommonOperationResult Init(bool deleteDb = false)
        {
            Data.Clear();
            return CommonOperationResult.SayOk();
        }

        public List<T> GetItemsList()
        {
            return Data;
        }

    }
}