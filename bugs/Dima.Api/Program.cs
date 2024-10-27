using Dima.Api;
using Dima.Api.Common.Api;
using Dima.Api.Data;
using Dima.Api.Endpoints;
using Dima.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();



var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

var shouldApplyMigrations = Environment.GetEnvironmentVariable(Configuration.E2ETestEnv);
if (shouldApplyMigrations is not null && shouldApplyMigrations == "true")
{
    using (var Scope = app.Services.CreateScope())
    {
        Configuration.ConnectionString = "Server=127.0.0.1,37000;Database=dimadb;User Id=sa;Password=somePassw0rd!;TrustServerCertificate=True";
        var context = Scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();

    }
}

app.UseCors(ApiConfiguration.CorsPolicyName);
app.UseSecurity();
app.MapEndpoints();

app.Run();

public partial class Program { }