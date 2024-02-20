using Microsoft.EntityFrameworkCore;
using MedicSystem.Core.Application;
using MedicSystem.Infrastructure.Persistence;
using MedicSystem.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<ValidarSesion, ValidarSesion>();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Index}/{id?}");

app.Run();
