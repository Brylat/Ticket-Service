using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ticketomat.Core.Repositories;
using Ticketomat.Infrastructure.DTO;
using Ticketomat.Infrastructure.Extensions;

namespace Ticketomat.Infrastructure.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public TicketService(IUserRepository userRepository, IEventRepository eventRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }
        
        public async Task<TicketDTO> GetAsync(Guid userId, Guid eventId, Guid ticketId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var ticket = await _eventRepository.GetTicketOrFailAsync(eventId, ticketId);

            return _mapper.Map<TicketDTO>(ticket);
        }

        public async Task PurchaseAsync(Guid userId, Guid eventId, int amount)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var @event = await _eventRepository.GetOrFailAsync(eventId);
            @event.PurchaseTicket(user, amount);
            await _eventRepository.UpdateAsync(@event);
        }

        public async Task CancelAsync(Guid userId, Guid eventId, int amount)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var @event = await _eventRepository.GetOrFailAsync(eventId);
            @event.CancelPurchaseTicket(user, amount);
            await _eventRepository.UpdateAsync(@event);
        }

        public async Task<IEnumerable<TicketDetailsDTO>> GetForUserAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var events = await _eventRepository.BrowseAsync();
            var allTickets = new List<TicketDetailsDTO>();
            foreach(var @event in events)
            {
                var tickets = _mapper.Map<IEnumerable<TicketDetailsDTO>>(@event.GetTicketsPurchasedByUser(user)).ToList();
                tickets.ForEach(x => {
                    x.EventId = @event.Id;
                    x.EventName = @event.Name;
                });
                allTickets.AddRange(tickets);
            }

            return allTickets;
        }
    }
}