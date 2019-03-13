using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tips.Database;
using Tips.Models;
using Tweetinvi;

namespace Tips.Controllers
{
    [Authorize]
    public class TipsController : Controller
    {
        private readonly TipsContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TipsController(TipsContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Tips
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Index()
        {
            var tipsContext = _context.Tips.Include(t => t.User);
            return View(await tipsContext.ToListAsync());
        }

        public async Task<IActionResult> MyTips()
        { 
            var sysUserId = _userManager.GetUserId(User);
            var userId = _context.Users.FirstOrDefault(u => u.SysUserId == sysUserId)?.Id; 
            if (userId != null)
            {
                var tipsContext = _context.Tips.Where(t => t.UserId == userId).Include(t => t.User);
                return View("index", await tipsContext.ToListAsync());
            }
            else
            {
                return RedirectToAction("Create", "Users");
            }       
            
        }

        // GET: Tips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tip = await _context.Tips
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tip == null)
            {
                return NotFound();
            }

            return View(tip);
        }

        // GET: Tips/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        // POST: Tips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,UserId,ImagePath,VideoPath,Location")] Tip tip)
        {
            if (ModelState.IsValid)
            {
                tip.CreateDate = DateTime.Now;
                _context.Add(tip);
                await _context.SaveChangesAsync();

                Tweet.PublishTweet($"{tip.Title}: {tip.Content}\nhttps://localhost:5001/tips/Details/{tip.Id}");
                return RedirectToAction(nameof(MyTips));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", tip.UserId);
            return View(tip);
        }

        // GET: Tips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tip = await _context.Tips.FindAsync(id);
            if (tip == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", tip.UserId);
            return View(tip);
        }

        // POST: Tips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,UserId,ImagePath,VideoPath")] Tip tip)
        {
            if (id != tip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tip.CreateDate = DateTime.Now;
                    _context.Update(tip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipExists(tip.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MyTips));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", tip.UserId);
            return View(tip);
        }

        // GET: Tips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tip = await _context.Tips
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tip == null)
            {
                return NotFound();
            }

            return View(tip);
        }

        // POST: Tips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tip = await _context.Tips.FindAsync(id);
            _context.Tips.Remove(tip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipExists(int id)
        {
            return _context.Tips.Any(e => e.Id == id);
        }
    }
}
