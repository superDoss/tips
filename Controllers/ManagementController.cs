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
    public class ManagementController : Controller
    {
        // GET: 
        public IActionResult Index()
        {
            return View();
        }

    }
}
