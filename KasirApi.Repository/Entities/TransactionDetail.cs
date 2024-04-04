using System;
using System.Collections.Generic;

namespace KasirApi.Repository.Entities
{
    public partial class TransactionDetail
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public int Price { get; set; }
        public int Qty { get; set; }
        public int? Discount { get; set; }
        public int Total { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual Transaction Transaction { get; set; } = null!;
    }
}
