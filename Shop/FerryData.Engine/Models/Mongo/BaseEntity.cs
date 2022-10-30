using MongoDB.Bson.Serialization.Attributes;
using System;

namespace T104.Store.Engine.Models
{
    public class BaseEntity
    {
        [BsonId]
        public Guid Uid { get; set; }

        public string Owner { get; set; }

        public BaseEntity()
        {
            Uid = Guid.NewGuid();
        }
    }
}
