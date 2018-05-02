using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Ticketomat.Core.Domain;
using Ticketomat.Core.Repositories;

namespace Ticketomat.Infrastructure.Repositories
{
    public class DatabaseEventRepository : BaseDatabaseRepository, IEventRepository
    {
        public async Task AddAsync(Event @event)
        {
            try {  
                DynamicParameters parameters = new DynamicParameters();  
                parameters.Add("@Id", @event.Id); 
                parameters.Add("@Name", @event.Name);  
                parameters.Add("@Description", @event.Description);  
                parameters.Add("@CreateAt", @event. CreatedAt);  
                parameters.Add("@StartDate", @event.StartDate); 
                parameters.Add("@EndDate", @event.EndDate); 
                parameters.Add("@UpdateDate", @event.UpdateDate);  
                SqlMapper.Execute(con, "AddEvent", param: parameters, commandType: CommandType.StoredProcedure);  
                await Task.CompletedTask;  
            } catch (Exception ex) {  
                throw ex;  
            }
        }

        public async Task AddTickets(IEnumerable<Ticket> tickets)
        {
            foreach(var ticket in tickets){
                try {  
                DynamicParameters parameters = new DynamicParameters();  
                parameters.Add("@Id", ticket.Id); 
                parameters.Add("@EventId", ticket.EventId);  
                parameters.Add("@Seating", ticket.Seating);  
                parameters.Add("@Price", ticket.Price); 
                SqlMapper.Execute(con, "AddTicket", param: parameters, commandType: CommandType.StoredProcedure);  
            } catch (Exception ex) {  
                throw ex;  
            }
            }
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Event>> BrowseAsync(string name = "")
        {
            try {  
                DynamicParameters parameters = new DynamicParameters();  
                parameters.Add("@Name", name);
                var result = SqlMapper.Query<Event>(con, "BrowseEvent", param: parameters, commandType: CommandType.StoredProcedure);
                foreach(var even in result) even.AddTicketsCollection(await GetTicketsAsync(even.Id));
                return await Task.FromResult(result);
            } catch (Exception ex) {  
                throw ex;  
            }
        }

        public async Task DeleteAsync(Event @event)
        {
            try {  
                DynamicParameters parameters = new DynamicParameters();  
                parameters.Add("@Id", @event.Id);  
                SqlMapper.Execute(con, "DeleteEvent", param: parameters, commandType: CommandType.StoredProcedure);  
                await Task.CompletedTask;  
            } catch (Exception ex) {  
                throw ex;  
            }
        }

        public async Task<Event> GetAsync(Guid id)
        {
            try {  
                DynamicParameters parameters = new DynamicParameters();  
                parameters.Add("@Id", id);
                var result = SqlMapper.Query<Event>(con, "GetEventById", param: parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                result?.AddTicketsCollection(await GetTicketsAsync(result.Id));
                return await Task.FromResult(result);
            } catch (Exception ex) {  
                throw ex;  
            } 
        }

        public async Task<Event> GetAsync(string name)
        {
            try {  
                DynamicParameters parameters = new DynamicParameters();  
                parameters.Add("@Name", name);
                var result = SqlMapper.Query<Event>(con, "GetEventByName", param: parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                result?.AddTicketsCollection(await GetTicketsAsync(result.Id)); 
                return await Task.FromResult(result);
            } catch (Exception ex) {  
                throw ex;  
            } 
        }

        private async Task<IEnumerable<Ticket>> GetTicketsAsync(Guid eventId) {
            try {  
                DynamicParameters parameters = new DynamicParameters();  
                parameters.Add("@EventId", eventId);
                var result = SqlMapper.Query<Ticket>(con, "GetTicketsByEventId", param: parameters, commandType: CommandType.StoredProcedure); 
                return await Task.FromResult(result);
            } catch (Exception ex) {  
                throw ex;  
            }
        }

        public async Task UpdateAsync(Event @event)
        {
            try {  
                DynamicParameters parameters = new DynamicParameters();  
                parameters.Add("@Id", @event.Id); 
                parameters.Add("@Name", @event.Name);  
                parameters.Add("@Description", @event.Description);  
                parameters.Add("@CreateAt", @event. CreatedAt);  
                parameters.Add("@StartDate", @event.StartDate); 
                parameters.Add("@EndDate", @event.EndDate); 
                parameters.Add("@UpdateDate", @event.UpdateDate);  
                SqlMapper.Execute(con, "UpdateEvent", param: parameters, commandType: CommandType.StoredProcedure);  
                await Task.CompletedTask;  
            } catch (Exception ex) {  
                throw ex;  
            }
        }

        public async Task PurchaseTicket(IEnumerable<Ticket> tickets, Guid userId)
        {
            var timeNow = DateTime.UtcNow;
            foreach(var ticket in tickets){
                try {  
                DynamicParameters parameters = new DynamicParameters();  
                parameters.Add("@TicketId", ticket.Id); 
                parameters.Add("@PurchasedAt", timeNow);  
                parameters.Add("@UserId", userId);
                SqlMapper.Execute(con, "PurchaseTicket", param: parameters, commandType: CommandType.StoredProcedure);  
                } catch (Exception ex) {  
                    throw ex;  
                }
            }
            await Task.CompletedTask;
        }

        public async Task CanceledTicket(IEnumerable<Ticket> tickets)
        {
            foreach(var ticket in tickets){
                try {  
                DynamicParameters parameters = new DynamicParameters();  
                parameters.Add("@TicketId", ticket.Id);
                SqlMapper.Execute(con, "CanceledTicket", param: parameters, commandType: CommandType.StoredProcedure);  
                } catch (Exception ex) {  
                    throw ex;  
                }
            }
            await Task.CompletedTask;
        }
    }
}