using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using T104.Store.DataAccess.Abstract;
using T104.Store.DataAccess.Models;
using T104.Store.Service;

namespace T104.Store.DataAccess
{
    public class EfAsyncRepository<T> : IAsyncRepository<T> where T: BaseEntity
    {
        
        private DbContext _context;

        public EfAsyncRepository(DbContext context)
        {
            _context = context;
        }

        public Task<int> Count => Task.FromResult(_context.Set<T>().Count());

        public Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                var rez = Task.FromResult((IEnumerable<T>)_context.Set<T>());
                return rez;
            }
            catch (Exception ex)
            {
                List<T> lst = new List<T>();
                IEnumerable<T> en = (IEnumerable<T>)lst;
                return Task.FromResult(en);
            }
        }

        public Task<T> GetByIdOrNullAsync(Guid id)
        {
            return _context.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            T targetObejct = GetByIdOrNullAsync(id).Result;
            return Task.FromResult(targetObejct == null);
        }

        public Task<CommonOperationResult> AddAsync(T t)
        {
            try
            {
                _context.Set<T>().Add(t);
                _context.SaveChanges();
                return Task.FromResult(CommonOperationResult.sayOk());
            }
            catch (Exception ex)
            {
                return Task.FromResult(CommonOperationResult.sayFail(ex.Message));
            }
        }

        public Task<CommonOperationResult> UpdateAsync(T t)
        {
            _context.Set<T>().Update(t);
            var rez = _context.SaveChanges();
            return Task.FromResult(CommonOperationResult.sayOk(rez.ToString()));
        }

        public Task<CommonOperationResult> DeleteAsync(Guid id)
        {
            T t = this.GetByIdOrNullAsync(id).Result;
            if (t == null)
            {
                return Task.FromResult(CommonOperationResult.sayFail($"Id not found: {id}"));
            }
            _context.Set<T>().Remove(t);
            var rez = _context.SaveChanges();
            return Task.FromResult(CommonOperationResult.sayOk(rez.ToString()));
        }

        public Task<bool> Exists(Guid id)
        {
            T t = this.GetByIdOrNullAsync(id).Result;
            return Task.FromResult(t == null);
        }

        public Task<CommonOperationResult> InitAsync(bool deleteDb=false)
        {
            if (deleteDb) _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            return Task.FromResult(CommonOperationResult.sayOk());
        }

        public Task<List<T>> GetItemsListAsync()
        {
            List<T> rez = new List<T>();

            IEnumerable<T> list = GetAllAsync().Result;

            foreach (T t in list)
            {
                rez.Add(t);
            }
            return Task.FromResult(rez);
        }

    }
}