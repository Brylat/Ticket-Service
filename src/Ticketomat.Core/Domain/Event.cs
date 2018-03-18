using System;
using System.Collections.Generic;
using System.Linq;

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
        public IEnumerable<Ticket> PurchasedTickets => Tickets.Where(x => x.Purchased);
        public IEnumerable<Ticket> AwalibleTickets => Tickets.Except(PurchasedTickets);
        protected Event() {}

        public Event(Guid id, string name, string description, DateTime startDate, DateTime endDate) {
            Id = id;
            SetName(name);
            SetDescription(description);
            StartDate = startDate;
            EndDate = endDate;
            CreatedAt = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
        }

        public void SetName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new Exception($"Event with id: '{Id}' an not have an empty name");
            }
            Name = name;
            UpdateDate =  DateTime.UtcNow;
        }

        public void SetDescription(string description)
        {
            if(string.IsNullOrWhiteSpace(description))
            {
                throw new Exception($"Event with id: '{Id}' an not have an empty name");
            }
            Description = description;
            UpdateDate =  DateTime.UtcNow;
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