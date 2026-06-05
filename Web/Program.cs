using Application;
using Data;
using Data.Context;
using Scalar.AspNetCore;
using Web;
using Web.Securities;
using Domain.Contract;
using Domain.Entites;
using Microsoft.Extensions.DependencyInjection;

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

// async Task SeedDatabaseAsync(IServiceProvider services)
// {
//     await using var scope = services.CreateAsyncScope();
//     var dbContext = scope.ServiceProvider.GetRequiredService<AmincoDbContext>();
//     await dbContext.Database.EnsureCreatedAsync();
//
//     var roleRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Role, long>>();
//     var userRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<User, long>>();
//     var userRoleRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<UserRole, long>>();
//     
//     if (!await roleRepo.AnyAsync(r => r.Name == "Admin"))
//         await roleRepo.AddEntity(new Role { Name = "Admin", IsActive = true });
//     if (!await roleRepo.AnyAsync(r => r.Name == "Customer"))
//         await roleRepo.AddEntity(new Role { Name = "Customer", IsActive = true });
//     await roleRepo.SaveChangesAsync();
//     
//     static string HashPassword(string password)
//     {
//         using var sha256 = System.Security.Cryptography.SHA256.Create();
//         var bytes = System.Text.Encoding.UTF8.GetBytes(password);
//         var hash = sha256.ComputeHash(bytes);
//         return Convert.ToBase64String(hash);
//     }
//
//     var adminRole = await roleRepo.GetSingleAsync(r => r.Name == "Admin");
//     if (adminRole != null && !await userRepo.AnyAsync(u => u.Email == "admin@aminco.com"))
//     {
//         var adminUser = new User
//         {
//             FullName = "مدیر سیستم",
//             Email = "admin@aminco.com",
//             PasswordHash = HashPassword("123456"),
//             IsActive = true
//         };
//         await userRepo.AddEntity(adminUser);
//         await userRepo.SaveChangesAsync();
//
//         await userRoleRepo.AddEntity(new UserRole { UserId = adminUser.Id, RoleId = adminRole.Id });
//         await userRoleRepo.SaveChangesAsync();
//     }
// }
//
// // اجرای seeding
// await SeedDatabaseAsync(app.Services);

// ==================================================

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