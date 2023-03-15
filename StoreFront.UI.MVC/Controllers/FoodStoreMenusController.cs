using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using StoreFront.UI.MVC.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;
using X.PagedList;

namespace StoreFront.UI.MVC.Controllers
{
    public class FoodStoreMenusController : Controller
    {
        private readonly FoodStoreFrontContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FoodStoreMenusController(FoodStoreFrontContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: FoodStoreMenus
        public async Task<IActionResult> Index(string searchTerm, int categoryId = 0, int page = 1)
        {

            int pageSize = 12;

            var products = _context.FoodStoreMenus
                 .Include(p => p.Category).Include(p => p.Supplier).ToList();

            #region Optional Category Filter
            //Create a ViewData object to send a list of Categories to the View
            //(This is similar to what we can see in the Products/Create())
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");

            //Add a ViewBag variable to persist the selected category
            ViewBag.Category = 0;

            if (categoryId != 0)
            {
                 products = products.Where( p => p.CategoryId == categoryId)
                    .ToList();

                //Repopulate with the current category selected
                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", categoryId);

                //Reassign the ViewBag variable accordinly
                ViewBag.Category = categoryId;
            }
            #endregion

            #region Optional Search Filter
            if (!String.IsNullOrEmpty(searchTerm))
            {
                //searchTerm = searchTerm.ToLower();


                products = products.Where(p => p.FoodName.ToLower().Contains(searchTerm.ToLower()) ||
p.Supplier.SupplierName.ToLower().Contains(searchTerm.ToLower()) ||
p.Category.CategoryName.ToLower().Contains(searchTerm.ToLower()) ||
p.FoodDesc.ToLower().Contains(searchTerm.ToLower())).ToList();

                //ViewBag variable for the number of results

                ViewBag.NbrResults = products.Count();


                //ViewBag variable for the search term
                ViewBag.SearchTerm = searchTerm;

            }
            else
            {
                ViewBag.NbrResults = null;
                ViewBag.SearchTerm = null;
            }


            #endregion

            return View(products.ToPagedList(page, pageSize));

        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AdminProds()
        {
            var foodStoreFrontContext = _context.FoodStoreMenus.Include(f => f.Category).Include(f => f.Supplier);
            return View(await foodStoreFrontContext.ToListAsync());
        }
        public async Task<IActionResult> NewProds()
        {
            var foodStoreFrontContext = _context.FoodStoreMenus.Include(f => f.Category).Include(f => f.Supplier).OrderByDescending(f=>f.FoodId);
            return View(await foodStoreFrontContext.ToListAsync());
        }


        // GET: FoodStoreMenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FoodStoreMenus == null)
            {
                return NotFound();
            }

            var foodStoreMenu = await _context.FoodStoreMenus
                .Include(f => f.Category)
                .Include(f => f.Supplier)
                .FirstOrDefaultAsync(m => m.FoodId == id);
            if (foodStoreMenu == null)
            {
                return NotFound();
            }

            return View(foodStoreMenu);
        }

        // GET: FoodStoreMenus/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName");
            return View();
        }

        // POST: FoodStoreMenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodId,FoodName,FoodDesc,FoodPrice,InStock,SupplierId,CategoryId,FoodImg,Image")] FoodStoreMenu foodStoreMenu)
        {
            if (ModelState.IsValid)
            {



                #region File Upload - Create
                //ONLY BOTHER WITH THIS IF MODEL IS VALID
                //check to see if a file was uploaded
                if ( foodStoreMenu.Image != null)
                {
                    //There is a file upload
                    //check file type
                    string ext = Path.GetExtension(foodStoreMenu.Image.FileName);

                    //create a list of acceptable extentions

                    string[] validExt = { ".jpeg", ".jpg", ".gif", ".png" };

                    //verify the uploaded file has a matching extension
                    //AND verify the file size will work w/ out .NET app
                    //(for the length -size in bytes - you can use _ inplace of , for visual separator
                    //and it doesn't affect the actual INT value
                    if (validExt.Contains(ext.ToLower()) && foodStoreMenu.Image.Length < 4_194_383)
                    {
                        //generate a unique filename with Guid
                        foodStoreMenu.FoodImg = Guid.NewGuid() + ext;

                        //to save this file to right folder on the web server (loval OR production)
                        //To access the web root (no matter what computer this is running on) ise
                        string webRootPath = _webHostEnvironment.WebRootPath;
                        string fullImagePath = webRootPath + "/images/";//gets us to wwwroot/images

                        //create a MemoryStream to read the image int server memory
                        using (var memoryStream = new MemoryStream())
                        {
                            await foodStoreMenu.Image.CopyToAsync(memoryStream);//transfer file from request to server memory

                            using (var img = Image.FromStream(memoryStream))//setting up an Image not a file
                            {
                                //now send the image to the ImageUtility to resize for base file
                                //and also thumbnail
                                int maxImageSize = 500;//in pixels
                                int maxThumbSize = 100;

                                ImageUtility.ResizeImage(fullImagePath, foodStoreMenu.FoodImg, img, maxImageSize, maxThumbSize);//resize for new base image & thumbnail & saved
                            }
                        }

                    }

                }
                else
                {
                    //in this case, no file was uploaded, so assign a default filename
                    //IF YOU HAVEN'T already, go get that default file and put it with images!
                    foodStoreMenu.FoodImg = "NoImage.png";
                }
                #endregion



                _context.Add(foodStoreMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", foodStoreMenu.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", foodStoreMenu.SupplierId);
            return View(foodStoreMenu);
        }

        // GET: FoodStoreMenus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FoodStoreMenus == null)
            {
                return NotFound();
            }

            var foodStoreMenu = await _context.FoodStoreMenus.FindAsync(id);
            if (foodStoreMenu == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", foodStoreMenu.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", foodStoreMenu.SupplierId);
            return View(foodStoreMenu);
        }

        // POST: FoodStoreMenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodId,FoodName,FoodDesc,FoodPrice,InStock,SupplierId,CategoryId,FoodImg,Image")] FoodStoreMenu foodStoreMenu)
        {
            if (id != foodStoreMenu.FoodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                #region EDIT File Upload
                //retain old image file name so we can delete if a new file was uploaded
                string oldImageName = foodStoreMenu.FoodImg;

                //Check if the user uploaded a file
                if (foodStoreMenu.Image != null)
                {
                    //get the file's extension
                    string ext = Path.GetExtension(foodStoreMenu.Image.FileName);

                    //list valid extensions
                    string[] validExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    //check the file's extension against the list of valid extensions
                    if (validExts.Contains(ext.ToLower()) && foodStoreMenu.Image.Length < 4_194_303)
                    {
                        //generate a unique file name
                        foodStoreMenu.FoodImg = Guid.NewGuid() + ext;
                        //build our file path to save the image
                        string webRootPath = _webHostEnvironment.WebRootPath;
                        string fullPath = webRootPath + "/images/";

                        //Delete the old image
                        if (oldImageName != "noimage.png")
                        {
                            ImageUtility.Delete(fullPath, oldImageName);
                        }

                        //Save the new image to webroot
                        using (var memoryStream = new MemoryStream())
                        {
                            await foodStoreMenu.Image.CopyToAsync(memoryStream);
                            using (var img = Image.FromStream(memoryStream))
                            {
                                int maxImageSize = 500;
                                int maxThumbSize = 100;
                                ImageUtility.ResizeImage(fullPath, foodStoreMenu.FoodImg, img, maxImageSize, maxThumbSize);
                            }
                        }

                    }
                }
                #endregion
                try
                {
                    _context.Update(foodStoreMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodStoreMenuExists(foodStoreMenu.FoodId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", foodStoreMenu.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", foodStoreMenu.SupplierId);
            return View(foodStoreMenu);
        }

        // GET: FoodStoreMenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FoodStoreMenus == null)
            {
                return NotFound();
            }

            var foodStoreMenu = await _context.FoodStoreMenus
                .Include(f => f.Category)
                .Include(f => f.Supplier)
                .FirstOrDefaultAsync(m => m.FoodId == id);
            if (foodStoreMenu == null)
            {
                return NotFound();
            }

            return View(foodStoreMenu);
        }

        // POST: FoodStoreMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FoodStoreMenus == null)
            {
                return Problem("Entity set 'FoodStoreFrontContext.FoodStoreMenus'  is null.");
            }
            var foodStoreMenu = await _context.FoodStoreMenus.FindAsync(id);
            if (foodStoreMenu != null)
            {
                _context.FoodStoreMenus.Remove(foodStoreMenu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminProds));
        }

        private bool FoodStoreMenuExists(int id)
        {
          return _context.FoodStoreMenus.Any(e => e.FoodId == id);
        }
    }
}
