using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPortal.Models;
using PagedList.Mvc;
using PagedList;

namespace WebPortal.Controllers
{
    public class ReportsController : Controller
    {
        private OPSEntities db = new OPSEntities();
        //private opstryEntities db = new opstryEntities();
        private PatientUserEntities patdb = new PatientUserEntities();
        
        // GET: Reports

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult OutpatientMedicalCareProfile()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult OutpatientMedicalCareProfile(string search, int? page, string currentfilter)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    Guid query = new Guid();

                    query = (from b in patdb.patient_hospital_usage where b.visible_patient_id == search select b.patient_id).FirstOrDefault();

                    if (search != null)
                    {
                        page = 1;
                    }
                    else
                    {
                        search = currentfilter;
                    }

                    ViewBag.CurrentFilter = search;

                    int pageSize = 10;
                    int pageNumber = (page ?? 1);

                    OPSViewModel opsmodel = new OPSViewModel()
                    {
                        patient_allergies = db.webportal_patient_allergies_new.Where(a => a.patient_id == query).OrderBy(a => a.cause).ToPagedList(pageNumber, pageSize),
                        patient_diagnosis = db.webportal_patient_diagnosis.Where(a => a.patient_id == query).OrderBy(a => a.recorded_at_date_time).ToPagedList(pageNumber, pageSize),
                        patient_medication = db.webportal_patient_medication.Where(a => a.patient_id == query).OrderBy(a => a.note_date).ToPagedList(pageNumber, pageSize),
                        patient_prev_hosp = db.webportal_patient_prev_hospitalization.Where(a => a.patient_id == query).OrderBy(a => a.visit_start_date_time).ToPagedList(pageNumber, pageSize),
                        patient_prev_surgeries = db.webportal_patient_prev_surgeries.Where(a => a.patient_id == query).OrderBy(a => a.previous_surgeries).ToPagedList(pageNumber, pageSize)
                    };

                    //OPSViewModel opsmodel = new OPSViewModel()
                    //{
                    //    patient_allergies = db.patient_allergy.Where(a => a.patient_id == query).OrderBy(a => a.allergen).ToList(),
                    //    patient_diagnosis = db.patient_visit_diagnosis.Where(a => a.patient_id == query).OrderBy(a => a.recorded_at_date_time).ToList(),
                    //    patient_medication = db.patient_medication.Where(a => a.patient_id == query).OrderBy(a => a.note_date).ToList(),
                    //    patient_prev_hosp = db.patient_visit.Where(a => a.patient_id == query).OrderBy(a => a.visit_start_date_time).ToList(),
                    //    patient_prev_surgeries = db.patient_previous_surgery.Where(a => a.patient_id == query).OrderBy(a => a.previous_surgeries).ToList()
                    //};
                    
                    if (opsmodel != null)
                    {
                        successful = true;
                        ViewBag.Successful = "true";
                    }
                    else
                    {
                        successful = false;
                        ViewBag.Successful = "false";
                    }
 

                    return View(opsmodel);
                }
                catch (Exception)
                {
                    retry++;
                }
        }
            return View();
        }
        
    }
}