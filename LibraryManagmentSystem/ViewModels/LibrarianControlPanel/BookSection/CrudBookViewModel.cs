using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagmentSystem.Models.Books;
using LibraryManagmentSystem.Services;
using LibraryManagmentSystem.Views.LibrarianControlPanel.BookSection;
using System.Collections.ObjectModel;

namespace LibraryManagmentSystem.ViewModels.LibrarianControlPanel.BookSection;

/// <summary>
/// CrudBookViewModel, binded to CrudBookView, helps with binding properties and commands to View and gets data from DB.
/// Crud (Create, Read, Update, Delete) information from Book database.
/// </summary>

public partial class CrudBookViewModel : BaseViewModel
{
    //Fields
    //Logged user info.
    [ObservableProperty]
    private int _currentUserId;
    [ObservableProperty]
    private string _currentUserName;

    //The Book that entries insert info into.
    [ObservableProperty]
    private Book _insertedBook;

    //All books from table and the selectedBook.
    [ObservableProperty]
    private List<Book> _booksAll; //A field containing all books.
    [ObservableProperty]
    private ObservableCollection<Book> _books;
    [ObservableProperty]
    private Book _selectedBook;

    //Constructors
    public CrudBookViewModel()
    {
        //Aquire current user Id and Name.
        (CurrentUserId, CurrentUserName) = CurrentUserService.Instance.GetIdAndName();

        _booksAll = Task.Run(() => GetItemsAsync<Book>()).Result;
        _books = new ObservableCollection<Book>(BooksAll);

        //Init new book object.
        _insertedBook = new();
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
    public async Task InsertBookAsync()
    {
        InsertedBook.DateOfPurchase = DateTime.Now;
        InsertedBook.AddedByLibrarianId = CurrentUserId;
        InsertedBook.IsAvailable = true;

        //Insert new book in DB if it has no null or empty properties.
        if (!InsertedBook.HasNullOrEmptyProperty())
        {
            //Add book in DB and take out to aquire the ID generated.
            await InsertItemAsync(InsertedBook);
            List<Book> booksFromDB = await GetItemsAsync<Book>();
            _booksAll.Add(booksFromDB.Last());
            _books.Add(booksFromDB.Last());

            //Clear object.
            InsertedBook = new();
        }
        else
        {
            await Shell.Current.DisplayAlert("Insert Error!", "Cannot have empty entry fields!", "OK");
        }
    }

    [RelayCommand]
    public async Task DeleteBookAsync()
    {
        if (SelectedBook != null)
        {
            await DeleteItemAsync(SelectedBook); //Delete the Book from the DataBase
            Books.Remove(SelectedBook);
            BooksAll.Remove(SelectedBook);
            SelectedBook = null;
        }
        else
        {
            await Shell.Current.DisplayAlert("Delete Error!", "No book selected!", "OK");
        }
    }

    [RelayCommand]
    public async Task GoToUpdateAsync()
    {
        if (SelectedBook != null)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "SentBook", SelectedBook }
            };

            await Shell.Current.GoToAsync(nameof(UpdateBookView), navigationParameter); //Go to update view and send the selected book.
        }
        else
        {
            await Shell.Current.DisplayAlert("Error!", "No book selected!", "OK");
        }
    }

    //Command to bind in the onnavigated to method in view.
    [RelayCommand]
    public void OnNavigatedTo()
    {
        if (SelectedBook != null)
        {
            Books = new ObservableCollection<Book>(BooksAll);
        }
    }

}
