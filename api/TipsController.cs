using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Tips.Models;
using Tips.Database;
using tips.api;

namespace Tips.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipsController : ControllerBase
    {
        private readonly TipsContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TipsController(TipsContext context,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpGet]
        //[Route("api/tips/")]
        public async Task<ActionResult<IEnumerable<Tip>>> GetTips()
        {
            return await _context.Tips.OrderByDescending(x => x.CreateDate).Take(4).ToListAsync();
        }

        [HttpPost("rate")]
        //[Route("api/tips/rate")]
        
        public async Task<ActionResult> RateTips([FromBody]TipRateReq tipRateReq)
        {
            var sysUserId = _userManager.GetUserId(User);
            TipRating tipRate = new TipRating();
            tipRate.TipId = tipRateReq.TipId;
            tipRate.UserId = _context.Users.FirstOrDefault(u => u.SysUserId == sysUserId).Id;
            tipRate.RateValue = tipRateReq.RateValue;
            _context.Add(tipRate);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("search")]
        public async Task<ActionResult<IEnumerable<Tip>>> SearchTips(TipSearchReq req)
        {
            var result = _context.Tips.AsQueryable();

            if (req.Username != "")
            {
                result = result.Where(tip => tip.User.fullName == req.Username);
            }

            if (req.Category != "")
            {
                result = result.Where(tip => tip.TipCategories.Any(ctgr => ctgr.Category.Name == req.Category));
            }

            result = result.Where(tip => tip.TipRatings.Any(rtg => rtg.RateValue >= req.Rate));
            

            return await result.OrderByDescending(x => x.CreateDate).Take(5).ToListAsync();
        }

        [HttpPost("create")]
        public async Task<OkResult> CreateTip(TipCreateReq req)
        {
            var tip = new Tip(req);
            tip.User = FindUser();
            tip.TipCategories = req.Category.Select(category => _context.TipCategories.FirstOrDefault(ccategory => ccategory.Category.Name == category)).ToList();
            _context.Add(tip);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        private User FindUser()
        {
            var sysUserId = this._userManager.GetUserId(User);
            var user = _context.Users.FirstOrDefault(u => u.SysUserId == sysUserId);
            return user;

        }
    }
}