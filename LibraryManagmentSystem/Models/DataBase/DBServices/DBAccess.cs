using LibraryManagmentSystem.Models.Books;

namespace LibraryManagmentSystem.Models.DataBase.DBServices;

/// <summary>
/// Generic static class, used for inserting, modifying and deleting data from the DataBase.
/// </summary>
/// <typeparam name="T">As a Type of class with a parametless constructor.</typeparam>

public static class DBAccess<T> where T : new()
{
    //Methods
    public static async Task<List<T>> GetTableAsync()
    {
        List<T> data = new();

        try
        {
            await DataBase.Instance.DataBaseConnection.CreateTableAsync<T>(); //Checks if table exists, if not create it.
            data = await DataBase.Instance.DataBaseConnection.Table<T>().ToListAsync(); // Return table.          
        }
        catch (Exception ex)
        {
            throw new Exception("Access to database has failed!", ex);
        }
        finally
        {
            await DataBase.Instance.DataBaseConnection.CloseAsync(); //If table data access failed, throw exception and close connection.
        }

        return data;
    }
  
    public static async Task<int> InsertInTableAsync(T classObject)
    {
        int result = 0;

        try
        {
            await DataBase.Instance.DataBaseConnection.CreateTableAsync<T>();
            result = await DataBase.Instance.DataBaseConnection.InsertAsync(classObject);
        }
        catch (Exception ex)
        {
            throw new Exception("Access to database has failed!", ex);
        }
        finally
        {
            await DataBase.Instance.DataBaseConnection.CloseAsync(); //If table data access failed, throw exception and close connection.
        }    
        
        return result;
    }

    public static async Task<int> RemoveFromTableAsync(T classObject)
    {
        int result = 0;

        try
        {
            await DataBase.Instance.DataBaseConnection.CreateTableAsync<T>();
            result = await DataBase.Instance.DataBaseConnection.DeleteAsync(classObject);
        }
        catch (Exception ex)
        {
            throw new Exception("Access to database has failed!", ex);
        }
        finally
        {
            await DataBase.Instance.DataBaseConnection.CloseAsync(); //If table data access failed, throw exception and close connection.
        }
        
        return result;
    }

    public static async Task<int> UpdateInTableAsync(T classObject)
    {
        int result = 0;

        try
        {
            await DataBase.Instance.DataBaseConnection.CreateTableAsync<T>();
            result = await DataBase.Instance.DataBaseConnection.UpdateAsync(classObject);
        }
        catch (Exception ex)
        {
            throw new Exception("Access to database has failed!", ex);
        }
        finally
        {
            await DataBase.Instance.DataBaseConnection.CloseAsync(); //If table data access failed, throw exception and close connection.
        }
               
        return result;
    }
}
