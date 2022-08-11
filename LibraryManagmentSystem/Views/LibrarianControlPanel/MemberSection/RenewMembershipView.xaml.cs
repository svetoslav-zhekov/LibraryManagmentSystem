using LibraryManagmentSystem.ViewModels.LibrarianControlPanel.MemberSection;

namespace LibraryManagmentSystem.Views.LibrarianControlPanel.MemberSection;

public partial class RenewMembershipView : ContentPage
{
    public RenewMembershipView(RenewMembershipViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}