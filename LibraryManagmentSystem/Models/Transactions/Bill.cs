using LibraryManagmentSystem.Models.Interfaces;
using SQLite;

namespace LibraryManagmentSystem.Models.Transactions;

/// <summary>
/// Bill base class, used as table in DB, created each time a member renews membership or is registred in DB.
/// </summary>

public class Bill : IDataBaseTable
{
    //Fields
    private int _id;
    private DateTime _dateOfCreation;
    private int _memberId;
    private decimal _paidAmount;

    //Properties
    [PrimaryKey, AutoIncrement]
    public int Id { get => _id; set => _id = value; }
    public DateTime DateOfCreation { get => _dateOfCreation; set => _dateOfCreation = value; }
    public int MemberId { get => _memberId; set => _memberId = value; }
    public decimal PaidAmount { get => _paidAmount; set => _paidAmount = value; }

    //Constructors
    public Bill()
    { }

    public Bill(int memberId, decimal paidAmount)
    {
        _dateOfCreation = DateTime.Now;
        _memberId = memberId;
        _paidAmount = paidAmount;
    }
}
