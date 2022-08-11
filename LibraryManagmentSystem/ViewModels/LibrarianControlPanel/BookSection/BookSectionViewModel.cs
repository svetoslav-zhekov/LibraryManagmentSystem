using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagmentSystem.Models.Books;
using LibraryManagmentSystem.Services;
using LibraryManagmentSystem.Views.LibrarianControlPanel.BookSection;
using System.Collections.ObjectModel;

namespace LibraryManagmentSystem.ViewModels.LibrarianControlPanel.BookSection;

/// <summary>
/// BookSectionViewModel, binded to BookSectionView, helps with binding properties and commands to View and gets data from DB.
/// </summary>

public partial class BookSectionViewModel : BaseViewModel
{
    //Fields
    //Logged user info.
    [ObservableProperty]
    private int _currentUserId;
    [ObservableProperty]
    private string _currentUserName;

    //Book info.
    [ObservableProperty]
    private List<Book> _booksAll;
    [ObservableProperty]
    private ObservableCollection<Book> _books;
    [ObservableProperty]
    private Book _book;

    //Constructors
    public BookSectionViewModel()
    {
        //Aquire current user Id and Name.
        (CurrentUserId, CurrentUserName) = CurrentUserService.Instance.GetIdAndName();

        _booksAll = Task.Run(() => GetItemsAsync<Book>()).Result;
        _books = new ObservableCollection<Book>(_booksAll);
    }

    //Methods
    [RelayCommand]
    public void PerformBookSearch(string bookName)
    {
        //Updates the ObservableCollection binding bound to the View's ListView.         
        if (bookName == null || bookName == "")
        {
            Books.Clear();
            foreach (Book book in BooksAll)
            {
                Books.Add(book);
            }
        }
        else
        {
            Books.Clear();
            foreach (Book book in BooksAll)
            {
                if (book.Name.ToLower() == bookName.ToLower())
                {
                    Books.Add(book);
                }
            }
        }
    }

    [RelayCommand]
    public async Task GoToModifyAsync()
    {
        await Shell.Current.GoToAsync(nameof(CrudBookView));
    }

    [RelayCommand]
    public async Task GoToAddRemoveAsync()
    {
        await Shell.Current.GoToAsync(nameof(CrudBookView));
    }

    [RelayCommand]
    public async Task OnNavigatedToAsync()
    {
        //Aquire current user Id and Name.
        (CurrentUserId, CurrentUserName) = CurrentUserService.Instance.GetIdAndName();
     
        BooksAll = await GetItemsAsync<Book>();
        Books.Clear();
        foreach (Book book in BooksAll)
        {
            Books.Add(book);
        }
    }
}
