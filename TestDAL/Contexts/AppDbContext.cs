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
    }
}
