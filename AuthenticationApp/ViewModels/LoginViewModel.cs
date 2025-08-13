using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AuthenticationApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AuthenticationApp.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    private readonly IAuthenticationService _authService;
    private readonly INavigationService _navService;

    [ObservableProperty]
    [Required]
    [EmailAddress()]
    private string _email = string.Empty;
    
    [ObservableProperty] 
    [Required] 
    [MinLength(6)]
    private string _password = string.Empty;
    
    [ObservableProperty] private string _errorMessage = string.Empty;

    [ObservableProperty] private bool _isLoading;
    
    public LoginViewModel(IAuthenticationService authService, INavigationService navService)
    {
        _authService = authService;
        _navService = navService;
    }
    public LoginViewModel() :this(null!, null!) { }

    [RelayCommand]
    private async Task Login()
    {
        ValidateAllProperties();
        if (HasErrors)
            return;
        IsLoading = true;
        ErrorMessage = string.Empty;
        
        try
        {
            var user = await _authService.LoginAsync(Email, Password);
            if (user != null)
            {
                await _navService.NavigateTo<HomeViewModel>();
            }
            else
            {
                ErrorMessage = "Invalid Email or Password";
            }
        }
        catch (Exception e)
        {
            ErrorMessage = $"Login Failed: {e.Message}";
        }
        finally
        {
            IsLoading = false;  
        }
    }

    [RelayCommand]
    private async Task NavigateToRegister()
    {
        await _navService.NavigateTo<RegisterViewModel>();
    }
}