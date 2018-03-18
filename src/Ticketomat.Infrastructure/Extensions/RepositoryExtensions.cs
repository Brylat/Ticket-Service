using System;
using System.Threading.Tasks;
using Ticketomat.Core.Domain;
using Ticketomat.Core.Repositories;

namespace Ticketomat.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Event> GetOrFailAsync(this IEventRepository repository, Guid id)
        {
            var @event = await repository.GetAsync(id);
            if(@event == null)
            {
                throw new Exception($"Event with id: '{id}' does not exist.");
            }

            return @event;
        }
    }
}