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
    
    public partial class webportal_patient_prev_hospitalization
    {
        public System.Guid patient_visit_id { get; set; }
        public System.Guid patient_id { get; set; }
        public string hospital_nr { get; set; }
        public string last_name_l { get; set; }
        public string first_name_l { get; set; }
        public string middle_name_l { get; set; }
        public Nullable<System.DateTime> date_of_birth { get; set; }
        public Nullable<int> age { get; set; }
        public string gender_rcd { get; set; }
        public string gender { get; set; }
        public Nullable<System.DateTime> visit_start_date_time { get; set; }
        public Nullable<System.DateTime> visit_end_date_time { get; set; }
        public Nullable<System.DateTime> cancelled_date_time { get; set; }
        public string visit_code { get; set; }
        public string visit_type_rcd { get; set; }
        public string visit_type { get; set; }
        public string charge_type_rcd { get; set; }
        public string charge_type { get; set; }
        public string primary_service_rcd { get; set; }
        public string primary_service { get; set; }
        public System.Guid created_by { get; set; }
    }
}
