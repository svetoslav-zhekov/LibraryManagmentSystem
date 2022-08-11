using LibraryManagmentSystem.ViewModels.LibrarianControlPanel.BookSection;

namespace LibraryManagmentSystem.Views.LibrarianControlPanel.BookSection;

public partial class MoreBookInfoView : ContentPage
{
    public MoreBookInfoView(MoreBookInfoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}