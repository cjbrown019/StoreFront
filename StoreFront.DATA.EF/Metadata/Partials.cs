using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.DATA.EF.Models
{
    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Category { }
    [ModelMetadataType(typeof(DepartmentMetadata))]
    public partial class Department { }
    [ModelMetadataType(typeof(FoodStoreMenuMetadata))]
    public partial class FoodStoreMenu { }
    [ModelMetadataType(typeof(OrderMetadata))]
    public partial class Order { }
    [ModelMetadataType(typeof(SupplierMetadata))]
    public partial class Suppliers { }
    [ModelMetadataType(typeof(UserDetailsMetadata))]
    public partial class UserDetail { }
}
