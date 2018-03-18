using System.Collections.Generic;

namespace Ticketomat.Infrastructure.DTO
{
    public class EventDetailsDTO : EventDTO
    {
        public IEnumerable<TicketDTO> Tiskets { get; set; }
    }
}