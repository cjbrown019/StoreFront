using Microsoft.AspNetCore.Mvc;
using StoreFront.DATA.EF.Models;
using Microsoft.AspNetCore.Identity;
using StoreFront.UI.MVC.Models;
using Newtonsoft.Json;

namespace StoreFront.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {


        private readonly FoodStoreFrontContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ShoppingCartController(FoodStoreFrontContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            //Retrieve the contents from the Session shopping cart (stored as JSON) and 
            //convert them to C# via the Newtonsoft.Json package. After coverting to C#, 
            //we can pass the collection of cart contents back to the strongly-typed view
            //for display.

            //Retrieve the cart contents
            var sessionCart = HttpContext.Session.GetString("cart");

            //Create an empty shell for the local (C#) shopping cart
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            //Check to see if the session cart was null
            if (sessionCart == null || sessionCart.Count() == 0)
            {
                //User either hasn't put anything into the cart OR they have removed 
                //all items from it. So, we'll set the shoppingCart to an empty object
                shoppingCart = new Dictionary<int, CartItemViewModel>();

                ViewBag.Message = "There are no items in your cart.";

            }
            else
            {
                //Something is in the cart, so..,
                ViewBag.Message = null;

                //Deserialize the cart contents from JSON into C#
                shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);
            }


            return View(shoppingCart);
        }

        public JsonResult AddToCart(int id)
        {

            //Create an empty shell for our LOCAL shopping cart variable
            //int (key) > ProductID
            //CartItemViewModel (value) > Product & Qty

            Dictionary<int, CartItemViewModel> shoppingCart = null;

            #region Session Notes
            /*
             * Session is a tool available on the server-side that can store information for a user while they are actively using your site.
             * Typically the session lasts for 20 minutes (this can be adjusted in Program.cs).
             * Once the 20 minutes is up, the session variable is disposed.
             * 
             * Values that we can store in Session are limited to: string, int
             * - Because of this we have to get creative when trying to store complex objects (like CartItemViewModel).
             * To keep the info separated into properties we will convert the C# object to a JSON string.
             * */
            #endregion


            var sessionCart = HttpContext.Session.GetString("cart");

            //Check to see if the Session cart exists
            //If not, instantiate a new local collection
            if (String.IsNullOrEmpty(sessionCart))
            {
                //If the session cart didn't exist yet, instantiate a collection as a new object
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }
            else
            {
                //Cart already exists. So, transfer the existing cart items from the session cart 
                //into our local shoppingCart.

                //DeserializeObject() is a method that converts JSON to C#. We MUST tell this method 
                //which C# class to convert the JSON into (here, Dictionary<int, CartItemViewModel>)
                shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);
            }

            //Add the newly selected Product(s) to the cart
            //Retrieve the Product from the Database

            FoodStoreMenu menuItem = _context.FoodStoreMenus.Find(id);

            //Instantiate the CartItemViewModel object so we can add it to the cart
            CartItemViewModel civm = new CartItemViewModel(1, menuItem); //Adds 1 of the selected Product to the cart

            //If the product was already in the cart, increase the quantity by 1.
            //Otherwise, add the new item to the cart
            if (shoppingCart.ContainsKey(menuItem.FoodId))
            {
                //Item is already in the cart -- increase the quantity
                shoppingCart[menuItem.FoodId].Qty++;
            }
            else
            {
                //Item wasn't in the cart -- add it
                shoppingCart.Add(menuItem.FoodId, civm);
            }

            //Update the Session version of the cart 
            //Take the local copy and serialize it as JSON
            //Finally, assign that value to our Session
            string jsonCart = JsonConvert.SerializeObject(shoppingCart);
            HttpContext.Session.SetString("cart", jsonCart);

           

            return Json(new { id = id, message = "Added to cart" });
            //return RedirectToAction("Index");
        }



        public IActionResult RemoveFromCart(int id)
        {
            var sessionCart = HttpContext.Session.GetString("cart");

            //Convert the json to C#
            Dictionary<int, CartItemViewModel> shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);

            //Remove the item from the cart
            shoppingCart.Remove(id);

            if (shoppingCart.Count == 0)
            {
                HttpContext.Session.Remove("cart");

            }
            else
            {
                string jsonCart = JsonConvert.SerializeObject(shoppingCart);
                HttpContext.Session.SetString("cart", jsonCart);
            }

            return RedirectToAction("Index");
        }

        public IActionResult UpdateCart(int menuId, int qty)
        {
            //Retrieve the cart
            var sessionCart = HttpContext.Session.GetString("cart");
            //Deserialize the cart from JSON into C#
            Dictionary<int, CartItemViewModel> shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);

            if (qty <= 0)
            {
                shoppingCart[menuId].Qty = 1;
            }
            else
            {
                shoppingCart[menuId].Qty = qty;

            }
            string jsonCart = JsonConvert.SerializeObject(shoppingCart);
            HttpContext.Session.SetString("cart", jsonCart);
            return RedirectToAction("Index");
        }

        public IActionResult ConfPage()
        {
            var sessionCart = HttpContext.Session.GetString("cart");

            //Convert the json to C#
            Dictionary<int, CartItemViewModel> shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);

            //Remove the item from the cart
            foreach(var item in shoppingCart)
            {
            shoppingCart.Remove(item.Key);
            if (shoppingCart.Count == 0)
            {
                HttpContext.Session.Remove("cart");

            }
            else
            {
                string jsonCart = JsonConvert.SerializeObject(shoppingCart);
                HttpContext.Session.SetString("cart", jsonCart);
            }

            }


            return View();
        }

    }
}
