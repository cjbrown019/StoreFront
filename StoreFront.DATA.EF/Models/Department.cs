using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Department
    {
        public Department()
        {
            Categories = new HashSet<Category>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;

        public virtual ICollection<Category> Categories { get; set; }
    }
}
