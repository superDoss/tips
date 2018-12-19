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
    public class UserRatingsController : Controller
    {
        private readonly TipsContext _context;

        public UserRatingsController(TipsContext context)
        {
            _context = context;
        }

        // GET: UserRatings
        public async Task<IActionResult> Index()
        {
            var tipsContext = _context.UserRatings.Include(u => u.RatedUser).Include(u => u.User);
            return View(await tipsContext.ToListAsync());
        }

        // GET: UserRatings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRating = await _context.UserRatings
                .Include(u => u.RatedUser)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRating == null)
            {
                return NotFound();
            }

            return View(userRating);
        }

        // GET: UserRatings/Create
        public IActionResult Create()
        {
            ViewData["RatedUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: UserRatings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,RatedUserId,Reputation")] UserRating userRating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RatedUserId"] = new SelectList(_context.Users, "Id", "FirstName", userRating.RatedUserId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userRating.UserId);
            return View(userRating);
        }

        // GET: UserRatings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRating = await _context.UserRatings.FindAsync(id);
            if (userRating == null)
            {
                return NotFound();
            }
            ViewData["RatedUserId"] = new SelectList(_context.Users, "Id", "FirstName", userRating.RatedUserId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userRating.UserId);
            return View(userRating);
        }

        // POST: UserRatings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,RatedUserId,Reputation")] UserRating userRating)
        {
            if (id != userRating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRatingExists(userRating.Id))
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
            ViewData["RatedUserId"] = new SelectList(_context.Users, "Id", "FirstName", userRating.RatedUserId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userRating.UserId);
            return View(userRating);
        }

        // GET: UserRatings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRating = await _context.UserRatings
                .Include(u => u.RatedUser)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRating == null)
            {
                return NotFound();
            }

            return View(userRating);
        }

        // POST: UserRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userRating = await _context.UserRatings.FindAsync(id);
            _context.UserRatings.Remove(userRating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRatingExists(int id)
        {
            return _context.UserRatings.Any(e => e.Id == id);
        }
    }
}
