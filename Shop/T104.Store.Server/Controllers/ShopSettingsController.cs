using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using T104.Store.DataAccess.Abstract;
using T104.Store.Engine.Models;
using T104.Store.Service;
using Serilog;
using T104.Store.Engine.Environment;

namespace OtusREST.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShopSettingsController : Controller
    {

        IAsyncRepository<ShopSetting>  _repo;
        Serilog.ILogger _logger;

        public ShopSettingsController(IAsyncRepository<ShopSetting> repo, Serilog.ILogger logger)
        {
            _repo = repo;
            _logger = logger;
            //_logger.Information($"ShopSettings controller activated");
        }


        /// <summary>
        /// Диагностическое сообщение контроллера
        /// </summary>
        [HttpGet]
        public string Get()
        {
            return "This is ShopSettings controller!";
        }

        /// <summary>
        /// Количество объектов в репозитории (async)
        /// </summary>
        [HttpGet("count/")]
        public async Task<int> Count()
        {
            return await _repo.Count;
        }


        /// <summary>
        /// Существует ли объект с таким id (async)
        /// </summary>
        /// 
        [HttpGet("exists/{id}")]
        public async Task<bool> Exists(Guid id)
        {
            return await _repo.Exists(id);
        }

        /// <summary>
        /// Достать объект по id или вернуть null (async)
        /// </summary>
        /// 
        [HttpGet("GetByIdOrNull/{id}")]
        public async Task<ShopSetting> GetByIdOrNullAsync(Guid id)
        {
            return await _repo.GetByIdOrNullAsync(id);
        }

        /// <summary>
        /// Получить список всех объектов (async)
        /// </summary>
        [HttpGet]
        [Route("getall/")]
        public async Task<IEnumerable<ShopSetting>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }


        /// <summary>
        /// Добавить объект (async)
        /// </summary>
        [HttpPost]
        public async Task<CommonOperationResult> AddAsync([FromBody] ShopSetting shopSetting)
        {
            return await _repo.AddAsync(shopSetting);
        }


        /// <summary>
        /// Изменить существующий объект (async)
        /// </summary>
        [HttpPut]
        public async Task<CommonOperationResult> Modify([FromBody] ShopSetting shopSetting)
        {
            _logger.Information($"saving shopSetting {shopSetting.FullName}={shopSetting.Value}");
            return await _repo.UpdateAsync(shopSetting);
        }

        /// <summary>
        /// Удалить объект с данным id (async)
        /// </summary>
        /// 
        [HttpDelete("{id}")]
        public async Task<CommonOperationResult> Delete(Guid id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}
