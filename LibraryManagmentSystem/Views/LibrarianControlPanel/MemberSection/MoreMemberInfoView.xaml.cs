using LibraryManagmentSystem.ViewModels.LibrarianControlPanel.MemberSection;

namespace LibraryManagmentSystem.Views.LibrarianControlPanel.MemberSection;

public partial class MoreMemberInfoView : ContentPage
{
    public MoreMemberInfoView(MoreMemberInfoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}