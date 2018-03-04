using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ticketomat.Infrastructure.DTO;

namespace Ticketomat.Infrastructure.Services
{
    public interface IEventService
    {
        Task<EventDTO> GetAsync(Guid id);
        Task<EventDTO> GetAsync(string name);
        Task<IEnumerable<EventDTO>> BrowseAsync(string name = null);
        Task CreateAsync(Guid id, string name, string description, DateTime startDate, DateTime endDate);
        Task AddTicketsAsync(Guid eventId, int amount, decimal price);
        Task UpdateAsync(Guid id, string name, string description);
        Task DeleteAsync(Guid id);
    }
}