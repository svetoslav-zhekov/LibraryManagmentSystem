using LibraryManagmentSystem.ViewModels.LibrarianControlPanel.BookSection;

namespace LibraryManagmentSystem.Views.LibrarianControlPanel.BookSection;

public partial class UpdateBookView : ContentPage
{
    //Constructors
    public UpdateBookView(UpdateBookViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}