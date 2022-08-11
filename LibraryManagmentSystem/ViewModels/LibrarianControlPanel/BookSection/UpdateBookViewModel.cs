using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagmentSystem.Models.Books;
using LibraryManagmentSystem.Services;

namespace LibraryManagmentSystem.ViewModels.LibrarianControlPanel.BookSection;

/// <summary>
/// UpdateBookViewModel, binded to UpdateBookView, helps with binding properties and commands to View and gets data from DB.
/// Updates the data for a specific Book object.
/// </summary>

[QueryProperty("SentBook", "SentBook")] //The book sent by the last view.
public partial class UpdateBookViewModel : BaseViewModel
{
    //Fields
    //Picker info.
    [ObservableProperty]
    private string _infoToChange;

    //IsVisible bindings.
    [ObservableProperty]
    private bool _nameVisible;
    [ObservableProperty]
    private bool _typeOfBookVisible;
    [ObservableProperty]
    private bool _authorVisible;
    [ObservableProperty]
    private bool _editionVisible;
    [ObservableProperty]
    private bool _rackNoVisible;
    [ObservableProperty]
    private bool _buttonVisible;

    //Logged user info.
    [ObservableProperty]
    private int _currentUserId;
    [ObservableProperty]
    private string _currentUserName;

    //Book info.
    [ObservableProperty]
    private string _allBookInfo;

    [ObservableProperty]
    private Book _sentBook;

    //Constructors
    public UpdateBookViewModel()
    {
        //Aquire current user Id and Name.
        (CurrentUserId, CurrentUserName) = CurrentUserService.Instance.GetIdAndName();
    }

    //Methods
    //OnPropertyChangedMethods
    partial void OnInfoToChangeChanged(string value)
    {
        ButtonVisible = true;
        AllBookInfo = SentBook.GetBookInfo();
        switch (InfoToChange) //On picker selected change, also change the visability of element.
        {
            case "Name":
                ChangeVisabilities(true, false, false, false, false);
                break;
            case "Book Type":
                ChangeVisabilities(false, true, false, false, false);
                break;
            case "Author":
                ChangeVisabilities(false, false, true, false, false);
                break;
            case "Edition":
                ChangeVisabilities(false, false, false, true, false);
                break;
            case "Rack Number":
                ChangeVisabilities(false, false, false, false, true);
                break;
            default:
                break;
        }
    }

    private void ChangeVisabilities(bool name, bool typeOfBook, bool author, bool edition, bool rackNo)
    {
        NameVisible = name;
        TypeOfBookVisible = typeOfBook;
        AuthorVisible = author;
        EditionVisible = edition;
        RackNoVisible = rackNo;
    }

    private async Task UpdateDBAndLeave()
    {
        await UpdateItemAsync(SentBook);
        await Shell.Current.DisplayAlert("Update Sucessful!", "Book info has been updated sucesfully.", "OK");
        await Shell.Current.GoToAsync($"..");
    }

    [RelayCommand]
    public async Task UpdateBookAsync()
    {
        if (SentBook != null)
        {
            bool alertAnswer = await Shell.Current.DisplayAlert("Confirm Update?",
            $"You are about to UPDATE the book!, confirm?", "YES", "NO");

            if (alertAnswer == false)
            {
                return;
            }

            //If the book has no null or empty properties, proceed to update in DB and go back. 
            if (SentBook.HasNullOrEmptyProperty())
            {
                await Shell.Current.DisplayAlert("Update Error!", "Cannot have empty entry fields!", "OK");
                return;
            }
            await UpdateDBAndLeave();
        }
    }
}
