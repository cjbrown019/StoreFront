using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Category
    {
        public Category()
        {
            FoodStoreMenus = new HashSet<FoodStoreMenu>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public int? DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual ICollection<FoodStoreMenu> FoodStoreMenus { get; set; }
    }
}
