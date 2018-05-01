using System.Linq;
using AutoMapper;
using Ticketomat.Core.Domain;
using Ticketomat.Infrastructure.DTO;

namespace Ticketomat.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Event, EventDTO>();
                cfg.CreateMap<Event, EventDetailsDTO>();
                cfg.CreateMap<Ticket, TicketDTO>();
                cfg.CreateMap<Ticket, TicketDetailsDTO>();
                cfg.CreateMap<User, AccountDTO>();
            })
            .CreateMapper();
    }
}