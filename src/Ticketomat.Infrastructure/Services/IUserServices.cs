using System;
using System.Threading.Tasks;
using Ticketomat.Infrastructure.DTO;

namespace Ticketomat.Infrastructure.Services
{
    public interface IUserServices
    {
        Task<AccountDTO> GetAcountAsync(Guid userId);
         Task RegisterAsync(Guid userId, string email, string name, string password, string role = "user");
         Task<TokenDTO> LoginAsync(string email, string password);
    }
}