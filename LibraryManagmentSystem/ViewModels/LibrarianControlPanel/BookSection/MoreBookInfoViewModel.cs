using CommunityToolkit.Mvvm.ComponentModel;
using LibraryManagmentSystem.Models.Books;
using LibraryManagmentSystem.Services;

namespace LibraryManagmentSystem.ViewModels.LibrarianControlPanel.BookSection;

/// <summary>
/// MoreBookInfoViewModel, binded to MoreBookInfoView, helps with binding properties and commands to View and gets data from DB.
/// </summary>


[QueryProperty("SentBook", "SentBook")] //The book sent by the last view.
public partial class MoreBookInfoViewModel : BaseViewModel
{
    //Fields
    //Logged user info.oip
    [ObservableProperty]
    private int _currentUserId;
    [ObservableProperty]
    private string _currentUserName;

    //Sent book.
    [ObservableProperty]
    private Book _sentBook;

    //All book info.
    [ObservableProperty]
    private string _allBookInfo;

    //Constructors
    public MoreBookInfoViewModel()
    {
        //Aquire current user Id and Name.
        (CurrentUserId, CurrentUserName) = CurrentUserService.Instance.GetIdAndName();
    }

    //Methods
    partial void OnSentBookChanged(Book value)
    {
        AllBookInfo = value.GetBookInfo();
    }
}
