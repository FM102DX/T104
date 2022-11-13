using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using T109.ActiveDive.Core;

namespace T109.ActiveDive.EventCatalogue.EventCatalogueApi
{
    [ApiController]
    [Route("api/eventdata")]
    public class EventDisplayController : Controller
    {
        public DiveEventInMemoryManager Manager { get; set; }
        public Serilog.ILogger Logger { get; set; }

        public EventDisplayController(DiveEventInMemoryManager manager, Serilog.ILogger logger)
        {
            Manager = manager;
            Logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new EventDisplayViewModel
            {
                ItemList = Manager.GetAllAsList(),
                CssPath = @"/css/ControllerStyle.css",
             }; 
            return View("EventBase", viewModel);
        }

        [HttpGet]
        [Route("getall/")]
        public IEnumerable<ActiveDiveEvent> GetFrontendAssort()
        {
            return Manager.GetAll();
        }

        [HttpGet]
        [Route("search/{searchText}")]
        public IEnumerable<ActiveDiveEvent> SearchFrontednAssort(string searchText)
        {
            return Manager.Search(searchText);
        }

        [HttpGet]
        [Route("GetByIdOrNull/{id}")]
        public ActiveDiveEvent GetByIdOrNull(Guid id)
        {
            return Manager.Repository.GetByIdOrNull(id);
        }
        

    }
}
