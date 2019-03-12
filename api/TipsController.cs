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
            return await _context.Tips.Take(5).ToListAsync();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Tip>>> GetTips(TipSearchReq req)
        {
            return await _context.Tips.Take(5).ToListAsync();
        }
    }
}