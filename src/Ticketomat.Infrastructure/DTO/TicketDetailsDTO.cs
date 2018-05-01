using System;

namespace Ticketomat.Infrastructure.DTO
{
    public class TicketDetailsDTO : TicketDTO
    {
        public Guid EventId { get; set; }
        public string EventName { get; set; }
    }
}