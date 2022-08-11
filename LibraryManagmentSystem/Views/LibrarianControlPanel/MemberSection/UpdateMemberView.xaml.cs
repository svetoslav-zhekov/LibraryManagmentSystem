using LibraryManagmentSystem.ViewModels.LibrarianControlPanel.MemberSection;

namespace LibraryManagmentSystem.Views.LibrarianControlPanel.MemberSection;

public partial class UpdateMemberView : ContentPage
{
    //Constructors
    public UpdateMemberView(UpdateMemberViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}