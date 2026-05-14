using Application;
using Data;
using Scalar.AspNetCore;
using Web;
using Web.Securities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add services from other layers
builder.Services.AddDataServices(builder.Configuration);
builder.Services.ApplicationServiceProvider(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);

builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});
// Add API explorer
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// app.UseRotativa();

// Ensure database is created (for development)
// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<AmincoDbContext>();
//     await dbContext.Database.EnsureCreatedAsync();
//
//     var roleRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Role, long>>();
//
//     if (!await roleRepo.AnyAsync(r => r.Name == "Admin"))
//         await roleRepo.AddEntity(new Role { Name = "Admin", IsActive = true });
//
//     if (!await roleRepo.AnyAsync(r => r.Name == "Customer"))
//         await roleRepo.AddEntity(new Role { Name = "Customer", IsActive = true });
//
//     await roleRepo.SaveChangesAsync();
// }

// Configure the HTTP request pipeline.
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

// 🔥 Map OpenAPI endpoint (serves the openapi.json file)
app.MapOpenApi();

// 🔥 Map Scalar UI to the '/scalar' endpoint
app.MapScalarApiReference();

app.Run();