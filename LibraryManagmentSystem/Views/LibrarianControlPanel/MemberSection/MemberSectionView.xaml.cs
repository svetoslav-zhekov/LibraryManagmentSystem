using LibraryManagmentSystem.ViewModels.LibrarianControlPanel.BookSection;
using LibraryManagmentSystem.ViewModels.LibrarianControlPanel.MemberSection;

namespace LibraryManagmentSystem.Views.LibrarianControlPanel.MemberSection;

public partial class MemberSectionView : ContentPage
{
    //Constructors
    public MemberSectionView(MemberSectionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    //Methods
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        MemberSectionViewModel viewModel = (MemberSectionViewModel)BindingContext;
        viewModel.NavigatedToCommand.Execute(null);
        base.OnNavigatedTo(args);
    }
}