using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortal.Models
{
    public class OPSViewModel
    {
        public IPagedList<webportal_patient_allergies> patient_allergies { get; set; }
        public IPagedList<webportal_patient_diagnosis> patient_diagnosis { get; set; }
        public IPagedList<webportal_patient_medication> patient_medication { get; set; }
        public IPagedList<webportal_patient_prev_hospitalization> patient_prev_hosp { get; set; }
        public IPagedList<webportal_patient_prev_surgeries> patient_prev_surgeries { get; set; }
    }
}