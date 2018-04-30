using System;
using Ticketomat.Infrastructure.DTO;

namespace Ticketomat.Infrastructure.Services
{
    public interface IJwtHandler
    {
         JwtDTO CreateToken(Guid userId, string role);
    }
}