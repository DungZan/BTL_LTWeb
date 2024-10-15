using BTL_LTWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//database config
var connectionString = builder.Configuration.GetConnectionString("QLBanHangBtlwebContext");
builder.Services.AddDbContext<QlbangHangBtlwebContext>(options => options.UseSqlServer(connectionString));

//set up identity cookie
builder.Services.AddIdentity<TUser, IdentityRole>()
    .AddEntityFrameworkStores<QlbangHangBtlwebContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Đường dẫn tới trang đăng nhập
    options.LogoutPath = "/Account/Logout"; // Đường dẫn tới trang đăng xuất
    options.AccessDeniedPath = "/Account/AccessDenied"; // Đường dẫn khi quyền bị từ chối
});

//
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();
