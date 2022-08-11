using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagmentSystem.Views;

namespace LibraryManagmentSystem.ViewModels;

/// <summary>
/// AppShellViewModel, binded to AppShell, helps with binding properties and commands to View and gets data from DB.
/// </summary>

public partial class AppShellViewModel : ObservableObject
{
    //Constructor
    public AppShellViewModel()
    {
    }

    //Methods
    [RelayCommand]
    public async Task LogoutAsync()
    {
        await Shell.Current.GoToAsync($"//{nameof(LoginView)}");
    }
}
