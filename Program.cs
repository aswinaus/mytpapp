using AzureOpenAIChat.Data;
using AzureOpenAIChat.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace AzureOpenAIChat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Database connection
            builder.Services.AddDbContext<AzureOpenAIChatSQLVectorContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();

            // Radzen
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();

            // Service
            builder.Services.AddScoped<AzureOpenAIChatSQLVectorService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}