using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCORE.Models;

namespace TestDAL.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public DbSet<CardItem> CardItems { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(u => new { u.UserId, u.LoginProvider, u.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(r => new { r.UserId, r.RoleId });
            modelBuilder.Entity<IdentityUserClaim<string>>().HasKey(c => c.Id);
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasKey(c => c.Id);
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
        }
    }
}
