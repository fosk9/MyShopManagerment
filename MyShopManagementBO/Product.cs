using System;
using System.Collections.Generic;

namespace MyShopManagementBO
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }
        public int CategoryId { get; set; }

        public string Image { get; set; }

        public virtual Category Category { get; set; } = null!;
    }
}
