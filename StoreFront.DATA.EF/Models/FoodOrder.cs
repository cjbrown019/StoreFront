using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class FoodOrder
    {
        public int FoodOrderId { get; set; }
        public int FoodId { get; set; }
        public int OrderId { get; set; }

        public virtual FoodStoreMenu Food { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
