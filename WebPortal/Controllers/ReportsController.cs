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
                    if (search != null)
                    {
                        if (patdb.patient_hospital_usage.Any(a => a.visible_patient_id == search))
                        {
                            Guid query = new Guid();
                            query = (from b in patdb.patient_hospital_usage where b.visible_patient_id == search select b.patient_id).FirstOrDefault();
                            OPSViewModel patientinfo = new OPSViewModel()
                            {
                                patient_info = db.webportal_patient_info.Where(a => a.person_id == query).ToList()
                            };

                            Session["searchstring"] = search;
                            Session["pid"] = query;
                            ViewBag.Successful = "true";
                            ModelState.Clear();
                            return View(patientinfo);
                        }
                        else
                        {
                            ViewBag.ShowModal = "true";
                            ModelState.Clear();
                            return View();
                        }
                    }
                    else
                    {
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

            bool successful = false;
            int retry = 0;

            while (!successful && retry < 4)
            {
                try
                {
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
                catch (Exception)
                {
                    retry++;
                }
            }
            return PartialView("_patallergy");
        }

        public ActionResult OMCPpatientPrevHosp(int? page, string currentfilter, string sortorder)
        {
            var pid = new Guid(Session["pid"].ToString());
            var searchstring = Session["searchstring"].ToString();
            ViewBag.DateSort = sortorder == "Date" ? "Date_desc" : "Date";

            bool successful = false;
            int retry = 0;

            while (!successful && retry < 4)
            {
                try
                {
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
                catch (Exception)
                {
                    retry++;
                }

            }
            return PartialView("_prevhosp");
        }

        public ActionResult OMCPpatientDiagnosis(int? page, string currentfilter)
        {
            var pid = new Guid(Session["pid"].ToString());
            var searchstring = Session["searchstring"].ToString();

            //if (Request.IsAjaxRequest())
            //{
            bool successful = false;
            int retry = 0;

            while (!successful && retry < 4)
            {
                try
                {
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
                catch (Exception)
                {
                    retry++;
                }
            }
            return PartialView("_patdiag");
        }

        public ActionResult OMCPpatientMedication(int? page, string currentfilter)
        {
            var pid = new Guid(Session["pid"].ToString());
            var searchstring = Session["searchstring"].ToString();

            bool successful = false;
            int retry = 0;

            while (!successful && retry < 4)
            {
                try
                {
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
                catch (Exception)
                {
                    retry++;
                }
            }
            return PartialView("_patmedic");
        }

        public ActionResult OMCPpatientPrevSurg(int? page, string currentfilter)
        {
            var pid = new Guid(Session["pid"].ToString());
            var searchstring = Session["searchstring"].ToString();

            bool successful = false;
            int retry = 0;

            while (!successful && retry < 4)
            {
                try
                {
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
                catch (Exception)
                {
                    retry++;
                }
            }
            return PartialView("_prevsurg");
        }

        public ActionResult GenerateOMCPPDF()
        {
            return new Rotativa.ActionAsPdf("OMCPpatientAllergy");
        }
    }
}