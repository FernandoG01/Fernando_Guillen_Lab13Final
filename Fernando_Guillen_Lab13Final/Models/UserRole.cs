using System;

namespace Fernando_Guillen_Lab13Final.Models
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public DateTime AssignedAt { get; set; }
    }
}