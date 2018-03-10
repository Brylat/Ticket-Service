using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ticketomat.Core.Repositories;
using Ticketomat.Infrastructure.DTO;

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
        public async Task<EventDTO> GetAsync(Guid id)
        {
            var @event = await _eventRepository.GetAsync(id);
            return _mapper.Map<EventDTO>(@event);
        }
        public async Task<EventDTO> GetAsync(string name)
        {
            var @event = await _eventRepository.GetAsync(name);
            return _mapper.Map<EventDTO>(@event);
        }
        public async Task<IEnumerable<EventDTO>> BrowseAsync(string name = null)
        {
            var events = await _eventRepository.BrowseAsync(name);
            return _mapper.Map<IEnumerable<EventDTO>>(events);
        }
        //TODO belowe later
        public async Task CreateAsync(Guid id, string name, string description, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
        public async Task AddTicketsAsync(Guid eventId, int amount, decimal price)
        {
            throw new NotImplementedException();
        }
        public async Task UpdateAsync(Guid id, string name, string description)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}