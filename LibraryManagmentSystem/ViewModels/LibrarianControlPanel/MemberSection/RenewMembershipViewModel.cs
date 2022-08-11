using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagmentSystem.Models.Transactions;
using LibraryManagmentSystem.Models.Users.Members;
using LibraryManagmentSystem.Models.Users.Members.Memberships;
using LibraryManagmentSystem.Services;
using System.Collections.ObjectModel;

namespace LibraryManagmentSystem.ViewModels.LibrarianControlPanel.MemberSection;

/// <summary>
/// RenewMembershipViewModel, binded to RenewMembershipView, helps with binding properties and commands to View and gets data from DB.
/// </summary>

public partial class RenewMembershipViewModel : BaseViewModel
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
    private Member _selectedMember;

    //Memberships
    [ObservableProperty]
    private List<Membership> _memberShipsAll;

    //Constructors
    public RenewMembershipViewModel()
    {
        //Aquire current user Id and Name.
        (CurrentUserId, CurrentUserName) = CurrentUserService.Instance.GetIdAndName();

        _membersAll = Task.Run(() => GetExpiredMembers()).Result;
        _members = new ObservableCollection<Member>(_membersAll);
    }

    //Methods
    private async Task<List<Member>> GetExpiredMembers()
    {
        //Gets only the expired memberships.
        MemberShipsAll = await GetItemsAsync<Membership>();
        MemberShipsAll = await Task.Run(() => MemberShipsAll.Where(x => x.EndOfMembership < DateTime.Now).ToList());

        //Gets only members which match with the memberships by id.
        var membersAll = await GetItemsAsync<Member>();
        List<Member> membersToReturn = new();
        foreach (Membership ms in MemberShipsAll)
        {
            foreach (Member member in membersAll)
            {
                if (member.Id == ms.MemberId)
                {
                    membersToReturn.Add(member);
                }
            }
        }

        return membersToReturn;
    }

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

    [RelayCommand]
    public async Task RenewMembershipAsync()
    {
        if (SelectedMember != null)
        {
            MembersAll.Remove(_selectedMember);
            Members.Remove(_selectedMember);

            //Gets the member's membership, updates it in DB, also creates new Bill.
            Membership memberShip = MemberShipsAll.FirstOrDefault(x => x.MemberId == SelectedMember.Id);
            Bill newBill = memberShip.RenewMembership(SelectedMember);

            await UpdateItemAsync(memberShip);
            await InsertItemAsync(newBill);

            MemberShipsAll.Remove(memberShip);
            SelectedMember = null;
        }
        else
        {
            await Shell.Current.DisplayAlert("Error!", "No member selected!", "OK");
        }
    }
}
