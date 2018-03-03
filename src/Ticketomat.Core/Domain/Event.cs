using System;
using System.Collections.Generic;

namespace Ticketomat.Core.Domain
{
    public class Event : Entity
    {
        private ISet<Ticket> _tickets = new HashSet<Ticket>();
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }
        public DateTime UpdateDate { get; protected set; }
        public IEnumerable<Ticket> Tickets => _tickets;

        protected Event() {}

        public Event(Guid id, string name, string description, DateTime startDate, DateTime endDate) {
            Id = id;
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            CreatedAt = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
        }

        public void AddTickets(int amount, decimal price){
            var seating = _tickets.Count + 1;
            for(var i=0; i<amount; i++){
                _tickets.Add(new Ticket(this, seating, price));
                seating++;
            }
        }
    }

}