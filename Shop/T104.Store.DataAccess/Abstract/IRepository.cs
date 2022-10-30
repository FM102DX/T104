using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using T104.Store.DataAccess.Models;
using T104.Store.Service;

namespace T104.Store.DataAccess.Abstract
{
    public interface IRepository<T> where T : IBaseEntity
    {
        // подробнее https://metanit.com/sharp/entityframework/3.13.php

        public IEnumerable<T> GetAll();

        public List<T> GetItemsList();

        public T GetByIdOrNull(Guid id);

        public int Count { get; }

        public  bool Exists(Guid id);

        public CommonOperationResult Add(T t);

        public CommonOperationResult Update(T t);

        public CommonOperationResult Delete(Guid id);

        public CommonOperationResult Init(bool deleteDb = false);

    }
}
