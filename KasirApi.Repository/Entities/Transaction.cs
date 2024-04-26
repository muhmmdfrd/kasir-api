using System;
using System.Collections.Generic;

namespace KasirApi.Repository.Entities
{
    public partial class Transaction
    {
        public Transaction()
        {
            TransactionDetails = new HashSet<TransactionDetail>();
        }

        public int Id { get; set; }
        public string ReferenceNumber { get; set; } = null!;
        public int? MemberId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Member? Member { get; set; }
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
