using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortal.Models
{
    public class OPSViewModel
    {
        public IEnumerable<webportal_patient_info> patient_info { get; set; }
        public PagedList.IPagedList<webportal_patient_allergies_new> patient_allergies { get; set; }
        public PagedList.IPagedList<webportal_patient_diagnosis> patient_diagnosis { get; set; }
        public PagedList.IPagedList<webportal_patient_medication> patient_medication { get; set; }
        public PagedList.IPagedList<webportal_patient_prev_hospitalization> patient_prev_hosp { get; set; }
        public PagedList.IPagedList<webportal_patient_prev_surgeries> patient_prev_surgeries { get; set; }
    }


    public class patientAllergiesModel
    {
        public PagedList.IPagedList<webportal_patient_allergies_new> patient_allergies { get; set; }
    }

    public class patientDiagnosisModel
    {
        public PagedList.IPagedList<webportal_patient_diagnosis> patient_diagnosis { get; set; }
    }

    public class patientMedicationModel
    {
        public PagedList.IPagedList<webportal_patient_medication> patient_medication { get; set; }
    }

    public class patientPrevHospModel
    {
        public PagedList.IPagedList<webportal_patient_prev_hospitalization> patient_prev_hosp { get; set; }
    }

    public class patientPrevSurgModel
    {
        public PagedList.IPagedList<webportal_patient_prev_surgeries> patient_prev_surgeries { get; set; }
    }

    public class patientInfoModel
    {
        public IEnumerable<webportal_patient_info> patient_info { get; set; }
    }

    //public class OPSViewModel
    //{
    //    public IEnumerable<patient_allergy> patient_allergies { get; set; }
    //    public IEnumerable<patient_visit_diagnosis> patient_diagnosis { get; set; }
    //    public IEnumerable<patient_medication> patient_medication { get; set; }
    //    public IEnumerable<patient_visit> patient_prev_hosp { get; set; }
    //    public IEnumerable<patient_previous_surgery> patient_prev_surgeries { get; set; }
    //}
}