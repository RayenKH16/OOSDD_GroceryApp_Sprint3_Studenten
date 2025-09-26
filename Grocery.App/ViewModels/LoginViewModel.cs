using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
namespace Grocery.App.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly GlobalViewModel _global;

        [ObservableProperty]
        private string email = "user3@mail.com";

        [ObservableProperty]
        private string password = "user3";

        [ObservableProperty]
        private string loginMessage;

        // *** VOEG DEZE 3 PROPERTIES TOE ***
        [ObservableProperty]
        private bool isPasswordHidden = true;

        [ObservableProperty]
        private string passwordToggleText = "👁️";

        public LoginViewModel(IAuthService authService, GlobalViewModel global)
        {
            _authService = authService;
            _global = global;
        }

        [RelayCommand]
        private void Login()
        {
            Client? authenticatedClient = _authService.Login(Email, Password);
            if (authenticatedClient != null)
            {
                LoginMessage = $"Welkom {authenticatedClient.Name}!";
                _global.Client = authenticatedClient;
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                LoginMessage = "Ongeldige inloggegevens.";
            }
        }

        // *** VOEG DEZE METHODE TOE ***
        [RelayCommand]
        private void TogglePasswordVisibility()
        {
            IsPasswordHidden = !IsPasswordHidden;
            PasswordToggleText = IsPasswordHidden ? "👁️" : "🙈";
        }
    }
}