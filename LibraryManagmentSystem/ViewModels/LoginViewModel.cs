using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagmentSystem.Models.DataBase.DBServices;
using LibraryManagmentSystem.Models.Users;
using LibraryManagmentSystem.Services;
using LibraryManagmentSystem.Views;
using LibraryManagmentSystem.Views.LibrarianControlPanel;

namespace LibraryManagmentSystem.ViewModels;

/// <summary>
/// The Login Page ViewModel, validates login information and proceeds to go to the InfoFilterView.
/// </summary>

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    private string _username;
    [ObservableProperty]
    private string _password;

    public LoginViewModel()
    {
    }

    [RelayCommand]
    public async Task LoginAsync()
    {
        if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
        {
            if (Admin.UserPassMatches(Username, Password))
            {
                //Clear login details from entries;
                Username = default;
                Password = default;
                
                await Shell.Current.GoToAsync($"//{nameof(AdminView)}");             
            }
            else
            {
                //Get librarians table and check if one has the correct username and password, if it does, log in.
                List<Librarian> librarians = Task.Run(() => DBAccess<Librarian>.GetTableAsync()).Result;
                Librarian librarian = librarians.FirstOrDefault(x => x.UserPassMatches(Username, Password));
                if (librarian != null)
                {
                    //Update the singleton CurrentUser class info.
                    CurrentUserService.Instance.Id = librarian.Id;
                    CurrentUserService.Instance.Name = librarian.Name;

                    //Clear login details from entries;
                    Username = default;
                    Password = default;

                    await Shell.Current.GoToAsync($"//{nameof(InfoFilterView)}");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Login Error!", "Incorrect Username or Password!", "OK");
                }
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("Login Error!", "Empty Username or Password fields!", "OK");
        }
    }
}

