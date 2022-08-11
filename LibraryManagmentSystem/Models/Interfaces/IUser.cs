namespace LibraryManagmentSystem.Models.Interfaces;

/// <summary>
/// IUser interface, implemented by the user base classes, which are used as tables in DB.
/// </summary>

public interface IUser
{
    int Id { get; set; }
    string Name { get; set; }
}
