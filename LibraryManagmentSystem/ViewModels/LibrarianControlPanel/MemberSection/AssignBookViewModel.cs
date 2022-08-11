using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagmentSystem.Models.Books;
using LibraryManagmentSystem.Models.Transactions;
using LibraryManagmentSystem.Models.Users.Members;
using LibraryManagmentSystem.Services;
using System.Collections.ObjectModel;

namespace LibraryManagmentSystem.ViewModels.LibrarianControlPanel.MemberSection;

/// <summary>
/// AssignBookViewModel, binded to AssignBookView, helps with binding properties and commands to View and gets data from DB.
/// </summary>

public partial class AssignBookViewModel : BaseViewModel
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

    //Time picker.
    [ObservableProperty]
    private string _periodOfTime;

    //Constructors
    public AssignBookViewModel()
    {
        //Aquire current user Id and Name.
        (CurrentUserId, CurrentUserName) = CurrentUserService.Instance.GetIdAndName();

        _membersAll = Task.Run(() => GetAvailableMembersAsync()).Result;
        _members = new ObservableCollection<Member>(_membersAll);

        _booksAll = Task.Run(() => GetAvailableBooksAsync()).Result;
        _books = new ObservableCollection<Book>(_booksAll);
    }

    //Methods  
    private async Task<List<Book>> GetAvailableBooksAsync()
    {
        var bookTable = await GetItemsAsync<Book>();
        return bookTable.Where(x => x.IsAvailable == true).ToList(); //Return only the available books.
    }

    //Gets all members who's book limit has not been reached yet.
    private async Task<List<Member>> GetAvailableMembersAsync()
    {
        var memberTable = await GetItemsAsync<Member>();
        return memberTable.Where(x => x.NumOfBooksIssued < x.MaxBooksLimit).ToList();
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
    public async Task CreateTransaction()
    {
        if (Member != null && Book != null && PeriodOfTime != null)
        {
            bool alertAnswer = await Shell.Current.DisplayAlert("Confirm Transaction?",
            $"You are about to assign {Member.Name} the book {Book.Name}, confirm?", "YES", "NO");

            if (alertAnswer == false)
            {
                return;
            }

            DateTime returnDate = DateTime.Now;

            switch (PeriodOfTime)
            {
                case "Week":
                    returnDate.AddDays(7);
                    break;
                case "Month":
                    returnDate.AddMonths(1);
                    break;
                case "Year":
                    returnDate.AddYears(1);
                    break;
                default:
                    return;
            }

            //Add transaction to DB.
            Transaction transaction = new(Member.Id, Book.Id, DateTime.Now, returnDate);
            await InsertItemAsync(transaction);

            //Update the book and member info in DB.
            Book.IsAvailable = false;
            await UpdateItemAsync(Book);
            BooksAll.Remove(Book);
            Books.Remove(Book);
            Book = null;

            Member.NumOfBooksIssued++;
            await UpdateItemAsync(Member);
            Members.Remove(Member);
            Members.Add(Member);
            Member = null;
        }
        else
        {
            await Shell.Current.DisplayAlert("Error!", "No member and/or book selected!", "OK");
        }

    }

}
