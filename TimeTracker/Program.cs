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
using TimeTracker.Configuration;
using Lib.Common.Services;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
DockerHelpers.UpdateCaCertificates();

#endif

TimeTrackerConfiguration timeTrackerConfiguration = builder.Configuration.Get<TimeTrackerConfiguration>();
builder.Services.AddSingleton<ITimeTrackerConfiguration>(timeTrackerConfiguration);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(timeTrackerConfiguration.ConnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddSingleton<DapperContext>(new DapperContext(timeTrackerConfiguration.ConnectionString));
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

//app.UseHttpsRedirection();
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
