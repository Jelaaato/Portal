using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    public class ReportsController : Controller
    {
        private OPSEntities db = new OPSEntities();
        private PatientUserEntities patdb = new PatientUserEntities();
        // GET: Reports

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult OutpatientMedicalCareProfile(string search, int? page, string currentfilter)
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

            //int pageSize = 5;
            //int pageNumber = (page ?? 1);

            var allergies = from a in db.webportal_patient_allergies
                            where a.patient_id == query
                            select new webportal_patient_allergies
                            {
                                patient_id = a.patient_id,
                                allergen = a.allergen,
                                allergy_status = a.allergy_status,
                                adverse_reaction = a.adverse_reaction
                            };

            var opsmodel = new OPSViewModel
            {
                patient_allergies = allergies,
                //patient_medication = db.webportal_patient_medication.Where(x => x.patient_id == query).OrderBy(x => x.note_date).ToList(),
                //patient_prev_surgeries = db.webportal_patient_prev_surgeries.Where(x => x.patient_id == query).OrderBy(x => x.previous_surgeries).ToList(),
                //patient_diagnosis = db.webportal_patient_diagnosis.Where(x => x. == query).OrderBy(x => x.recorded_at_date_time).ToList(),
                //patient_prev_hospitalization = db.webportal_patient_prev_hospitalization.Where(x => x.patient_id == query).OrderBy(x => x.visit_start_date_time).ToList()
            };
            return View(opsmodel);
        }
    }
}