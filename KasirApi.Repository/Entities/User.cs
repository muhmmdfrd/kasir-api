using System;
using System.Collections.Generic;

namespace KasirApi.Repository.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string Nip { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int DataStatusId { get; set; }
        public int RoleId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Role Role { get; set; } = null!;
    }
}
