using System;
using T109.ActiveDive.DataAccess.Models;
using T109.ActiveDive.DataAccess.Abstract;

namespace T109.ActiveDive.Core
{
    public class ActiveDiveEvent : BaseEntity, IBaseEntity
    {
        //товарная позиция магазина
        public ActiveDiveEvent() : base()
        {
        }
        public string Alias { get; set; }
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndtDateTime { get; set; }
        public string Description { get; set; }
        public string FirstPic { get; set; }
        public string SecondPic { get; set; }
        public string ThirdPic { get; set; }
        public override string ToString()
        {
            return $"{Id} {Name} ";
        }
        public override string ShortString()
        {
            return $"{Id} {Name} ";
        }
    }
}
