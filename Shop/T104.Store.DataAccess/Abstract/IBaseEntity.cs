using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using T104.Store.DataAccess.Models;
using T104.Store.Service;

namespace T104.Store.DataAccess.Abstract
{
    public interface IBaseEntity
    {
        public Guid Id { get; set; }

        public string SysMessage { get; set; } // для диагностических нужд
        public string ShortTimeStamp { get; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime LastModifiedDatTime { get; set; }

        public  BaseEntity Clone();
    }
}
