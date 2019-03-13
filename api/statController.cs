using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tips.Models;
using Tips.Database;
using System;

namespace Tips.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        private readonly TipsContext _context;
        public StatController(TipsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult TipsPerDay()
        {
            var grouped = _context.Tips.Where(tip => tip.CreateDate.Date > DateTime.Now)
                        .GroupBy(tip => tip.CreateDate.Date);

            TipPerDay tpd = new TipPerDay() {
                Dates = grouped.Select(x => x.Key).ToList(),
                Sum = grouped.Select(x => x.Count()).ToList()
            };

            return Ok(tpd);
        }
    }
}