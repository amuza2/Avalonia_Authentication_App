using System;
using System.IO;
using AuthenticationApp.Data;
using AuthenticationApp.Services;
using AuthenticationApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection collection)
    {
        // Register the DbContext
        collection.AddDbContext<ApplicationDbContext>(options =>
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var dbPath = Path.Combine(baseDirectory, "AuthenticationApp.db");
            options.UseSqlite($"Data Source={dbPath}");
        });
        
        // Register Services
        collection.AddTransient<IAuthenticationService, AuthenticationService>();
        collection.AddSingleton<INavigationService, NavigationService>();
        
        // Register ViewModels
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddTransient<LoginViewModel>();
        collection.AddTransient<RegisterViewModel>();
        collection.AddTransient<HomeViewModel>();
    }
}