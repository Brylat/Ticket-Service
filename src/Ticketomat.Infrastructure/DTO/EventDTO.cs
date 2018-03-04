using System;

namespace Ticketomat.Infrastructure.DTO
{
    public class EventDTO
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int TicketCount { get; set; }

    }
}