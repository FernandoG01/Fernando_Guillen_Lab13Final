using System;
using System.Collections.Generic;

namespace Fernando_Guillen_Lab13Final.Models
{
    public class Role
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}