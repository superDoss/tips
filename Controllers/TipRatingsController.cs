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
    public class TipRatingsController : Controller
    {
        private readonly TipsContext _context;

        public TipRatingsController(TipsContext context)
        {
            _context = context;
        }

        // GET: TipRatings
        public async Task<IActionResult> Index()
        {
            var tipsContext = _context.TipRatings.Include(t => t.Tip).Include(t => t.User);
            return View(await tipsContext.ToListAsync());
        }

        // GET: TipRatings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipRating = await _context.TipRatings
                .Include(t => t.Tip)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipRating == null)
            {
                return NotFound();
            }

            return View(tipRating);
        }

        // GET: TipRatings/Create
        public IActionResult Create()
        {
            ViewData["TipId"] = new SelectList(_context.Tips, "Id", "Content");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: TipRatings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipId,UserId,RateValue")] TipRating tipRating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipRating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipId"] = new SelectList(_context.Tips, "Id", "Content", tipRating.TipId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", tipRating.UserId);
            return View(tipRating);
        }

        // GET: TipRatings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipRating = await _context.TipRatings.FindAsync(id);
            if (tipRating == null)
            {
                return NotFound();
            }
            ViewData["TipId"] = new SelectList(_context.Tips, "Id", "Content", tipRating.TipId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", tipRating.UserId);
            return View(tipRating);
        }

        // POST: TipRatings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipId,UserId,RateValue")] TipRating tipRating)
        {
            if (id != tipRating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipRating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipRatingExists(tipRating.Id))
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
            ViewData["TipId"] = new SelectList(_context.Tips, "Id", "Content", tipRating.TipId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", tipRating.UserId);
            return View(tipRating);
        }

        // GET: TipRatings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipRating = await _context.TipRatings
                .Include(t => t.Tip)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipRating == null)
            {
                return NotFound();
            }

            return View(tipRating);
        }

        // POST: TipRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipRating = await _context.TipRatings.FindAsync(id);
            _context.TipRatings.Remove(tipRating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipRatingExists(int id)
        {
            return _context.TipRatings.Any(e => e.Id == id);
        }
    }
}
