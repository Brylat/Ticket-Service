using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketomat.Infrastructure.Services;

namespace Ticketomat.Api.Controllers
{
    [Authorize]
    [Route("events/{eventId}/tickets")]
    public class TicketsController : ApiControlerBase
    {
        private readonly ITicketService _ticketService;
        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> Get(Guid eventId, Guid ticketId)
        {
            var ticket = await _ticketService.GetAsync(UserId, eventId, ticketId);
            if(ticket == null)
            {
                return NotFound();
            }
            return Json(ticket);
        }

        [HttpPost("purchase/{amount}")]
        public async Task<IActionResult> Post(Guid eventId, int amount)
        {
            await _ticketService.PurchaseAsync(UserId, eventId, amount);
            
            return NoContent();
        }

        [HttpDelete("cancel/{amount}")]
        public async Task<IActionResult> Delete(Guid eventId, int amount)
        {
            await _ticketService.CancelAsync(UserId, eventId, amount);
            
            return NoContent();
        }
    }
}