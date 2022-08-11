using LibraryManagmentSystem.Models.Interfaces;
using SQLite;

namespace LibraryManagmentSystem.Models.Users;

/// <summary>
/// The Librarian base class, used as a login and manage member crud operations.
/// </summary>

public class Librarian : IDataBaseTable, IUser
{
    //Fields
    private int _id;
    private string _name;
    private string _username;
    private string _password;

    //Properties
    [PrimaryKey, AutoIncrement]
    public int Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public string Username { get => _username; set => _username = value; }
    public string Password { get => _password; set => _password = value; }

    //Constructors
    public Librarian()
    {
    }

    public Librarian(string name, string username, string password)
    {
        _name = name;
        _username = username;
        _password = password;
    }

    //Methods
    public bool UserPassMatches(string username, string password)
    {
        if (username == _username && password == _password)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //TODO Implement Methods
}
