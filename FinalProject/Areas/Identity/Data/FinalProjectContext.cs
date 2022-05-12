using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
 
using FinalProject.Models;
using PurchasingSystem.Areas.Identity.Data;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace FinalProject.Data;

public class FinalProjectContext : IdentityDbContext<AppUser>
{
    public FinalProjectContext(DbContextOptions<FinalProjectContext> options)
        : base(options)
    {

    }
   // public DbSet<AppUser> appUsers { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    // public DbSet<AppUser> appUsers { get; set; }
    
}

