using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; // Required for PasswordHasher
using QuestPDF.Infrastructure;
using RestuarantOrderSystem.Data;
using RestuarantOrderSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// QuestPDF License setup
QuestPDF.Settings.License = LicenseType.Community;

// Database Context Setup with PendingModelChangesWarning suppression for EF Core 9
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning)));

// Register Custom Application Services
builder.Services.AddScoped<PdfReportService>();
builder.Services.AddScoped<RecommendationService>();

// Cookie Authentication & Authorization Policies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Customer/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireStaff", policy => policy.RequireRole("Admin", "Staff"));
    options.AddPolicy("RequireCustomer", policy => policy.RequireRole("Customer"));
});

// Configure Razor Pages & Session State
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Admin", "RequireAdmin");
    options.Conventions.AuthorizeFolder("/Staff", "RequireStaff");

    // Only require login for personal member history, keeping Menu, Cart, and Checkout open for guests
    options.Conventions.AuthorizePage("/Customer/Orders", "RequireCustomer");

    // Allow unauthenticated access to login and registration pages explicitly
    options.Conventions.AllowAnonymousToPage("/Admin/Login");
    options.Conventions.AllowAnonymousToPage("/Staff/Login");
    options.Conventions.AllowAnonymousToPage("/Customer/Login");
    options.Conventions.AllowAnonymousToPage("/Customer/Register");
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// ==========================================
// AUTOMATIC DATABASE SEEDING & MIGRATION
// ==========================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    // Automatically apply pending migrations on startup
    context.Database.Migrate();

    // Standalone hasher to securely hash passwords for custom User models
    var passwordHasher = new PasswordHasher<object>();
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

// Set Customer Menu as the default landing page (Root URL redirection)
app.MapGet("/", async context =>
{
    context.Response.Redirect("/Customer/Menu");
    await Task.CompletedTask;
});

app.MapRazorPages();
app.Run();