using LibraryManagmentSystem.Models.Interfaces;
using LibraryManagmentSystem.Models.Transactions;
using SQLite;

namespace LibraryManagmentSystem.Models.Users.Members.Memberships;

/// <summary>
/// Membership base class, used as a table in DB, is created when a member is registered in the DB.
/// </summary>

public class Membership : IDataBaseTable
{
    //Fields
    private int _id;
    private int _memberId;
    private MembershipType _membershipType;
    private decimal _membershipPrice;
    private DateTime _startOfMembership;
    private DateTime _endOfMembership;

    //Properties
    [PrimaryKey, AutoIncrement]
    public int Id { get => _id; set => _id = value; }
    public int MemberId { get => _memberId; set => _memberId = value; }
    public MembershipType CurrentMembershipType { get => _membershipType; set => _membershipType = value; }
    public DateTime StartOfMembership { get => _startOfMembership; set => _startOfMembership = value; }
    public DateTime EndOfMembership { get => _endOfMembership; set => _endOfMembership = value; }
    public decimal MembershipPrice { get => _membershipPrice; set => _membershipPrice = value; }

    //Constructors
    public Membership(MembershipType membershipType, int memberId)
    {
        _membershipType = membershipType;
        _memberId = memberId;
        UpdateDates(_membershipType);
        _membershipPrice = ChangePrice(_membershipType);
    }

    public Membership()
    { }

    //Methods
    public bool IsMembershipExpired()
    {
        return _endOfMembership <= DateTime.Now; //Returns true if end date is equal or earlier than current date. 
    }

    public Bill CreateMembershipBill(Member member)
    {
        return new Bill(member.Id, _membershipPrice);
    }

    public Bill RenewMembership(Member member)
    {
        //RenewsMembership and creates a new bill.
        UpdateDates(_membershipType);
        return new Bill(member.Id, _membershipPrice);
    }

    public void ChangeMembership(MembershipType membershipType)
    {
        _membershipType = membershipType;
        UpdateDates(_membershipType);
        _membershipPrice = ChangePrice(_membershipType);
    }

    private decimal ChangePrice(MembershipType membershipType)
    {
        decimal price = _membershipPrice;
        switch (membershipType) //Checks the membership type and updates the price accordingly.
        {
            case MembershipType.Month:
                price = 9.99m;
                break;
            case MembershipType.Year:
                price = 49.99m;
                break;
            default:
                break;
        }

        return price;
    }

    private void UpdateDates(MembershipType membershipType)
    {
        switch (membershipType) //Checks the membership type and updates the dates accordingly.
        {
            case MembershipType.Month:
                _endOfMembership = DateTime.Now.AddMonths(1);
                break;
            case MembershipType.Year:
                _endOfMembership = DateTime.Now.AddYears(1);
                break;
            default:
                return;
        }
        _startOfMembership = DateTime.Now;
    }

    //Method to get string of membership info.
    public string GetMemberShipInfo()
    {
        return $"Id: {_id}, MemberId: {_memberId}, MembershipType: {_membershipType}, StartDate: {_startOfMembership}, " +
            $"EndDate: {_endOfMembership}, Price: {_membershipPrice}";
    }
}
