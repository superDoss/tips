using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tips.Database;
using Tips.Models;

namespace Tips.Controllers
{
    public class TipCategoriesController : Controller
    {
        private readonly TipsContext _context;

        public TipCategoriesController(TipsContext context)
        {
            _context = context;
        }

        // GET: TipCategories
        public async Task<IActionResult> Index()
        {
            var tipsContext = _context.TipCategories.Include(t => t.Category).Include(t => t.Tip);
            return View(await tipsContext.ToListAsync());
        }

        // GET: TipCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipCategory = await _context.TipCategories
                .Include(t => t.Category)
                .Include(t => t.Tip)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipCategory == null)
            {
                return NotFound();
            }

            return View(tipCategory);
        }

        // GET: TipCategories/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["TipId"] = new SelectList(_context.Tips, "Id", "Content");
            return View();
        }

        // POST: TipCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipId,CategoryId")] TipCategory tipCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", tipCategory.CategoryId);
            ViewData["TipId"] = new SelectList(_context.Tips, "Id", "Content", tipCategory.TipId);
            return View(tipCategory);
        }

        // GET: TipCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipCategory = await _context.TipCategories.FindAsync(id);
            if (tipCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", tipCategory.CategoryId);
            ViewData["TipId"] = new SelectList(_context.Tips, "Id", "Content", tipCategory.TipId);
            return View(tipCategory);
        }

        // POST: TipCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipId,CategoryId")] TipCategory tipCategory)
        {
            if (id != tipCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipCategoryExists(tipCategory.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", tipCategory.CategoryId);
            ViewData["TipId"] = new SelectList(_context.Tips, "Id", "Content", tipCategory.TipId);
            return View(tipCategory);
        }

        // GET: TipCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipCategory = await _context.TipCategories
                .Include(t => t.Category)
                .Include(t => t.Tip)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipCategory == null)
            {
                return NotFound();
            }

            return View(tipCategory);
        }

        // POST: TipCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipCategory = await _context.TipCategories.FindAsync(id);
            _context.TipCategories.Remove(tipCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipCategoryExists(int id)
        {
            return _context.TipCategories.Any(e => e.Id == id);
        }
    }
}
