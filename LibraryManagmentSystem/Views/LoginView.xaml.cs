using LibraryManagmentSystem.ViewModels;

namespace LibraryManagmentSystem.Views;

public partial class LoginView : ContentPage
{
    public LoginView(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

