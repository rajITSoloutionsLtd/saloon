﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace demo.Demo.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SMSEntities : DbContext
    {
        public SMSEntities()
            : base("name=SMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Instrument_Table> Instrument_Table { get; set; }
        public virtual DbSet<Service_Table> Service_Table { get; set; }
        public virtual DbSet<ServiceType> ServiceTypes { get; set; }
        public virtual DbSet<Token_Dettails_Table> Token_Dettails_Table { get; set; }
        public virtual DbSet<Token_Table> Token_Table { get; set; }
        public virtual DbSet<tblServiceAgentMapping> tblServiceAgentMappings { get; set; }
        public virtual DbSet<Agent_Table> Agent_Table { get; set; }
        public virtual DbSet<TblSrvcAgntMapDettail> TblSrvcAgntMapDettails { get; set; }
        public virtual DbSet<TblOfCustomer> TblOfCustomers { get; set; }
        public virtual DbSet<Customer_Table> Customer_Table { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<tblToken_dettails_Service> tblToken_dettails_Service { get; set; }
    }
}
