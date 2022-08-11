using LibraryManagmentSystem.Models.Interfaces;
using SQLite;

namespace LibraryManagmentSystem.Models.Users.Members;

/// <summary>
/// Member base class, used as a table in DataBase.
/// </summary>

public class Member : IDataBaseTable
{
    //Fields
    private int _id;
    private string _name;
    private string _address;
    private long _phoneNo;
    private MemberType _typeOfMember;
    private DateTime _dateAdded;
    private int _addedByLibrarianId;
    private int _numOfBooksIssued;
    private int _maxBookLimit;

    //Properties
    [PrimaryKey, NotNull, AutoIncrement]
    public int Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public string Address { get => _address; set => _address = value; }
    public long PhoneNo { get => _phoneNo; set => _phoneNo = value; }
    public MemberType TypeOfMember { get => _typeOfMember; set => _typeOfMember = value; }
    public DateTime DateAdded { get => _dateAdded; set => _dateAdded = value; }
    public int AddedByLibrarianId { get => _addedByLibrarianId; set => _addedByLibrarianId = value; }
    public int NumOfBooksIssued { get => _numOfBooksIssued; set => _numOfBooksIssued = value; }
    public int MaxBooksLimit { get => _maxBookLimit; set => _maxBookLimit = value; }

    //Constructors
    public Member()
    { }

    public Member(string name, string address, long phoneNo, MemberType typeOfMember,
        DateTime dateAdded, int addedByLibrarianId, int maxBooksLimit)
    {
        _name = name;
        _address = address;
        _phoneNo = phoneNo;
        _typeOfMember = typeOfMember;
        _dateAdded = dateAdded;
        _addedByLibrarianId = addedByLibrarianId;
        _numOfBooksIssued = 0;
        _maxBookLimit = maxBooksLimit;
    }

    //Methods
    public void IncreaseBookLimit(int amount)
    {
        _maxBookLimit += amount;
    }

    public void DecreaseBookLimit(int amount)
    {
        _maxBookLimit -= amount;
    }

    public string GetMemberInfo()
    {
        return $"Id: {_id} | Name: {_name} | Address: {_address} | PhoneNo: {_phoneNo} | " +
            $"MemberType: {_typeOfMember} | NumOfBooksIssued: {_numOfBooksIssued} | MaxBookLimit: {_maxBookLimit}";
    }

    //Checks wether a property has a null or empty value.
    public bool HasNullOrEmptyProperty()
    {
        return GetType().GetProperties()
        .Where(pi => pi.PropertyType == typeof(string))
        .Select(pi => (string)pi.GetValue(this))
        .Any(value => string.IsNullOrEmpty(value));
    }
}
