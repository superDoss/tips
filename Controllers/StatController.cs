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
    public class StatController : Controller
    {
        private readonly TipsContext _context;

        public StatController(TipsContext context)
        {
            _context = context;
        }

        // GET: Tips
        public IActionResult Index()
        {
            return View();
        }

    }
}
