﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebPortal.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OPSEntities : DbContext
    {
        public OPSEntities()
            : base("name=OPSEntities")
        {
            this.SetCommandTimeout(300);
        }

        public void SetCommandTimeout(int timeout)
        {
            var objContext = (this as IObjectContextAdapter).ObjectContext;
            objContext.CommandTimeout = timeout;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<webportal_patient_allergies_new> webportal_patient_allergies_new { get; set; }
        public virtual DbSet<webportal_patient_diagnosis> webportal_patient_diagnosis { get; set; }
        public virtual DbSet<webportal_patient_info> webportal_patient_info { get; set; }
        public virtual DbSet<webportal_patient_medication> webportal_patient_medication { get; set; }
        public virtual DbSet<webportal_patient_prev_hospitalization> webportal_patient_prev_hospitalization { get; set; }
        public virtual DbSet<webportal_patient_prev_surgeries> webportal_patient_prev_surgeries { get; set; }
    }
}
