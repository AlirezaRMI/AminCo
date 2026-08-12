using Application;
using Data;
using Data.Context;
using Data.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using Web;
using Web.Securities;

var builder = WebApplication.CreateBuilder(args);

// ====== Services ======
builder.Services.AddDataServices(builder.Configuration);
builder.Services.ApplicationServiceProvider(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// ====== OpenAPI ======
builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});
builder.Services.AddEndpointsApiExplorer();

// ====== JWT Settings ======
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.SecretKey))
    throw new Exception("JwtSettings not configured properly");

// ====== Authentication ======
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.Request.Cookies["accessToken"];
            if (!string.IsNullOrEmpty(token))
                context.Token = token;
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// ====== Migration & Seeding ======
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AmincoDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        dbContext.Database.Migrate();
        LandingPageSeeder.Seed(scope.ServiceProvider);
        logger.LogInformation("Database migrated and seeded successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
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

app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapReverseProxy();
}
else
{
    app.MapFallbackToFile("index.html");
}

// ====== OpenAPI & Scalar ======
app.MapOpenApi();
app.MapScalarApiReference();

app.Run();