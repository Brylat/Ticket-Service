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

        [HttpGet("{eventId}")]
        public async Task<IActionResult> Get(Guid eventId)
        {
            var @event = await _eventService.GetAsync(eventId);
            if(@event == null)
            {
                return NotFound();
            }
            return Json(@event);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateEvent command)
        {
            var eventId = Guid.NewGuid();
            await _eventService.CreateAsync(eventId, command.Name,
                command.Description, command.StartDate, command.EndDate);
            await _eventService.AddTicketsAsync(eventId, command.Tickets, command.Price);
            return Created($"/events/{eventId}", null);
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> Put(Guid eventId, [FromBody]UpdateEvent command)
        {
            await _eventService.UpdateAsync(eventId, command.Name, command.Description);
            return NoContent();
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> Delete(Guid eventId)
        {
            await _eventService.DeleteAsync(eventId);
            return NoContent();
        }
    }
}