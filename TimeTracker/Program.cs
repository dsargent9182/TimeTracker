using DataLayer.Context;
using DataLayer.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Data;
using TimeTracker.BizLayer;
using Microsoft.AspNetCore.Authorization;
using OfficeOpenXml;
using System.Globalization;
using TimeTracker.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddSingleton<DapperContext>(new DapperContext(connectionString));
builder.Services.AddSingleton<ITimeTrackerRepository, TimeTrackerRepository>();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<ITimeTrackerService, TimeTrackerService>();

builder.Services.AddControllersWithViews();

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
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}").RequireAuthorization();

app.MapRazorPages();

CultureInfo.CurrentCulture = new CultureInfo("en-US");
CultureInfo.CurrentUICulture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CurrentCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CurrentUICulture;

app.Run();
