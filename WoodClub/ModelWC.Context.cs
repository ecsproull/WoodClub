﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WoodClubEntities : DbContext
    {
        public WoodClubEntities()
            : base("name=WoodClubEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CreditTotal> CreditTotals { get; set; }
        public virtual DbSet<LockerCost> LockerCosts { get; set; }
        public virtual DbSet<LockerLocation> LockerLocations { get; set; }
        public virtual DbSet<Locker> Lockers { get; set; }
        public virtual DbSet<MemberRFcard> MemberRFcards { get; set; }
        public virtual DbSet<MemberRoster> MemberRosters { get; set; }
        public virtual DbSet<MonitorParam> MonitorParams { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<BadgeCode> BadgeCodes { get; set; }
        public virtual DbSet<Time_zones> Time_zones { get; set; }
        public virtual DbSet<MemberBackup> MemberBackups { get; set; }
        public virtual DbSet<MachineApprover> MachineApprovers { get; set; }
        public virtual DbSet<MachinePerm> MachinePerms { get; set; }
    }
}
