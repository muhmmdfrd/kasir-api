using System;
using System.Collections.Generic;

namespace KasirApi.Repository.Entities
{
    public partial class Product
    {
        public Product()
        {
            TransactionDetails = new HashSet<TransactionDetail>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public int Stock { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
