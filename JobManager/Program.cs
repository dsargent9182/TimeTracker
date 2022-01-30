using Hangfire;
using Hangfire.SqlServer;
using JobManager;


var builder = WebApplication.CreateBuilder(args);

string? connectionString = Environment.GetEnvironmentVariable("HangfireConnectionString");

builder.Services.AddHangfire((sp, config) =>
{
	config.UseSqlServerStorage(connectionString, new SqlServerStorageOptions() { PrepareSchemaIfNecessary = true });
	config.UseSimpleAssemblyNameTypeSerializer();
	config.UseRecommendedSerializerSettings();
});

builder.Services.AddMvc();

builder.Services.AddHangfireServer();

var app = builder.Build();

app.Use((context, next) =>
{
	context.Request.Scheme = "https";
	return next();
});

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseRouting();
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
	Authorization = new[] { new DashboardAuthorizationFilter() },
	DashboardTitle = "Job Dashboard"
});

app.UseAuthentication();

app.Run();