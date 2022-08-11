using LibraryManagmentSystem.Models.Interfaces;

namespace LibraryManagmentSystem.Services;

/// <summary>
/// CurrentUserService class, used to save logged user Id and Name for access in different ViewModels.
/// </summary>

public class CurrentUserService : IUser
{
    //Fields
    private int _id;
    private string _name;
    private static CurrentUserService _instance = null;
    private static readonly object _threadLock = new object();

    //Properties
    public int Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public static CurrentUserService Instance
    {
        get
        {
            lock (_threadLock)
            {
                if (_instance == null)
                {
                    _instance = new CurrentUserService();
                }
                return _instance;
            }
        }
    }

    //Constructor
    private CurrentUserService()
    { }

    //Methods
    //Return tuple with both the id and name for easier variable binding in code.
    public (int, string) GetIdAndName()
    {
        return (_id, _name);
    }
}
