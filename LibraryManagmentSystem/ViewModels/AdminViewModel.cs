using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagmentSystem.Models.DataBase.DBServices;
using LibraryManagmentSystem.Models.Users;
using LibraryManagmentSystem.Views;
using System.Collections.ObjectModel;

namespace LibraryManagmentSystem.ViewModels;

/// <summary>
/// The AdminViewModel, implementing the back-end logic for AdminView page.
/// </summary>

public partial class AdminViewModel : ObservableObject
{
    //Fields
    [ObservableProperty]
    private string _name;
    [ObservableProperty]
    private string _username;
    [ObservableProperty]
    private string _password;
    [ObservableProperty]
    private ObservableCollection<Librarian> _librarians;
    [ObservableProperty]
    private Librarian _librarian;


    //Constructors
    public AdminViewModel()
    {
        _librarians = new ObservableCollection<Librarian>(Task.Run(() => DBAccess<Librarian>.GetTableAsync()).Result);
    }

    //Methods
    [RelayCommand]
    public async Task InsertLibrarianAsync()
    {
        //Insert the new Librarian user in the DataBase If Inputs
        // are not Null or Empty, username doesn't exist already or cerdentials are not the same or like the admin's.
        if (!string.IsNullOrEmpty(Name) && !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
        {
            if (Admin.UsernameMatches(Username) || Username == Password)
            {
                await Shell.Current.DisplayAlert("Insert Error!", "Check if Username is different than Admin's Username and " +
                    "is not the same as the Password!", "OK");
            }
            else
            {
                if (Librarians.Any(x => x.Username == Username))
                {
                    await Shell.Current.DisplayAlert("Insert Error!", "A librarian with this Username already exists, choose another one!", "OK");
                }
                else
                {
                    Librarian librarian = new(Name, Username, Password);
                    await DBAccess<Librarian>.InsertInTableAsync(librarian);
                    Librarians.Add(librarian);
                }
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("Insert Error!", "Cannot have empty entry fields!", "OK");
        }
    }

    [RelayCommand]
    public async Task DeleteLibrarianAsync()
    {
        if (Librarian != null)
        {
            await DBAccess<Librarian>.RemoveFromTableAsync(Librarian); //Delete the Librarian user from the DataBase.
            Librarians.Remove(Librarian);
            Librarian = null;
        }
        else
        {
            await Shell.Current.DisplayAlert("Delete Error!", "No librarian selected!", "OK");
        }
    }

    [RelayCommand]
    public async Task LogoutAsync()
    {
        await Shell.Current.GoToAsync($"//{nameof(LoginView)}");
    }
}
