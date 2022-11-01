global using System.ComponentModel.DataAnnotations;
global using static MagicVilla_Utility.SD;
global using AutoMapper;
global using MagicVilla_Web.Models.Dto;
global using MagicVilla_Web;
global using MagicVilla_Web.Models;
global using MagicVilla_Web.Services.IServices;
global using Newtonsoft.Json;
global using System.Text;
global using MagicVilla_Utility;
global using MagicVilla_Web.Services;
global using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddHttpClient<IVillaService, VillaService>();
builder.Services.AddScoped<IVillaService, VillaService>();

builder.Services.AddHttpClient<IVillaNumberService, VillaNumberService>();
builder.Services.AddScoped<IVillaNumberService, VillaNumberService>();

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
