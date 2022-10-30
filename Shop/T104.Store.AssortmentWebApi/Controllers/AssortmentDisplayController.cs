using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using T104.Store.AssortmentWebApiData;
using T104.Store.AssortmentWebApi.Models;
using T104.Store.Engine.Models;
using System.Threading.Tasks;
using System;

namespace T104.Store.AssortmentWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/AssortDemo")]
    public class AssortmentDisplayController : Controller
    {
        public StoreSkuInMemoryManager Manager { get; set; }
        public Serilog.ILogger Logger { get; set; }

        public AssortmentDisplayController(StoreSkuInMemoryManager manager, Serilog.ILogger logger)
        {
            Manager = manager;
            Logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new AssortmentDisplayViewModel
            {
                StoreSkuList = Manager.GetAllAsList(),
                CssPath = @"/css/ControllerStyle.css",
                PicPath = @"/ShopContent/",
                //CssPath = @"/Views/AssortmentDisplay/style.css"
            }; 
            return View("AssortBase", viewModel);
        }

        [HttpGet]
        [Route("getall/")]
        public IEnumerable<StoreSku> GetFrontendAssort()
        {
            //просто вернуть всю коллекцию ассортимента
            return Manager.GetAll();
        }

        [HttpGet]
        [Route("search/{searchText}")]
        public IEnumerable<StoreSku> SearchFrontednAssort(string searchText)
        {
            return Manager.Search(searchText);
        }

        [Route("GetByIdOrNull/{id}")]
        public StoreSku GetByIdOrNull(Guid id)
        {
            return Manager.Repository.GetByIdOrNull(id);
        }

    }
}
