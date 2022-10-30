using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using T109.ActiveDive.Service;

namespace T109.ActiveDive.DataAccess.Abstract
{
    public interface IAsyncRepository<T> where T : IBaseEntity
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public Task<List<T>> GetItemsListAsync();

        public Task<T> GetByIdOrNullAsync(Guid id);

        public Task<int> Count { get; }

        public  Task<bool> Exists(Guid id);

        public Task<CommonOperationResult> AddAsync(T t);

        public Task<CommonOperationResult> UpdateAsync(T t);

        public Task<CommonOperationResult> DeleteAsync(Guid id);

        public Task<CommonOperationResult> InitAsync(bool deleteDb = false);

    }
}
