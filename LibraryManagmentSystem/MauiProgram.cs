using LibraryManagmentSystem.ViewModels;
using LibraryManagmentSystem.ViewModels.LibrarianControlPanel;
using LibraryManagmentSystem.ViewModels.LibrarianControlPanel.BookSection;
using LibraryManagmentSystem.ViewModels.LibrarianControlPanel.MemberSection;
using LibraryManagmentSystem.Views;
using LibraryManagmentSystem.Views.LibrarianControlPanel;
using LibraryManagmentSystem.Views.LibrarianControlPanel.BookSection;
using LibraryManagmentSystem.Views.LibrarianControlPanel.MemberSection;

namespace LibraryManagmentSystem;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("SonsieOne-Regular.ttf", "SonsieOneRegular");
            });

        //Views
        builder.Services.AddTransient<LoginView>();
        builder.Services.AddTransient<AdminView>();

        //Views.LibrarianControlPanel
        builder.Services.AddTransient<InfoFilterView>();

        //Views.LibrarianControlPanel.MemberSection
        builder.Services.AddTransient<MemberSectionView>();
        builder.Services.AddTransient<CrudMemberView>();
        builder.Services.AddTransient<UpdateMemberView>();
        builder.Services.AddTransient<AssignBookView>();
        builder.Services.AddTransient<MoreMemberInfoView>();
        builder.Services.AddTransient<RenewMembershipView>();
        builder.Services.AddTransient<ReturnBookView>();

        //Views.LibrarianControlPanel.BookSection
        builder.Services.AddTransient<BookSectionView>();
        builder.Services.AddTransient<CrudBookView>();
        builder.Services.AddTransient<UpdateBookView>();
        builder.Services.AddTransient<MoreBookInfoView>();

        //ViewModels
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<AdminViewModel>();
        builder.Services.AddTransient<AppShellViewModel>();

        //ViewModels.LibrarianControlPanel
        builder.Services.AddTransient<InfoFilterViewModel>();

        //ViewModels.LibrarianControlPanel.MemberSection
        builder.Services.AddTransient<MemberSectionViewModel>();
        builder.Services.AddTransient<CrudMemberViewModel>();
        builder.Services.AddTransient<UpdateMemberViewModel>();
        builder.Services.AddTransient<AssignBookViewModel>();
        builder.Services.AddTransient<MoreMemberInfoViewModel>();
        builder.Services.AddTransient<RenewMembershipViewModel>();
        builder.Services.AddTransient<ReturnBookViewModel>();

        //ViewModels.LibrarianControlPanel.BookSection
        builder.Services.AddTransient<BookSectionViewModel>();
        builder.Services.AddTransient<CrudBookViewModel>();
        builder.Services.AddTransient<UpdateBookViewModel>();
        builder.Services.AddTransient<MoreBookInfoViewModel>();


        return builder.Build();
    }
}
