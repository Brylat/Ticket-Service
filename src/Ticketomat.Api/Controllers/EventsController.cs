using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ticketomat.Infrastructure.Commands.Events;
using Ticketomat.Infrastructure.Services;

namespace Ticketomat.Api.Controllers
{
    [Route("[controller]")]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTask(string name)
        {
            var events = await _eventService.BrowseAsync(name);
            return Json(events);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateEvent command)
        {
            var eventId = Guid.NewGuid();
            await _eventService.CreateAsync(eventId, command.Name,
                command.Description, command.StartDate, command.EndDate);
            return Created($"/events/{eventId}", null);
        }
    }
}