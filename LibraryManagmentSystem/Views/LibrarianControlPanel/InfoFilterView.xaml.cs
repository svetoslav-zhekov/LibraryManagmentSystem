using LibraryManagmentSystem.ViewModels.LibrarianControlPanel;

namespace LibraryManagmentSystem.Views.LibrarianControlPanel;

public partial class InfoFilterView : ContentPage
{
    //Constructors
    public InfoFilterView(InfoFilterViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    //Methods
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        InfoFilterViewModel viewModel = (InfoFilterViewModel)BindingContext;
        viewModel.NavigatedToCommand.Execute(null);
        base.OnNavigatedTo(args);
    }
}