namespace LibraryManagmentSystem.Models.Interfaces;

/// <summary>
/// IDataBaseTable, implemented by every single base class which is used as a table in the DB.
/// </summary>

public interface IDataBaseTable
{
    public int Id { get; set; }
}
