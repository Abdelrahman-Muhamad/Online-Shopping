#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using FinalProject.Services;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ItemsController : Controller
    {
        public IItemRepos ItemRepos { get; set; }
        public ICategoryRepos CategoryRepos { get; set; }

        public ItemsController( IItemRepos itemRepos, ICategoryRepos categoryRepos)
        {
            ItemRepos = itemRepos;
            CategoryRepos = categoryRepos;
        }
        // GET: Items
        public async Task<IActionResult> Index()
        {
            ViewData["CategoryID"] = new SelectList(CategoryRepos.GetAll(), "ID", "Name");
            return View(ItemRepos.GetAll());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = ItemRepos.GetDetails(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = CategoryRepos.GetAll().Find(c => c.ID == item.CategoryID);
            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(CategoryRepos.GetAll(), "ID", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Photopath,Description,Price,InStock,Details,CategoryID")] Item item)
        {
        
              ItemRepos.Insert(item);
              return RedirectToAction(nameof(Index));
         
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = ItemRepos.GetDetails(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(CategoryRepos.GetAll(), "ID", "Name", item.CategoryID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Photopath,Details,Description,Price,InStock,CategoryID")] Item item)
        {
            if (id != item.ID)
            {
                return NotFound();
            }

            
                try
                {
                    ItemRepos.UpdateItem(id,item);
                  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["CategoryID"] = new SelectList(CategoryRepos.GetAll(), "ID", "Name", item.CategoryID);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = ItemRepos.GetDetails(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ItemRepos.DeleteItem(id);
            return RedirectToAction("Index");
        }

        private bool ItemExists(int id)
        {

            return ItemRepos.GetAll().Any(e => e.ID == id);
        }
    }
}
