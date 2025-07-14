using System;
using System.Collections.Generic;

namespace MyShopManagementBO
{
    public partial class User
    {
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public bool Enabled { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
    }
}
