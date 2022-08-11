using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagmentSystem.Models.Users.Members;
using LibraryManagmentSystem.Services;
using LibraryManagmentSystem.Views.LibrarianControlPanel.MemberSection;
using System.Collections.ObjectModel;

namespace LibraryManagmentSystem.ViewModels.LibrarianControlPanel.MemberSection;

/// <summary>
/// MemberSectionViewModel, binded to MemberSectionView, helps with binding properties and commands to View and gets data from DB.
/// </summary>

public partial class MemberSectionViewModel : BaseViewModel
{
    //Fields
    //Logged user info.
    [ObservableProperty]
    private int _currentUserId;
    [ObservableProperty]
    private string _currentUserName;

    //Member info.
    [ObservableProperty]
    private List<Member> _membersAll; //A field containing the whole table.
    [ObservableProperty]
    private ObservableCollection<Member> _members;
    [ObservableProperty]
    private Member _member;

    //Constructors
    public MemberSectionViewModel()
    {
        //Aquire current user Id and Name.
        (CurrentUserId, CurrentUserName) = CurrentUserService.Instance.GetIdAndName();

        _membersAll = Task.Run(() => GetItemsAsync<Member>()).Result;
        _members = new ObservableCollection<Member>(_membersAll);
    }

    //Methods 
    [RelayCommand]
    public void PerformMemberSearch(string memberName)
    {
        //Updates the ObservableCollection binding bound to the View's ListView. 
        if (memberName == null || memberName == "")
        {
            Members.Clear();
            foreach (Member member in MembersAll)
            {
                Members.Add(member);
            }
        }
        else
        {
            Members.Clear();
            foreach (Member member in MembersAll)
            {
                if (member.Name.ToLower() == memberName.ToLower())
                {
                    Members.Add(member);
                }
            }
        }
    }

    //Go To Commands
    [RelayCommand]
    public async Task GoToAssignBookAsync()
    {
        await Shell.Current.GoToAsync(nameof(AssignBookView));
    }

    [RelayCommand]
    public async Task GoToReturnBookAsync()
    {
        await Shell.Current.GoToAsync(nameof(ReturnBookView));
    }

    [RelayCommand]
    public async Task GoToRenewMembershipAsync()
    {
        await Shell.Current.GoToAsync(nameof(RenewMembershipView));
    }

    [RelayCommand]
    public async Task GoToCrudAsync()
    {
        await Shell.Current.GoToAsync(nameof(CrudMemberView));
    }

    [RelayCommand]
    public async Task OnNavigatedToAsync()
    {
        //Aquire current user Id and Name.
        (CurrentUserId, CurrentUserName) = CurrentUserService.Instance.GetIdAndName();

        MembersAll = await GetItemsAsync<Member>();
        Members.Clear();
        foreach (Member member in MembersAll)
        {
            Members.Add(member);
        }
    }

}
