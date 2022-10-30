using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T109.ActiveDive.DataAccess.Abstract;
using T109.ActiveDive.DataAccess.Models;
using T109.ActiveDive.Service;

namespace T109.ActiveDive.DataAccess.DataAccess
{
    public class InMemoryAsyncRepository<T> : IAsyncRepository<T> where T : BaseEntity, new ()
    {
        bool DebugMode = true;
        
        private List<T> Data { get; set; } = new List<T>();

        public InMemoryAsyncRepository()
            {
                if (DebugMode)
                {
                    T t = new T();
                    t.Id = Guid.NewGuid();
                    t.SysMessage = $"repo created at {DateTime.Now.ToShortTimeString()}";
                    Data.Add(t);
                }
            }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult((IEnumerable<T>)Data);
        }

        public Task<T> GetByIdOrNullAsync(Guid id)
        {
            T t = Data.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(t);

        }

        public Task<int> Count
        {
            get 
            {
                return Task.FromResult(Data.Count);
            }
        }

        public Task<bool> Exists(Guid id)
        {
            return Task.FromResult(GetByIdOrNullAsync(id).Result != null); 
        }

        public Task<CommonOperationResult> AddAsync(T t)
        {
            Data.Add(t);
            return Task.FromResult(CommonOperationResult.sayOk());
        }

        public Task<CommonOperationResult> UpdateAsync(T t)
        {
            var i = Data.IndexOf(t);
            if (i == -1)
            {
                return Task.FromResult(CommonOperationResult.sayFail());
            }
            else
            {
                Data[i]=t;
                return Task.FromResult(CommonOperationResult.sayOk());
            }
        }

        public Task<CommonOperationResult> DeleteAsync(Guid id)
        {
            T t = GetByIdOrNullAsync(id).Result;
            if (t==null)
            {
                return Task.FromResult(CommonOperationResult.sayFail());
            }
            else
            {
                Data.Remove(t);
                return Task.FromResult(CommonOperationResult.sayOk());
            }
        }

        public Task<CommonOperationResult> InitAsync(bool deleteDb = false)
        {
            Data.Clear();
            return Task.FromResult(CommonOperationResult.sayOk());
        }

        Task<List<T>> IAsyncRepository<T>.GetItemsListAsync()
        {
            return Task.FromResult(Data);
        }
    }
}