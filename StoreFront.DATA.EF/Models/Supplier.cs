using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            FoodStoreMenus = new HashSet<FoodStoreMenu>();
        }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        
        public int StateId { get; set; }
        public string? Zip { get; set; }
        public string? Phone { get; set; }

        public virtual State State { get; set; } = null!;
        public virtual ICollection<FoodStoreMenu> FoodStoreMenus { get; set; }
    }
}
