using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class FoodStoreMenu
    {
        public FoodStoreMenu()
        {
            FoodOrders = new HashSet<FoodOrder>();
        }

        public int FoodId { get; set; }
        public string FoodName { get; set; } = null!;
        public string? FoodDesc { get; set; }
        public decimal FoodPrice { get; set; }
        public int? InStock { get; set; }
        public int? SupplierId { get; set; }
        public int CategoryId { get; set; }
        public string? FoodImg { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<FoodOrder> FoodOrders { get; set; }
    }
}
