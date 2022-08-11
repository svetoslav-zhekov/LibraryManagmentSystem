using LibraryManagmentSystem.ViewModels.LibrarianControlPanel.BookSection;

namespace LibraryManagmentSystem.Views.LibrarianControlPanel.BookSection;

public partial class CrudBookView : ContentPage
{
    //Constructors
    public CrudBookView(CrudBookViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    //Methods
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        CrudBookViewModel viewModel = (CrudBookViewModel)BindingContext;
        viewModel.NavigatedToCommand.Execute(null);
        base.OnNavigatedTo(args);
    }
}