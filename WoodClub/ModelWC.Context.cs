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
    
    public partial class WoodclubEntities : DbContext
    {
        public WoodclubEntities()
            : base("name=WoodclubEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MemberRFcard> MemberRFcards { get; set; }
        public virtual DbSet<MemberRoster> MemberRosters { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<MemberPhoto> MemberPhotos { get; set; }
        public virtual DbSet<Time_zones> Time_zones { get; set; }
        public virtual DbSet<BadgeCode> BadgeCodes { get; set; }
        public virtual DbSet<LockerCost> LockerCosts { get; set; }
        public virtual DbSet<LockerLocation> LockerLocations { get; set; }
        public virtual DbSet<Locker> Lockers { get; set; }
    }
}
