using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortal.Models
{
    public class AllergiesDTO
    {
        public Guid patient_id { get; set; }
        public string allergen { get; set; }
        public string allergy_status { get; set; }
        public string adverse_reaction { get; set; }
    }
}