
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace WoodClub
{

using System;
    using System.Collections.Generic;
    
public partial class MemberRoster
{

    public int id { get; set; }

    public string Badge { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Zip { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string Title { get; set; }

    public string RecCard { get; set; }

    public string Locker { get; set; }

    public Nullable<System.DateTime> MemberDate { get; set; }

    public Nullable<bool> Exempt { get; set; }

    public Nullable<System.DateTime> ExemptModDate { get; set; }

    public Nullable<bool> ExtHour { get; set; }

    public Nullable<bool> EarlyAM { get; set; }

    public Nullable<bool> ClubDuesPaid { get; set; }

    public Nullable<System.DateTime> ClubDuesPaidDate { get; set; }

    public Nullable<bool> RecDuesPaid { get; set; }

    public string CreditBank { get; set; }

    public string CardNo { get; set; }

    public string EntryCodes { get; set; }

    public Nullable<int> AuthorizedTimeZone { get; set; }

    public Nullable<bool> Authorized { get; set; }

    public Nullable<bool> OneTime { get; set; }

    public Nullable<System.DateTime> LastDayValid { get; set; }

    public Nullable<bool> NewBadge { get; set; }

    public byte[] Photo { get; set; }

    public string GroupTime { get; set; }

    public Nullable<bool> AdminBlock { get; set; }

    public Nullable<System.DateTime> QBmodified { get; set; }

    public Nullable<bool> Private { get; set; }

    public string ERContactFirstName { get; set; }

    public string ERContactPhone { get; set; }

}

}
