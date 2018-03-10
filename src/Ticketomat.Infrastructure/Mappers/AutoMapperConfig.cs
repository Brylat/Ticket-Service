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
                cfg.CreateMap<Event, EventDTO>()
                    .ForMember(x => x.TicketCount, m => m.MapFrom(p => p.Tickets.Count()));
            })
            .CreateMapper();
    }
}