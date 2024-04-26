using System;
using System.Collections.Generic;

namespace KasirApi.Repository.Entities
{
    public partial class Member
    {
        public Member()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string MemberNumber { get; set; } = null!;
        public string Nik { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateOnly BirthDate { get; set; }
        public string BirthPlace { get; set; } = null!;
        public char? Gender { get; set; }
        public DateTime JoinDate { get; set; }
        public string? Address { get; set; }
        public int DataStatusId { get; set; }
        public int Point { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
