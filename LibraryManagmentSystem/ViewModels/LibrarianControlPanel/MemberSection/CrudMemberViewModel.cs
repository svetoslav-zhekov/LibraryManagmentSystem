using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagmentSystem.Models.Transactions;
using LibraryManagmentSystem.Models.Users.Members;
using LibraryManagmentSystem.Models.Users.Members.Memberships;
using LibraryManagmentSystem.Services;
using LibraryManagmentSystem.Views.LibrarianControlPanel.MemberSection;
using System.Collections.ObjectModel;

namespace LibraryManagmentSystem.ViewModels.LibrarianControlPanel.MemberSection;

/// <summary>
/// CrudMemberViewModel, binded to CrudMemberView, helps with binding properties and commands to View and gets data from DB.
/// Crud (Create, Read, Update, Delete) information from Member database.
/// </summary>

public partial class CrudMemberViewModel : BaseViewModel
{
    //Fields
    //Logged user info.
    [ObservableProperty]
    private int _currentUserId;
    [ObservableProperty]
    private string _currentUserName;

    //The Member that entries insert info into.
    [ObservableProperty]
    private Member _insertedMember;
    [ObservableProperty]
    private MembershipType _memberShipType;

    [ObservableProperty]
    private List<Member> _membersAll; //A field containing the whole table.
    [ObservableProperty]
    private ObservableCollection<Member> _members;

    [ObservableProperty]
    private Member _selectedMember;

    //Constructors
    public CrudMemberViewModel()
    {
        //Aquire current user Id and Name.
        (CurrentUserId, CurrentUserName) = CurrentUserService.Instance.GetIdAndName();

        _membersAll = Task.Run(() => GetItemsAsync<Member>()).Result;
        _members = new ObservableCollection<Member>(_membersAll);

        //Init new member object.
        _insertedMember = new();
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

    [RelayCommand]
    public async Task InsertMemberAsync()
    {
        InsertedMember.DateAdded = DateTime.Now;
        InsertedMember.AddedByLibrarianId = _currentUserId;
        InsertedMember.NumOfBooksIssued = 0;

        ////Insert the new Member in the DataBase If Inputs are not Null or Empty.
        if (!InsertedMember.HasNullOrEmptyProperty())
        {
            //Input member DB and then take out to aquire the ID generated.
            await InsertItemAsync(InsertedMember);
            List<Member> membersFromDB = await GetItemsAsync<Member>();

            Membership memberShip = new(MemberShipType, membersFromDB.Last().Id);
            Bill bill = memberShip.CreateMembershipBill(membersFromDB.Last());

            await InsertItemAsync(bill);
            await InsertItemAsync(memberShip);
            Members.Add(membersFromDB.Last());
            MembersAll.Add(membersFromDB.Last());

            //Clear object.
            InsertedMember = new();
        }
        else
        {
            await Shell.Current.DisplayAlert("Insert Error!", "Cannot have empty entry fields!", "OK");
        }

    }

    [RelayCommand]
    public async Task DeleteMemberAsync()
    {
        if (SelectedMember != null)
        {
            bool alertAnswer = await Shell.Current.DisplayAlert("Confirm Deletion?",
            $"You are about to DELETE {SelectedMember.Name}, confirm?", "YES", "NO");

            if (alertAnswer == false)
            {
                return;
            }

            await DeleteItemAsync(SelectedMember); //Delete the Member from the DataBase
            Members.Remove(SelectedMember);
            MembersAll.Remove(SelectedMember);
            SelectedMember = null;
        }
        else
        {
            await Shell.Current.DisplayAlert("Error!", "No member selected!", "OK");
        }
    }

    [RelayCommand]
    public async Task GoToUpdateAsync()
    {
        if (SelectedMember != null)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "SentMember", SelectedMember }
            };

            await Shell.Current.GoToAsync(nameof(UpdateMemberView), navigationParameter); //Go to update view and send the selected member.
        }
        else
        {
            await Shell.Current.DisplayAlert("Error!", "No member selected!", "OK");
        }
    }

    //Command to bind in the onnavigated to method in view.
    [RelayCommand]
    public void OnNavigatedTo()
    {
        if (SelectedMember != null)
        {
            Members = new ObservableCollection<Member>(MembersAll);
        }
    }
}
