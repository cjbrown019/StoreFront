using StoreFront.DATA.EF.Models;

namespace StoreFront.UI.MVC.Models
{
    public class CartItemViewModel
    {
        //FIELDS
        //Variables used to store information
        //Always private
        //Only necessary if we need custom business rules

        //PROPERTIES
        //Publicly-visible "gatekeepers" of the fields
        //Always public
        //Can define custom business rules OR use shorthand syntax for "automatic properties"

        public int Qty { get; set; }

        public  FoodStoreMenu  MenuItem { get; set; }

        //Primitives > Any class with a single value (int, bool, char, decimal, etc.)
        //Complex Datatypes > Any class with multiple properties, each with their own values

        //Above, we have a complex datatype (Product) as a property of another complex datatype (CartItemViewModel).
        //This is an example of a development concept called "Containment."


        //CONSTRUCTORS
        //Special method(s) used to construct objects of this type
        //Always public, always named the same as the class followed by parens ()

        //Fully-Qualified Constructor
        public CartItemViewModel(int qty, FoodStoreMenu menuItem)
        {
            //Assignment
            //Property = parameter;
            Qty = qty;
            MenuItem = menuItem;
        }

        //METHODS
        //Custom sets of code we can arrange to be executed when called upon
    }
}
