using LibraryManagmentSystem.Models.Interfaces;
using SQLite;

namespace LibraryManagmentSystem.Models.Transactions;

/// <summary>
/// Transaction base class, used as a table in DB, 
/// created each time a book gets assigned to a member, deleted when member returns book.
/// </summary>

public class Transaction : IDataBaseTable
{
    //Fields
    private int _id;
    private int _memberId;
    private int _bookId;
    private DateTime _dateOfIssue;
    private DateTime _dueDate;

    //Properties
    [PrimaryKey, AutoIncrement]
    public int Id { get => _id; set => _id = value; }
    public int MemberId { get => _memberId; set => _memberId = value; }
    public int BookId { get => _bookId; set => _bookId = value; }
    public DateTime DateOfIssue { get => _dateOfIssue; set => _dateOfIssue = value; }
    public DateTime DueDate { get => _dueDate; set => _dueDate = value; }

    //Constructors
    public Transaction()
    { }

    public Transaction(int memberId, int bookId, DateTime dateOfIssue, DateTime dueDate)
    {
        _memberId = memberId;
        _bookId = bookId;
        _dateOfIssue = dateOfIssue;
        _dueDate = dueDate;
    }
}
