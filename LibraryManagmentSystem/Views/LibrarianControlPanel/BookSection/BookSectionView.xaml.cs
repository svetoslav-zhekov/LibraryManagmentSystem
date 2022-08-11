using LibraryManagmentSystem.ViewModels.LibrarianControlPanel;
using LibraryManagmentSystem.ViewModels.LibrarianControlPanel.BookSection;

namespace LibraryManagmentSystem.Views.LibrarianControlPanel.BookSection;

public partial class BookSectionView : ContentPage
{
    //Constructors
    public BookSectionView(BookSectionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    //Methods
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        BookSectionViewModel viewModel = (BookSectionViewModel)BindingContext;
        viewModel.NavigatedToCommand.Execute(null);
        base.OnNavigatedTo(args);
    }
}