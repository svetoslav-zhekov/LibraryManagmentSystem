using CommunityToolkit.Mvvm.ComponentModel;
using LibraryManagmentSystem.Models.DataBase.DBServices;

namespace LibraryManagmentSystem.ViewModels;

public abstract class BaseViewModel : ObservableObject
{
    //Methods
    //CRUD Methods
    /// <summary>
    /// Method to get data from table in Database.
    /// </summary>
    /// <typeparam name="T">Type parameter T is a class with a parametless counstructor</typeparam>
    /// <returns>Returns: The data collected from Database.</returns>
    protected async Task<List<T>> GetItemsAsync<T>() where T : new() 
    {
        List<T> dataTable = new();

        try
        {
            dataTable = await DBAccess<T>.GetTableAsync();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"{ex.Message} Restart application and try again!", "OK");
        }
      
        return dataTable;
    }

    /// <summary>
    /// Method to insert data in Database.
    /// </summary>
    /// <typeparam name="T">Type parameter T is a class with a parametless counstructor</typeparam>
    /// <returns>Returns: If item was added in table, if it was added returns 1, if not then 0.</returns>
    protected async Task<int> InsertItemAsync<T>(T obj) where T : new()
    {
        int successful = 0;

        try
        {
           successful = await DBAccess<T>.InsertInTableAsync(obj);          
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"{ex.Message} Restart application and try again!", "OK");
        }

        return successful;
    }

    /// <summary>
    /// Method to update data in Database.
    /// </summary>
    /// <typeparam name="T">Type parameter T is a class with a parametless counstructor</typeparam>
    /// <returns>Returns: If item was updated in table, if it was updated returns 1, if not then 0.</returns>
    protected async Task<int> UpdateItemAsync<T>(T obj) where T : new()
    {
        int successful = 0;

        try
        {
            successful = await DBAccess<T>.UpdateInTableAsync(obj);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"{ex.Message} Restart application and try again!", "OK");
        }

        return successful;
    }

    /// <summary>
    /// Method to delete data in Database.
    /// </summary>
    /// <typeparam name="T">Type parameter T is a class with a parametless counstructor</typeparam>
    /// <returns>Returns: If item was deleted in table, if it was deleted returns 1, if not then 0.</returns>
    protected async Task<int> DeleteItemAsync<T>(T obj) where T : new()
    {
        int successful = 0;

        try
        {
            successful = await DBAccess<T>.RemoveFromTableAsync(obj);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"{ex.Message} Restart application and try again!", "OK");
        }

        return successful;
    }
}
