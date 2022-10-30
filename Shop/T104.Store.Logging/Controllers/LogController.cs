using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using T104.Store.Logging.Models;
using T104.Store.Service;
using T104.Store.DataAccess.Abstract;
using System.Linq;

namespace T104.Store.AssortmentWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class LogController : Controller
    {

        IAsyncRepository<LoggerMessage> _repo;

        public LogController(IAsyncRepository<LoggerMessage> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public string Get()
        {
            return "This is RI Logging controller!";
        }


        [HttpPost("log-events")]
        public void Post([FromBody] LogEvents body)
        {
            var nbrOfEvents = body.Events.Length;
            var apiKey = Request.Headers["X-Api-Key"].FirstOrDefault();

            foreach (var logEvent in body.Events)
            {

                _repo.AddAsync(new LoggerMessage { Message= logEvent.RenderedMessage } );  

                // logger.LogInformation("Message: {message}", logEvent.RenderedMessage);


            }
        }

    }
}
