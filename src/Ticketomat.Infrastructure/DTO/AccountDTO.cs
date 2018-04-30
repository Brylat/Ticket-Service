using System;

namespace Ticketomat.Infrastructure.DTO
{
    public class AccountDTO
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}