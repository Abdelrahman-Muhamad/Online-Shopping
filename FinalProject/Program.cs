using FinalProject;
using FinalProject.AuthenticationService;
using FinalProject.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using FinalProject.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using PurchasingSystem.Areas.Identity.Data;
using Microsoft.SqlServer.Management.Smo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<FinalProjectContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<PurchaseDbContext>(op =>
         op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<FinalProjectContext>();



builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);
builder.Services.AddAuthentication().AddFacebook(facebookOptions =>
{
    facebookOptions.AppId = "803065677332825";
    facebookOptions.AppSecret = "47b9ddd3e3c8a609094c832f73d715bb";
}).AddGoogle(
    googleOptions =>
{
    googleOptions.ClientId = "590270143314-50cjnvk09ag810bi77jctpkv4c8sh3dp.apps.googleusercontent.com";
    googleOptions.ClientSecret = "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";
}
).AddMicrosoftAccount(microsoftOptions =>
{
    microsoftOptions.ClientId = "e9875bee-1798-4ebc-85c7-9066d81066bf";
    microsoftOptions.ClientSecret = "IWc7Q~qgfJMbbXoHxpv2e.2A5r_BdLNdzKhji";
});


builder.Services.AddControllersWithViews();

//builder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });
//builder.Services.AddScoped<ICustomerRepos, CustomerServiceRepos>();
builder.Services.AddScoped<IItemRepos, ItemServiceRepos>();
builder.Services.AddScoped<ICategoryRepos, CategoryServiceRepos>();
builder.Services.AddScoped<IOrderRepos, OrderServiceRepos>();
builder.Services.AddScoped<IOrdersItemsRepos,OrdersItemsServiceRepos>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "CustArea",
    pattern: "{area:exists}/{controller=Customer}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    app.MapRazorPages();

app.Run();
