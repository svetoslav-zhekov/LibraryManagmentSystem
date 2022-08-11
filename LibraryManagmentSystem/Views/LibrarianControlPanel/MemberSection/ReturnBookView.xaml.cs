using LibraryManagmentSystem.ViewModels.LibrarianControlPanel.MemberSection;

namespace LibraryManagmentSystem.Views.LibrarianControlPanel.MemberSection;

public partial class ReturnBookView : ContentPage
{
    public ReturnBookView(ReturnBookViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}