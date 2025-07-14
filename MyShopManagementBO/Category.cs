using System;
using System.Collections.Generic;

namespace MyShopManagementBO
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }

        public override string? ToString()
        {
            return Name;
        }
    }
}
