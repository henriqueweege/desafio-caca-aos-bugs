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


if (builder.Configuration.GetValue<bool>("ShouldRunMigrations"))
{
    using (var Scope = app.Services.CreateScope())
    {
        var context = Scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
}

app.UseCors(ApiConfiguration.CorsPolicyName);
app.UseSecurity();
app.MapEndpoints();

app.Run();

public partial class Program { }