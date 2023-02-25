using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;

namespace StoreFront.UI.MVC.Controllers
{
    public class FoodStoreMenusController : Controller
    {
        private readonly FoodStoreFrontContext _context;

        public FoodStoreMenusController(FoodStoreFrontContext context)
        {
            _context = context;
        }

        // GET: FoodStoreMenus
        public async Task<IActionResult> Index()
        {
            var foodStoreFrontContext = _context.FoodStoreMenus.Include(f => f.Category).Include(f => f.Supplier);
            return View(await foodStoreFrontContext.ToListAsync());
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AdminProds()
        {
            var foodStoreFrontContext = _context.FoodStoreMenus.Include(f => f.Category).Include(f => f.Supplier);
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
        public async Task<IActionResult> Create([Bind("FoodId,FoodName,FoodDesc,FoodPrice,InStock,SupplierId,CategoryId,FoodImg")] FoodStoreMenu foodStoreMenu)
        {
            if (ModelState.IsValid)
            {
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
        public async Task<IActionResult> Edit(int id, [Bind("FoodId,FoodName,FoodDesc,FoodPrice,InStock,SupplierId,CategoryId,FoodImg")] FoodStoreMenu foodStoreMenu)
        {
            if (id != foodStoreMenu.FoodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            return RedirectToAction(nameof(Index));
        }

        private bool FoodStoreMenuExists(int id)
        {
          return _context.FoodStoreMenus.Any(e => e.FoodId == id);
        }
    }
}
