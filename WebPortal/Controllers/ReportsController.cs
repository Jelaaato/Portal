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

        [Authorize(Roles = "Administrator, OMCPUsers")]
        public ActionResult OutpatientMedicalCareProfile(string search)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 4)
            {
                try
                {
                    Guid query = new Guid();

                    query = (from b in patdb.patient_hospital_usage where b.visible_patient_id == search select b.patient_id).FirstOrDefault();

                    if (query != new Guid("00000000-0000-0000-0000-000000000000"))
                    {
                        OPSViewModel patientinfo = new OPSViewModel()
                        {
                            patient_info = db.webportal_patient_info.Where(a => a.person_id == query).ToList()
                        };

                        Session["searchstring"] = search;
                        Session["pid"] = query;
                        ViewBag.Successful = "true";
                        ModelState.Clear();
                        return View(patientinfo) ;
                    }

                    else
                    {
                        ViewBag.Message = "Hospital Number not found.";
                        return View();
                    }
                }
                catch (Exception)
                {
                    retry++;
                }
            }

            return View();
        }

        public ActionResult OMCPpatientAllergy(int? page, string currentfilter)
        {
            var pid = new Guid(Session["pid"].ToString());
            var searchstring = Session["searchstring"].ToString();


            if (searchstring != null && page < 1)
            {
                page = 1;
            }
            else
            {
                searchstring = currentfilter;
            }
           
            ViewBag.CurrentFilter = searchstring;

            int pageSize = 10;
            int pageNumber = (page ?? 1);

               var patientallergy = db.webportal_patient_allergies_new.Where(a => a.patient_id == pid).OrderBy(a => a.cause).ToPagedList(pageNumber, pageSize);
                
                return PartialView("_patallergy", patientallergy);
            }

        public ActionResult OMCPpatientPrevHosp(int? page, string currentfilter)
        {
            var pid = new Guid(Session["pid"].ToString());
            var searchstring = Session["searchstring"].ToString();

            //if (Request.IsAjaxRequest())
            //{
                if (searchstring != null && page < 1)
                {
                    page = 1;
                }
                else
                {
                    searchstring = currentfilter;
                }

                ViewBag.CurrentFilter = searchstring;

                int pageSize = 10;
                int pageNumber = (page ?? 1);

                var prevhosp = db.webportal_patient_prev_hospitalization.Where(a => a.patient_id == pid).OrderByDescending(a => a.visit_start_date_time).ToPagedList(pageNumber, pageSize);

                return PartialView("_prevhosp", prevhosp);
            }
            //else
            //{
            //    return PartialView("_prevhosp");
            //}

        public ActionResult OMCPpatientDiagnosis(int? page, string currentfilter)
        {
            var pid = new Guid(Session["pid"].ToString());
            var searchstring = Session["searchstring"].ToString();

            //if (Request.IsAjaxRequest())
            //{
            if (searchstring != null && page < 1)
            {
                page = 1;
            }
            else
            {
                searchstring = currentfilter;
            }

            ViewBag.CurrentFilter = searchstring;

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var patientdiagnosis = db.webportal_patient_diagnosis.Where(a => a.patient_id == pid).OrderByDescending(a => a.recorded_at_date_time).ToPagedList(pageNumber, pageSize);

            return PartialView("_patdiag", patientdiagnosis);
        }

        public ActionResult OMCPpatientMedication(int? page, string currentfilter)
        {
            var pid = new Guid(Session["pid"].ToString());
            var searchstring = Session["searchstring"].ToString();

            //if (Request.IsAjaxRequest())
            //{
            if (searchstring != null && page < 1)
            {
                page = 1;
            }
            else
            {
                searchstring = currentfilter;
            }

            ViewBag.CurrentFilter = searchstring;

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var patientmedication = db.webportal_patient_medication.Where(a => a.patient_id == pid).OrderByDescending(a => a.note_date).ToPagedList(pageNumber, pageSize);

            return PartialView("_patmedic", patientmedication);
        }

        public ActionResult OMCPpatientPrevSurg(int? page, string currentfilter)
        {
            var pid = new Guid(Session["pid"].ToString());
            var searchstring = Session["searchstring"].ToString();

            //if (Request.IsAjaxRequest())
            //{
            if (searchstring != null && page < 1)
            {
                page = 1;
            }
            else
            {
                searchstring = currentfilter;
            }

            ViewBag.CurrentFilter = searchstring;

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var prevsurg = db.webportal_patient_prev_surgeries.Where(a => a.patient_id == pid).OrderBy(a => a.previous_surgeries).ToPagedList(pageNumber, pageSize);

            return PartialView("_prevsurg", prevsurg);
        }
    }
}