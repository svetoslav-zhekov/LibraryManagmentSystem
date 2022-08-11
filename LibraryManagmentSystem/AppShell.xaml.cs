using LibraryManagmentSystem.ViewModels;
using LibraryManagmentSystem.Views.LibrarianControlPanel.BookSection;
using LibraryManagmentSystem.Views.LibrarianControlPanel.MemberSection;

namespace LibraryManagmentSystem;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        BindingContext = new AppShellViewModel();

        //Register not flyout views.
        //Member section.
        Routing.RegisterRoute(nameof(AssignBookView), typeof(AssignBookView));
        Routing.RegisterRoute(nameof(CrudMemberView), typeof(CrudMemberView));
        Routing.RegisterRoute(nameof(UpdateMemberView), typeof(UpdateMemberView));
        Routing.RegisterRoute(nameof(MoreMemberInfoView), typeof(MoreMemberInfoView));
        Routing.RegisterRoute(nameof(RenewMembershipView), typeof(RenewMembershipView));
        Routing.RegisterRoute(nameof(ReturnBookView), typeof(ReturnBookView));

        //Book section.
        Routing.RegisterRoute(nameof(CrudBookView), typeof(CrudBookView));
        Routing.RegisterRoute(nameof(UpdateBookView), typeof(UpdateBookView));
        Routing.RegisterRoute(nameof(MoreBookInfoView), typeof(MoreBookInfoView));
    }
}
