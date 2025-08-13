using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AuthenticationApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AuthenticationApp.ViewModels;

public partial class RegisterViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IAuthenticationService _authService;
    
    [ObservableProperty] [Required] [StringLength(100)] string _firstName = string.Empty;
    [ObservableProperty] [Required] [StringLength(100)] string _lastName = string.Empty;
    [ObservableProperty] [Required] [EmailAddress()] string _email = string.Empty;
    [ObservableProperty] [Required] [MinLength(6)] string _password = string.Empty;
    [ObservableProperty] [Required] [MinLength(6)] string _confirmPassword = string.Empty;
    
    [ObservableProperty] private string _errorMessage = string.Empty;
    [ObservableProperty] private string _successMessage = string.Empty;
    [ObservableProperty] private bool _isLoading;

    public RegisterViewModel(INavigationService navigationService, IAuthenticationService authService)
    {
        _navigationService = navigationService;
        _authService = authService;
    }

    public RegisterViewModel() : this(null!, null!)
    { }

    [RelayCommand]
    private async Task Register()
    {
        ValidateAllProperties();

        if (Password != ConfirmPassword)
        {
            ErrorMessage = "Password and confirm password do not match.";
            return;
        }
        if(HasErrors)
            return;

        IsLoading = true;
        ErrorMessage = string.Empty;
        SuccessMessage = string.Empty;

        try
        {
            if (await _authService.UserExistsAsync(Email))
            {
                ErrorMessage = "A user with this email already exists.";
                return;
            }

            var success = await _authService.RegisterAsync(FirstName, LastName, Email, Password);
            if (success)
            {
                SuccessMessage = "Registration successful.";
                await _navigationService.NavigateTo<HomeViewModel>();
            }
        }
        catch (Exception e)
        {
            ErrorMessage = $"Registration failed: {e.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task NavigateToLogin()
    {
        await _navigationService.NavigateTo<LoginViewModel>();
    }
}