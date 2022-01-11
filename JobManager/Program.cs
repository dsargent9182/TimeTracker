using Hangfire;
using Hangfire.SqlServer;
using JobManager;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire((sp, config) =>
{
    config.UseSqlServerStorage(@"Server=192.168.1.197,1433;Database=HangFireTest;Pooling=true;Persist Security Info=True;User ID=sa;PWD=Pa$$w0rd2021!;TrustServerCertificate=True", new SqlServerStorageOptions() { PrepareSchemaIfNecessary = true});
});

builder.Services.AddMvc();

builder.Services.AddHangfireServer();
var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHangfireDashboard("/hang", new DashboardOptions
    { 
         DashboardTitle = "MyCoolDashboard",
         Authorization = new []{ new DashboardAuthorizationFilter()}
    });
});

app.Run();