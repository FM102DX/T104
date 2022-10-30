using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T104.Store.DataAccess.Abstract;

namespace T104.Store.Engine.Models
{
    public interface IStoreSku : IBaseEntity
    {
        //товарная позиция магазина

        //можно начать с очень простой постановки вопроса
        
        Guid Id { get; set; }

        string Alias { get; set; }
        
        //название
        string FullName { get; set; }

        //описание
        string Description { get; set; }

        //цена в базовой валюте, пока что только рубли
        double Price { get; set; }

        //фото №1 - это просто путь к картинке на сервере
        string FirstPic { get; set; }

        //фото №2
        string SecondPic { get; set; }

        //фото №3
        string ThirdPic { get; set; }


    }
}
