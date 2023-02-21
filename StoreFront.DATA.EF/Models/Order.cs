using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Order
    {
        public Order()
        {
            FoodOrders = new HashSet<FoodOrder>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string DeliverAddress { get; set; } = null!;
        public string DeliverZip { get; set; } = null!;
        public int StateId { get; set; }

        public virtual State State { get; set; } = null!;
        public virtual UserDetail User { get; set; } = null!;
        public virtual ICollection<FoodOrder> FoodOrders { get; set; }
    }
}
