using System;
using System.Collections.Generic;

namespace Fernando_Guillen_Lab13Final.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Response> Responses { get; set; }
    }
}