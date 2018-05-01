using System;

namespace Ticketomat.Infrastructure.DTO
{
    public class EventDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int TicketsCount { get; set; }
        public int PurchasedTicketsCount { get; set; }
        public int AvalibleTicketsCount { get; set; }

    }
}