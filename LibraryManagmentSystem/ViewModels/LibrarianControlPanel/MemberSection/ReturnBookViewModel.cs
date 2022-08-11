using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagmentSystem.Models.Books;
using LibraryManagmentSystem.Models.Transactions;
using LibraryManagmentSystem.Models.Users.Members;
using LibraryManagmentSystem.Services;
using System.Collections.ObjectModel;

namespace LibraryManagmentSystem.ViewModels.LibrarianControlPanel.MemberSection;

/// <summary>
/// ReturnBookViewModel, binded to ReturnBookView, helps with binding properties and commands to View and gets data from DB.
/// </summary>

public partial class ReturnBookViewModel : BaseViewModel
{
    //Fields
    //Logged user info.
    [ObservableProperty]
    private int _currentUserId;
    [ObservableProperty]
    private string _currentUserName;

    //Member info.
    [ObservableProperty]
    private List<Member> _membersAll; //A field containing the whole table.
    [ObservableProperty]
    private ObservableCollection<Member> _members; //Gets data from _membersAll (if changed, no need to pull whole table again).
    [ObservableProperty]
    private Member _member;

    //Book info.
    [ObservableProperty]
    private List<Book> _booksAll;
    [ObservableProperty]
    private ObservableCollection<Book> _books;
    [ObservableProperty]
    private Book _book;

    //Transactions
    [ObservableProperty]
    private List<Transaction> _transactionsAll;

    //Constructors
    public ReturnBookViewModel()
    {
        //Aquire current user Id and Name.
        (CurrentUserId, CurrentUserName) = CurrentUserService.Instance.GetIdAndName();

        _membersAll = Task.Run(() => GetItemsAsync<Member>()).Result;
        _members = new ObservableCollection<Member>(_membersAll);

        _booksAll = Task.Run(() => GetItemsAsync<Book>()).Result;
        _books = new ObservableCollection<Book>();

        _transactionsAll = Task.Run(() => GetItemsAsync<Transaction>()).Result;
    }

    //Methods
    //OnSelectedMember changed, get the books he needs to return.
    partial void OnMemberChanged(Member value)
    {
        Books.Clear();
        foreach (Transaction transaction in TransactionsAll)
        {
            foreach (Book book in BooksAll)
            {
                if (transaction.MemberId == value.Id && transaction.BookId == book.Id && !book.IsAvailable)
                {
                    Books.Add(book);
                }
            }
        }
    }

    [RelayCommand]
    public void PerformMemberSearch(string memberName)
    {
        //Updates the ObservableCollection binding bound to the View's ListView. 
        if (memberName == null || memberName == "")
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
                if (member.Name.ToLower() == memberName.ToLower())
                {
                    Members.Add(member);
                }
            }
        }
    }

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
    public async Task ReturnBookAsync()
    {
        if (Member == null || Book == null)
        {
            await Shell.Current.DisplayAlert("Error!", "No member and/or book selected!", "OK");
            return;
        }

        Book.IsAvailable = true;
        Member.NumOfBooksIssued--;

        //Get correct old transaction and delete it from DB.
        Transaction oldTransaction = TransactionsAll.FirstOrDefault(x => x.BookId == Book.Id && x.MemberId == Member.Id);
        await DeleteItemAsync(oldTransaction);

        //Update book info in DB.
        await UpdateItemAsync(Book);

        //Update member info in DB.
        await UpdateItemAsync(Member);

        BooksAll.Remove(Book);
        Books.Remove(Book);
    }
}
