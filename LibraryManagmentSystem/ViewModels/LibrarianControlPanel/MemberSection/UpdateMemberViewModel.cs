using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagmentSystem.Models.Transactions;
using LibraryManagmentSystem.Models.Users.Members;
using LibraryManagmentSystem.Models.Users.Members.Memberships;
using LibraryManagmentSystem.Services;

namespace LibraryManagmentSystem.ViewModels.LibrarianControlPanel.MemberSection;

/// <summary>
/// UpdateMemberViewModel, binded to UpdateMemberView, helps with binding properties and commands to View and gets data from DB.
/// </summary>

[QueryProperty("SentMember", "SentMember")] //The member sent by the last view.
public partial class UpdateMemberViewModel : BaseViewModel
{
    //Fields
    //Picker info.
    [ObservableProperty]
    private string _infoToChange;

    //IsVisible bindings.
    [ObservableProperty]
    private bool _nameVisible;
    [ObservableProperty]
    private bool _addressVisible;
    [ObservableProperty]
    private bool _phoneNumVisible;
    [ObservableProperty]
    private bool _typeMemberVisible;
    [ObservableProperty]
    private bool _memberShipVisible;
    [ObservableProperty]
    private bool _bookLimitVisible;
    [ObservableProperty]
    private bool _buttonVisible;

    //Logged user info.
    [ObservableProperty]
    private int _currentUserId;
    [ObservableProperty]
    private string _currentUserName;

    //Sent member info.
    [ObservableProperty]
    private string _allMemberInfo;
    [ObservableProperty]
    private Member _sentMember;
    [ObservableProperty]
    private MembershipType _memberShipType;

    //Membership info.
    [ObservableProperty]
    private Membership _memberShip;
    [ObservableProperty]
    private List<Membership> _allMemberShips;

    //Constructors
    public UpdateMemberViewModel()
    {
        //Aquire current user Id and Name.
        (CurrentUserId, CurrentUserName) = CurrentUserService.Instance.GetIdAndName();

        //Aquire the member memberships details.
        _allMemberShips = Task.Run(() => GetItemsAsync<Membership>()).Result;
    }

    //Methods
    //OnPropertyChangedMethods
    partial void OnInfoToChangeChanged(string value)
    {
        ButtonVisible = true;
        AllMemberInfo = SentMember.GetMemberInfo();
        switch (value) //On picker selected change, also change the visability of element.
        {
            case "Name":
                ChangeVisabilities(true, false, false, false, false, false);
                break;
            case "Address":
                ChangeVisabilities(false, true, false, false, false, false);
                break;
            case "PhoneNo":
                ChangeVisabilities(false, false, true, false, false, false);
                break;
            case "Member Type":
                ChangeVisabilities(false, false, false, true, false, false);
                break;
            case "Membership Type":
                ChangeVisabilities(false, false, false, false, true, false);
                break;
            case "Max Book Limit":
                ChangeVisabilities(false, false, false, false, false, true);
                break;
            default:
                break;
        }
    }

    private void ChangeVisabilities(bool name, bool address, bool phoneNum, bool memberType, bool membershipType, bool bookLimit)
    {
        NameVisible = name;
        AddressVisible = address;
        PhoneNumVisible = phoneNum;
        TypeMemberVisible = memberType;
        MemberShipVisible = membershipType;
        BookLimitVisible = bookLimit;
    }

    private async Task UpdateDBAndLeave()
    {
        await UpdateItemAsync(SentMember); //Updates member in DB.
        await Shell.Current.DisplayAlert("Update Sucessful!", "Member info has been updated sucesfully.", "OK");
        await Shell.Current.GoToAsync($"..");
    }

    [RelayCommand]
    public async Task UpdateMemberAsync()
    {
        if (SentMember != null)
        {
            bool alertAnswer = await Shell.Current.DisplayAlert("Confirm Update?",
            $"You are about to UPDATE the member!", "YES", "NO");

            if (alertAnswer == false)
            {
                return;
            }

            if (SentMember.HasNullOrEmptyProperty())
            {
                await Shell.Current.DisplayAlert("Update Error!", "Cannot have empty entry fields!", "OK");
                return;
            }

            if (InfoToChange == "Membership Type")
            {
                MemberShip = AllMemberShips.FirstOrDefault(x => x.MemberId == SentMember.Id);

                if (MemberShip.CurrentMembershipType != MemberShipType)
                {
                    MemberShip.ChangeMembership(MemberShipType);
                    Bill bill = MemberShip.CreateMembershipBill(SentMember);
                    await InsertItemAsync(bill);
                    await UpdateItemAsync(MemberShip);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error!", "Already has the same membership!", "OK");
                    return;
                }

                await Shell.Current.DisplayAlert("Update Sucessful!", "Member info has been updated sucesfully.", "OK");
                await Shell.Current.GoToAsync("..");
            }

            await UpdateDBAndLeave();
        }
    }
}
