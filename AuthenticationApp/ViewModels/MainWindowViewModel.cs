using System.Threading.Tasks;
using AuthenticationApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AuthenticationApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    [ObservableProperty] private ViewModelBase? _currentViewModel;
    
    public MainWindowViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        ((NavigationService)navigationService).SetMainViewModel(this);
        _ = Task.Run(async () => await _navigationService.NavigateTo<LoginViewModel>());
    }
    public MainWindowViewModel():this( null!) { }
}