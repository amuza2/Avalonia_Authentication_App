using System;
using System.Threading.Tasks;
using AuthenticationApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationApp.Services;

public class NavigationService : INavigationService
{
    private MainWindowViewModel? _mainWindowViewModel;
    private readonly IServiceProvider _serviceProvider;
    
    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task NavigateTo<T>() where T : ViewModelBase
    {
        if(_mainWindowViewModel == null)
            return Task.CompletedTask;
        
        var vm = _serviceProvider.GetRequiredService<T>();
        _mainWindowViewModel.CurrentViewModel = vm;
        return Task.CompletedTask;
    }

    public void SetMainViewModel(MainWindowViewModel mainViewModel)
    {
        _mainWindowViewModel = mainViewModel;
    }
}