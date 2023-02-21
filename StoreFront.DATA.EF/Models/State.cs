using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class State
    {
        public State()
        {
            Orders = new HashSet<Order>();
            Suppliers = new HashSet<Supplier>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
