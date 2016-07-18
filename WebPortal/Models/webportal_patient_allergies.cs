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
    
    public partial class webportal_patient_allergies
    {
        public System.Guid patient_id { get; set; }
        public System.Guid patient_adverse_reaction_cause_id { get; set; }
        public string adverse_reaction_cause_type_rcd { get; set; }
        public string adverse_reaction_cause_type { get; set; }
        public Nullable<System.Guid> adverse_reaction_cause_rid { get; set; }
        public string adverse_reaction_cause_name { get; set; }
        public string adverse_reaction_cause_free_text { get; set; }
        public string adverse_reaction_cause { get; set; }
        public string patient_adverse_reaction_cause_status_rcd { get; set; }
        public string patient_adverse_reaction_cause_status { get; set; }
        public string allergen { get; set; }
        public string allergy_status_rcd { get; set; }
        public string allergy_status { get; set; }
        public Nullable<System.Guid> adverse_reaction_rid { get; set; }
        public string adverse_reaction_name { get; set; }
        public string adverse_reaction_free_text { get; set; }
        public string adverse_reaction { get; set; }
        public string life_threatening_status_rcd { get; set; }
        public string life_threatening_status { get; set; }
        public string dose_related_status_rcd { get; set; }
        public string dose_related_status { get; set; }
        public Nullable<bool> avoid_in_future_flag { get; set; }
        public string patient_adverse_reaction_status_rcd { get; set; }
        public string patient_adverse_reaction_status { get; set; }
        public Nullable<bool> active_flag { get; set; }
        public Nullable<bool> allergic_reaction_flag { get; set; }
    }
}