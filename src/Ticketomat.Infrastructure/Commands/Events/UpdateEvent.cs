using System;

namespace Ticketomat.Infrastructure.Commands.Events
{
    public class UpdateEvent
    {
        public Guid EventId {get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}