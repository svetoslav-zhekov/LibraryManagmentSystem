namespace LibraryManagmentSystem.Models.Users;

/// <summary>
/// Admin static class, used to aquire admin's username and password from DB etc.
/// </summary>

internal static class Admin
{
    //Fields
    private const string _username = "admin"; //STATICLY ADDED, CAN BE AQUIRED FROM A DIFFERENT DATABASE OR A TABLE etc.
    private const string _password = "admin"; // CONST FIELDS, FOR TESTING PURPOSES

    //Methods
    public static bool UserPassMatches(string username, string password)
    {
        if (_username == username && _password == password)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool UsernameMatches(string username)
    {
        if (_username == username)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
