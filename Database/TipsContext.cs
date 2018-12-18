using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Tips.Models;

namespace Tips.Database
{
    public class TipsContext : DbContext
    {
        public TipsContext(DbContextOptions<TipsContext> options)
            : base(options)
        { }

        public DbSet<Tip> Tips { get; set; }
    }
}