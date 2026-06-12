using Application;
using Data;
using Data.Context;
using Data.Seed;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Web;
using Web.Securities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDataServices(builder.Configuration);
builder.Services.ApplicationServiceProvider(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);

builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsProduction())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AmincoDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        try
        {
            dbContext.Database.Migrate();        
            LandingPageSeeder.Seed(scope.ServiceProvider); // سید دیتا (با بررسی تکراری نبودن)
            logger.LogInformation("Database migrated and seeded successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating or seeding the database.");
        }
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapOpenApi();
app.MapScalarApiReference();

app.Run();