using CommunityToolkit.Mvvm.ComponentModel;
using LibraryManagmentSystem.Models.Transactions;
using LibraryManagmentSystem.Models.Users.Members;
using LibraryManagmentSystem.Models.Users.Members.Memberships;
using LibraryManagmentSystem.Services;
using System.Collections.ObjectModel;

namespace LibraryManagmentSystem.ViewModels.LibrarianControlPanel.MemberSection;

/// <summary>
/// MoreMemberInfoViewModel, binded to MoreMemberInfoView, helps with binding properties and commands to View and gets data from DB.
/// </summary>

[QueryProperty("SentMember", "SentMember")] //The member sent by the last view.
public partial class MoreMemberInfoViewModel : BaseViewModel
{
    //Fields
    //Logged user info.oip
    [ObservableProperty]
    private int _currentUserId;
    [ObservableProperty]
    private string _currentUserName;

    //Member and membership info.
    [ObservableProperty]
    private string _allMemberInfo;
    [ObservableProperty]
    private string _allMemberShipInfo;

    //Sent member.
    [ObservableProperty]
    private Member _sentMember;

    //Membership
    [ObservableProperty]
    private Membership _memberShip;

    //Bills
    [ObservableProperty]
    private ObservableCollection<Bill> _billsAll;

    //Constructors
    public MoreMemberInfoViewModel()
    {
        //Aquire current user Id and Name.
        (CurrentUserId, CurrentUserName) = CurrentUserService.Instance.GetIdAndName();
    }

    //Methods
    partial void OnSentMemberChanged(Member value)
    {
        MemberShip = Task.Run(() => GetMembershipAsync()).Result;
        AllMemberShipInfo = MemberShip.GetMemberShipInfo();
        AllMemberInfo = value.GetMemberInfo();

        //Aquire all member bills.
        BillsAll = new ObservableCollection<Bill>(Task.Run(() => GetItemsAsync<Bill>()).Result.Where(x => x.MemberId == value.Id));
    }

    private async Task<Membership> GetMembershipAsync()
    {
        var bookTable = await GetItemsAsync<Membership>();
        return bookTable.FirstOrDefault(x => x.MemberId == SentMember.Id); //Return only the available books.
    }
}
