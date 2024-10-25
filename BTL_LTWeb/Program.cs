using BTL_LTWeb.Models;
using Microsoft.EntityFrameworkCore;
using BTL_LTWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database config
var connectionString = builder.Configuration.GetConnectionString("QLBanDoThoiTrangContext");
builder.Services.AddDbContext<QLBanDoThoiTrangContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddTransient<EmailService>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// Add Authentication using Cookie
builder.Services.AddAuthentication("MyCookieAuthentication")
    .AddCookie("MyCookieAuthentication", options =>
    {
        options.LoginPath = "/Account/Login"; 
        options.LogoutPath = "/Home/Index";
        options.AccessDeniedPath = "/Home/Home";
        options.SlidingExpiration = true;
    });
    

// Add Authorization
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add Authentication and Authorization middleware
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.Run();
