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

        public DbSet<Tips.Models.Tip> Tips { get; set; }

        public DbSet<Tips.Models.User> Users { get; set; }

        public DbSet<Tips.Models.Category> Categories { get; set; }

        public DbSet<Tips.Models.TipCategory> TipCategories { get; set; }
        public DbSet<Tips.Models.TipRating> TipRatings { get; set; }
        public DbSet<Tips.Models.UserRating> UserRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRating>()
                .HasOne(usrRtng => usrRtng.User)
                .WithMany(usr => usr.UserRatings)
                .HasForeignKey(usrRtng => usrRtng.UserId)
                .IsRequired();

            modelBuilder.Entity<UserRating>()
                .HasOne(usrRtng => usrRtng.RatedUser)
                .WithMany(usr => usr.RatedUserRatings)
                .HasForeignKey(usrRtng => usrRtng.RatedUserId)
                .IsRequired();

        }
    }
}