using Microsoft.EntityFrameworkCore;
using NET105_BANSACH.Models; 

var builder = WebApplication.CreateBuilder(args);

#pragma warning disable ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'

// Note: This code will be deprecated after Configurations are imported back.....

var configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();

#pragma warning restore ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
builder.Services.AddDbContext<AppDBContext>(Options => Options.UseSqlServer
(configuration?.GetConnectionString("DSCongNhan")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(option =>
option.IdleTimeout = TimeSpan.FromSeconds(300)
);

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
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
