using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T104.Store.DataAccess.Abstract;
using T104.Store.DataAccess.Models;
using T104.Store.Service;

namespace T104.Store.DataAccess.DataAccess
{
    public class InMemoryRepository<T> : IRepository<T> where T : IBaseEntity, new ()
    {
        
        public List<T> Data { get; set; } = new List<T>();

        public InMemoryRepository()
            {

            }

        public IEnumerable<T> GetAll()
        {
            return Data;
        }

        public T GetByIdOrNull(Guid id)
        {
            T t = Data.FirstOrDefault(x => x.Id == id);
            return t;

        }

        public int Count
        {
            get 
            {
                return Data.Count;
            }
        }

        public bool Exists(Guid id)
        {
            return GetByIdOrNull(id) != null; 
        }

        public CommonOperationResult Add(T t)
        {
            Data.Add(t);
            return CommonOperationResult.sayOk();
        }

        public CommonOperationResult Update(T t)
        {
            var i = Data.IndexOf(t);
            if (i == -1)
            {
                return CommonOperationResult.sayFail();
            }
            else
            {
                Data[i]=t;
                return CommonOperationResult.sayOk();
            }
        }

        public CommonOperationResult Delete(Guid id)
        {
            T t = GetByIdOrNull(id);
            if (t==null)
            {
                return CommonOperationResult.sayFail();
            }
            else
            {
                Data.Remove(t);
                return CommonOperationResult.sayOk();
            }
        }

        public CommonOperationResult Init(bool deleteDb = false)
        {
            Data.Clear();
            return CommonOperationResult.sayOk();
        }

        public List<T> GetItemsList()
        {
            return Data;
        }

    }
}