//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class webportal_patient_prev_surgeries
    {
        public System.Guid patient_id { get; set; }
        public string hospital_nr { get; set; }
        public string patient_name { get; set; }
        public Nullable<System.DateTime> date_of_birth { get; set; }
        public string nationality_rcd { get; set; }
        public string nationality { get; set; }
        public string residence_country_rcd { get; set; }
        public string residence_country { get; set; }
        public string religion_rcd { get; set; }
        public string religion { get; set; }
        public string ethnic_group_rcd { get; set; }
        public string ethnic_group { get; set; }
        public string race_rcd { get; set; }
        public string race { get; set; }
        public string previous_surgeries { get; set; }
    }
}