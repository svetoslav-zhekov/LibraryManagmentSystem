using LibraryManagmentSystem.ViewModels.LibrarianControlPanel.MemberSection;

namespace LibraryManagmentSystem.Views.LibrarianControlPanel.MemberSection;

public partial class AssignBookView : ContentPage
{
    public AssignBookView(AssignBookViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}