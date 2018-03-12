using System;

namespace Ticketomat.Infrastructure.Commands.Events
{
    public class CreateEvent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}