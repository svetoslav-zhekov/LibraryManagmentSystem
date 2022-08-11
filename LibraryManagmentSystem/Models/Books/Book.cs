using LibraryManagmentSystem.Models.Interfaces;
using SQLite;

namespace LibraryManagmentSystem.Models.Books;

/// <summary>
/// The Book base class, used as a table in DataBase.
/// </summary>

public class Book : IDataBaseTable
{
    //Fields
    private int _id;
    private string _name;
    private BookType _typeOfBook;
    private DateTime _dateOfPurchase;
    private string _author;
    private bool _isAvailable;
    private string _edition;
    private int _rackNo;
    private int _addedByLibrarianId;

    //Properties
    [PrimaryKey, NotNull, AutoIncrement]
    public int Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public BookType TypeOfBook { get => _typeOfBook; set => _typeOfBook = value; }
    public DateTime DateOfPurchase { get => _dateOfPurchase; set => _dateOfPurchase = value; }
    public string Author { get => _author; set => _author = value; }
    public bool IsAvailable { get => _isAvailable; set => _isAvailable = value; }
    public string Edition { get => _edition; set => _edition = value; }
    public int RackNo { get => _rackNo; set => _rackNo = value; }
    public int AddedByLibrarianId { get => _addedByLibrarianId; set => _addedByLibrarianId = value; }

    //Constructors
    public Book()
    { }

    public Book(string name, BookType bookType, DateTime dateOfPurchase,
        string author, string edition, int rackNo, int addedByLibrarianId)
    {
        _name = name;
        _typeOfBook = bookType;
        _dateOfPurchase = dateOfPurchase;
        _author = author;
        _isAvailable = true;
        _edition = edition;
        _rackNo = rackNo;
        _addedByLibrarianId = addedByLibrarianId;
    }

    //Methods
    public string GetBookInfo()
    {
        return $"Id: {_id} | Name: {_name} | BookType: {_typeOfBook} | DatePurchased: {_dateOfPurchase} | Author: {_author}" +
            $"| Edition: {_edition} | RackNo: {_rackNo} | AddedByLibrarian: {_addedByLibrarianId} | IsAvailable: {_isAvailable}";
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
