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
    
    public partial class person_formatted_name_iview
    {
        public System.Guid person_id { get; set; }
        public string display_name_l { get; set; }
        public string display_name_e { get; set; }
        public string legal_name_l { get; set; }
        public string legal_name_e { get; set; }
        public string list_name_l { get; set; }
        public string list_name_e { get; set; }
        public Nullable<System.DateTime> date_of_birth { get; set; }
        public string nationality_rcd { get; set; }
        public string sex_rcd { get; set; }
        public bool test_flag { get; set; }
        public Nullable<System.Guid> photo_id { get; set; }
    }
}
