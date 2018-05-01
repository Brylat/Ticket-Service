using System;

namespace Ticketomat.Core.Domain
{
    public class User : Entity
    {
        public string Role { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public DateTime CreateAt { get; protected set; }

        protected User(){ }

        public User(Guid id, string role, string name, string email, string password)
        {
            Id = id;
            SetRole(role);
            SetName(name);
            SetEmail(email);
            SetPassword(password);
            CreateAt = DateTime.UtcNow;
        }

        public void SetName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new Exception($"User with id: '{Id}' an not have an empty name.");
            }
            Name = name;
        }

        public void SetEmail(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
            {
                throw new Exception($"User with id: '{Id}' an not have an empty email.");
            }
            Email = email;
        }

        public void SetRole(string role)
        {
            if(string.IsNullOrWhiteSpace(role))
            {
                throw new Exception($"User with id: '{Id}' an not have an empty role.");
            }
            Role = role;
        }

        public void SetPassword(string password)
        {
            if(string.IsNullOrWhiteSpace(password))
            {
                throw new Exception($"User with id: '{Id}' an not have an empty password.");
            }
            Password = password;
        }

    }
}