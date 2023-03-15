using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.DATA.EF.Models
{
    public class CategoryMetadata
    {
        public int CategoryId { get; set; }
        [Required,MaxLength(20)]
        public string CategoryName { get; set; } = null!;
        public int? DepartmentId { get; set; }
    }
    public class DepartmentMetadata
    {
        public int DepartmentId { get; set; }
        [Required, MaxLength(20)]
        public string DepartmentName { get; set; } = null!;
    }
    public class FoodStoreMenuMetadata
    {
        public int FoodId { get; set; }
        [Required, MaxLength(20),Display(Name ="Name")]
        public string FoodName { get; set; } = null!;
        [MaxLength(200), Display(Name ="Description")]
        public string? FoodDesc { get; set; }
        [Required,DataType(DataType.Currency)]
        public decimal FoodPrice { get; set; }
        public int? InStock { get; set; }
        public int? SupplierId { get; set; }
        [Required, Display(Name ="Supplier")]
        public int CategoryId { get; set; }
        [ MaxLength(50)]
        public string? FoodImg { get; set; }
    }

    public class OrderMetadata
    {
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required,MaxLength(100),Display(Name ="Delivery Address")]
        public string DeliverAddress { get; set; } = null!;
        [Required,MaxLength(5),Display(Name ="Zip")]
        public string DeliverZip { get; set; } = null!;
        [Required,Display(Name ="State")]
        public int StateId { get; set; }
    }


    public class SupplierMetadata
    {
        public int SupplierId { get; set; }
        [Required,MaxLength(100)]
        public string SupplierName { get; set; } = null!;
        [Required, MaxLength(100)]
        public string Address { get; set; } = null!;
        [Required, MaxLength(100)]
        public string City { get; set; } = null!;
       
        public int StateId { get; set; }
        [Required, MaxLength(5)]
        public string? Zip { get; set; }
        [MaxLength(100),DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
    }

    public class UserDetailsMetadata
    {
        public int UserId { get; set; }
        public string UserFname { get; set; } = null!;
        public string UserLname { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public int UserPoints { get; set; }
    }
}
