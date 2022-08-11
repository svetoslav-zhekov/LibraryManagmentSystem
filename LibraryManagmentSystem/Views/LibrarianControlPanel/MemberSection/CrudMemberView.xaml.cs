using LibraryManagmentSystem.ViewModels.LibrarianControlPanel.MemberSection;

namespace LibraryManagmentSystem.Views.LibrarianControlPanel.MemberSection;

public partial class CrudMemberView : ContentPage
{
    //Constructors
    public CrudMemberView(CrudMemberViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    //Methods
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        CrudMemberViewModel viewModel = (CrudMemberViewModel)BindingContext;
        viewModel.NavigatedToCommand.Execute(null);
        base.OnNavigatedTo(args);
    }
}