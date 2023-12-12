using Microsoft.EntityFrameworkCore;
using ShopBee.Data;
using ShopBee.Repository;
using ShopBee.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.Cookies;
using ShopBee.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<AdminAuthentication>();

builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddTransient<AdminAuthentication>();
builder.Services.AddTransient<CustomerAuthentication>();
builder.Services.AddTransient<StoreAuthentication>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
app.MapRazorPages();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.UseSession();
/*app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=User}/{action=Login}/{id?}");*/

 app.UseEndpoints(endpoints =>
 {
    endpoints.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=User}/{action=Login}/{id?}");
 });

app.Run();
