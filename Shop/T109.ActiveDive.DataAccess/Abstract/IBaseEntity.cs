using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using T109.ActiveDive.DataAccess.Models;


namespace T109.ActiveDive.DataAccess.Abstract
{
    public interface IBaseEntity
    {
        public Guid Id { get; set; }

        public string SysMessage { get; set; }
        public string ShortTimeStamp { get; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime LastModifiedDatTime { get; set; }

        public  BaseEntity Clone();
    }
}
