using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tips.Models;
using Tips.Database;

namespace Tips.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipsController : ControllerBase
    {
        private readonly TipsContext _context;
         public TipsController(TipsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tip>>> GetTips()
        {
<<<<<<< HEAD
            return await _context.Tips.Take(4).ToListAsync();
        }

        // [HttpGet("search")]
        // public async Task<ActionResult<IEnumerable<Tip>>> GetTips(TipSearchReq req)
        // {
        //     return await _context.Tips.Take(5).ToListAsync();
        // }
=======
            return await _context.Tips.OrderByDescending(x => x.CreateDate).Take(5).ToListAsync();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Tip>>> GetTips(TipSearchReq req)
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
>>>>>>> 7937537dfe79228722c9b7c65952229b04d2a445
    }
}