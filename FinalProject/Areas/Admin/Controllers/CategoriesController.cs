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
//using PurchasingSystem.Services;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
       // public ICustomerRepos CustomerRepos { get; set; }
      //  public IItemRepos ItemRepos { get; set; }
        public ICategoryRepos CategoryRepos { get; set; }

        public CategoriesController(ICategoryRepos categoryRepos)
        {
            //CustomerRepos = customerRepos;
            //ItemRepos = itemRepos;
            CategoryRepos = categoryRepos;
        }
        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(CategoryRepos.GetAll());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = CategoryRepos.GetDetails(id);
                
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,Photo")] Category category)
        {
            if (ModelState.IsValid)
            {
                CategoryRepos.Insert(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = CategoryRepos.GetDetails(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] Category category)
        {
            if (id != category.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    CategoryRepos.UpdateCatg(id,category);
                 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.ID))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = CategoryRepos.GetDetails(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = CategoryRepos.GetDetails(id);
            if(category != null)
                    CategoryRepos.DeleteCatg(id);
          
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            var catgs = CategoryRepos.GetAll();
            return catgs.Any(e => e.ID == id);
        }
    }
}
