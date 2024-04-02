using ActivateUserWithToken.Data;
using ActivateUserWithToken.Helpers;
using ActivateUserWithToken.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<HelperPathProvider>();
builder.Services.AddTransient<HelperMails>();
builder.Services.AddTransient<HelperUploadFiles>();

string connectionstring = builder.Configuration.GetConnectionString("SqlBookify");
builder.Services.AddTransient<Repository>();
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionstring));

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
