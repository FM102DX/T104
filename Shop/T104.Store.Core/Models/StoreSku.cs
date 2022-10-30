using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T104.Store.DataAccess.Abstract;
using T104.Store.DataAccess.Models;

namespace T104.Store.Engine.Models
{
    public class StoreSku : BaseEntity, IStoreSku
    {
        //товарная позиция магазина
        public StoreSku():base()
        {
            
        }

        public string Alias { get; set; }

        //название
        public string FullName { get; set; }

        //описание
        public string Description { get; set; }

        //цена в базовой валюте, пока что только рубли
        public double Price { get; set; }


        //фото №1 - это просто путь к картинке на сервере
        public string FirstPic { get; set; }

        //фото №2
        public string SecondPic { get; set; }

        //фото №3
        public string ThirdPic { get; set; }



        public override string ToString()
        {
            return $"{Id} {Alias} {FullName} {Price}";
        }
        public override string ShortString()
        {
            return $"{Id} {Alias} {Price}";
        }
    }
}
