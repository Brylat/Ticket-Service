using System;
using System.Threading.Tasks;

namespace Ticketomat.Infrastructure.Services
{
    public interface IUserServices
    {
         Task RegisterAsync(Guid userId, string email, string name, string password, string role = "user");
         Task Login(string email, string password);
    }
}