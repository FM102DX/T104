using System;
using T109.ActiveDive.DataAccess.Abstract;

namespace T109.ActiveDive.DataAccess.Models
{
    public class BaseEntity:IBaseEntity
    {
        public Guid Id { get; set; }

        public BaseEntity()
        {
            Id = System.Guid.NewGuid();
            CreatedDateTime = DateTime.Now;
           // SysMessage = "Message101";
        }

        public BaseEntity(Guid id, DateTime createdDateTime, DateTime lastModifiedDateTime)
        {
            Id = id;
            CreatedDateTime = createdDateTime;
            LastModifiedDatTime = lastModifiedDateTime;
        }

        public string SysMessage { get; set; } // для диагностических нужд
        public string ShortTimeStamp { get { return $"{CreatedDateTime.ToShortDateString()} {CreatedDateTime.ToShortTimeString()}"; } }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastModifiedDatTime { get; set; }

        public virtual BaseEntity Clone()
        {
            return new BaseEntity() { Id = Id, SysMessage = SysMessage, CreatedDateTime = CreatedDateTime, LastModifiedDatTime = LastModifiedDatTime };
        }

        public virtual string ShortString() { return ""; }
    }
}