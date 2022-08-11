using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagmentSystem.Models.Books;
using LibraryManagmentSystem.Models.Users.Members;
using LibraryManagmentSystem.Services;
using LibraryManagmentSystem.Views.LibrarianControlPanel.BookSection;
using LibraryManagmentSystem.Views.LibrarianControlPanel.MemberSection;
using System.Collections.ObjectModel;

namespace LibraryManagmentSystem.ViewModels.LibrarianControlPanel;

/// <summary>
/// InfoFilterViewModel, binded to InfoFilterView, helps with binding properties and commands to View and gets data from DB.
/// </summary>

public partial class InfoFilterViewModel : BaseViewModel
{
    //Fields
    [ObservableProperty]
    private int _currentUserId;
    [ObservableProperty]
    private string _currentUserName;

    //Selected Items
    [ObservableProperty]
    private Member _selectedMember;
    [ObservableProperty]
    private Book _selectedBook;

    //All Members and Books
    [ObservableProperty]
    private List<Member> _membersAll; //A field containing the whole table.
    [ObservableProperty]
    private ObservableCollection<Member> _members; //Gets data from _membersAll (if changed, no need to pull whole table again).

    [ObservableProperty]
    private List<Book> _booksAll;
    [ObservableProperty]
    private ObservableCollection<Book> _books;

    //Search By
    [ObservableProperty]
    private string _memberSearchBy;
    [ObservableProperty]
    private string _bookSearchBy;

    //Constructors
    public InfoFilterViewModel()
    {
        //Aquire current user Id and Name.
        (CurrentUserId, CurrentUserName) = CurrentUserService.Instance.GetIdAndName();

        _membersAll = Task.Run(() => GetItemsAsync<Member>()).Result;
        _members = new ObservableCollection<Member>(_membersAll);

        _booksAll = Task.Run(() => GetItemsAsync<Book>()).Result;
        _books = new ObservableCollection<Book>(_booksAll);
    }

    //Methods   
    //Member filter info.
    private string GetMemberProperty(Member member)
    {
        return MemberSearchBy switch
        {
            "Name" => member.Name.ToLower(),
            "Address" => member.Address.ToLower(),
            "PhoneNo" => member.Name.ToString().ToLower(),
            "Member Type" => member.TypeOfMember.ToString().ToLower(),
            "Max Book Limit" => member.MaxBooksLimit.ToString().ToLower(),
            _ => "",
        };
    }

    [RelayCommand]
    public async Task PerformMemberSearchAsync(string searchValue)
    {
        if (MemberSearchBy == null)
        {
            await Shell.Current.DisplayAlert("Error!", "No member filter selected!", "OK");
            return;
        }

        //Updates the ObservableCollection binding bound to the View's ListView. 
        if (searchValue == null || searchValue == "")
        {
            Members.Clear();
            foreach (Member member in MembersAll)
            {
                Members.Add(member);
            }
        }
        else
        {
            Members.Clear();
            foreach (Member member in MembersAll)
            {
                if (GetMemberProperty(member) == searchValue.ToLower())
                {
                    Members.Add(member);
                }
            }
        }
    }

    //Book filter info.
    private string GetBookProperty(Book book)
    {
        return BookSearchBy switch
        {
            "Name" => book.Name.ToLower(),
            "Book Type" => book.TypeOfBook.ToString().ToLower(),
            "Author" => book.Author.ToLower(),
            "Edition" => book.Edition.ToLower(),
            "Rack Number" => book.RackNo.ToString().ToLower(),
            _ => "",
        };
    }

    [RelayCommand]
    public async Task PerformBookSearchAsync(string searchValue)
    {
        if (BookSearchBy == null)
        {
            await Shell.Current.DisplayAlert("Error!", "No book filter selected!", "OK");
            return;
        }

        //Updates the ObservableCollection binding bound to the View's ListView.         
        if (searchValue == null || searchValue == "")
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
                if (GetBookProperty(book) == searchValue.ToLower())
                {
                    Books.Add(book);
                }
            }
        }
    }

    [RelayCommand]
    public async Task GoToMoreMemberInfoAsync()
    {
        if (SelectedMember != null)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "SentMember", SelectedMember }
            };

            await Shell.Current.GoToAsync(nameof(MoreMemberInfoView), navigationParameter); //Go to update view and send the selected member.
        }
        else
        {
            await Shell.Current.DisplayAlert("Error!", "No member selected!", "OK");
        }
    }

    [RelayCommand]
    public async Task GoToMoreBookInfoAsync()
    {
        if (SelectedBook != null)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "SentBook", SelectedBook }
            };

            await Shell.Current.GoToAsync(nameof(MoreBookInfoView), navigationParameter); //Go to update view and send the selected member.
        }
        else
        {
            await Shell.Current.DisplayAlert("Error!", "No book selected!", "OK");
        }
    }

    [RelayCommand]
    public async Task OnNavigatedToAsync()
    {
        //Aquire current user Id and Name.
        (CurrentUserId, CurrentUserName) = CurrentUserService.Instance.GetIdAndName();

        SelectedBook = null;
        SelectedMember = null;

        MembersAll = await GetItemsAsync<Member>();
        BooksAll = await GetItemsAsync<Book>();

        Members.Clear();
        foreach (Member member in MembersAll)
        {
            Members.Add(member);
        }

        Books.Clear();
        foreach (Book book in BooksAll)
        {
            Books.Add(book);
        }

    }

}

