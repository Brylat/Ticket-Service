using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ticketomat.Core.Domain;
using Ticketomat.Core.Repositories;
using Ticketomat.Infrastructure.DTO;
using Ticketomat.Infrastructure.Extensions;

namespace Ticketomat.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public EventService(IEventRepository eventRepository, IMapper mappper)
        {
            _eventRepository = eventRepository;
            _mapper = mappper;
        }
        public async Task<EventDetailsDTO> GetAsync(Guid id)
        {
            var @event = await _eventRepository.GetAsync(id);
            return _mapper.Map<EventDetailsDTO>(@event);
        }
        public async Task<EventDetailsDTO> GetAsync(string name)
        {
            var @event = await _eventRepository.GetAsync(name);
            return _mapper.Map<EventDetailsDTO>(@event);
        }
        public async Task<IEnumerable<EventDTO>> BrowseAsync(string name = null)
        {
            var events = await _eventRepository.BrowseAsync(name);
            return _mapper.Map<IEnumerable<EventDTO>>(events);
        }
        public async Task CreateAsync(Guid id, string name, string description, DateTime startDate, DateTime endDate)
        {
            var @event = await _eventRepository.GetAsync(name);
            if(@event != null)
            {
                throw new Exception($"Event named: '{name}' arleady exist.");
            }
            @event = new Event(id, name, description, startDate, endDate);
            await _eventRepository.AddAsync(@event);
         }
        
        public async Task AddTicketsAsync(Guid eventId, int amount, decimal price)
        {
            var @event = await _eventRepository.GetOrFailAsync(eventId);
            var newTickets = @event.AddTickets(amount, price);
            await _eventRepository.AddTickets(newTickets);
        }
        public async Task UpdateAsync(Guid id, string name, string description)
        {
            var @event = await _eventRepository.GetAsync(name);
            if(@event != null)
            {
                throw new Exception($"Event named: '{name}' arleady exist.");
            }
            @event = await _eventRepository.GetOrFailAsync(id);
            @event.SetName(name);
            @event.SetDescription(description);
            await _eventRepository.UpdateAsync(@event);
        }
        //TODO below
        public async Task DeleteAsync(Guid id)
        {
            var @event = await _eventRepository.GetOrFailAsync(id);
            await _eventRepository.DeleteAsync(@event);
        }
    }
}