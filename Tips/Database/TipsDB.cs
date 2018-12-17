using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Tips.Models;

namespace Tips.Database
{
    public class TipsDB : DbContext
    {
        public TipsDB(DbContextOptions<TipsDB> options)
            : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Tip> Tips { get; set; }
        public DbSet<User> Users { get; set; }
    }
}