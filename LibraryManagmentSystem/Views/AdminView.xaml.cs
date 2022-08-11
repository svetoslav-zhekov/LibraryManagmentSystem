using LibraryManagmentSystem.ViewModels;

namespace LibraryManagmentSystem.Views;

public partial class AdminView : ContentPage
{
    public AdminView(AdminViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}