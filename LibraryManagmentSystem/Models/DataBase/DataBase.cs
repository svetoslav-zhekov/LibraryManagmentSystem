using SQLite;

namespace LibraryManagmentSystem.Models.DataBase;

/// <summary>
/// DataBase thread safe singleton class, used for creating one instance of DataBase.
/// Checks if DB exists, if it doesnt, creates a folder in MyDocuments folder and creates DB afterwards.
/// </summary>

public sealed class DataBase
{
    //Fields
    private readonly SQLiteAsyncConnection _dataBaseConnection = null;

    private static readonly object _lock = new();

    private static DataBase _instance = null;

    //Properties
    public SQLiteAsyncConnection DataBaseConnection
    {
        get
        {
            return _dataBaseConnection;
        }
    }

    public static DataBase Instance
    {
        get
        {
            lock (_lock) //Locking it so it cannot be used from other threads until it gets unlocked
            {
                if (_instance == null)
                {
                    string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string localAppDataFolder = Path.Combine(localAppData, "LibraryManagmentSystemDB");

                    if (!Directory.Exists(localAppDataFolder))
                    {
                        Directory.CreateDirectory(localAppDataFolder);
                    }

                    _instance = new DataBase(new SQLiteAsyncConnection(Path.Combine(localAppDataFolder, "LibraryManagmentSystemDB.db")));
                }

                return _instance;
            }
        }
    }

    //Constructors
    private DataBase(SQLiteAsyncConnection dataBaseConnection)
    {
        _dataBaseConnection = dataBaseConnection; //Sets up the required SQLiteConnection to the DataBase
    }
}
