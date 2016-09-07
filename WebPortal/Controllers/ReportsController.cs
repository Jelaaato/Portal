using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPortal.Models;
using PagedList.Mvc;
using PagedList;
using WebPortal.Helpers;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;

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

                            //OPSMethods ops = new OPSMethods();
                            //var patientinfo = ops.GetInfo(query);

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

        public ActionResult OMCPpatientPrevHosp(int? page, string currentfilter)
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
            var pid = new Guid(Session["pid"].ToString());
            var searchstring = Session["searchstring"].ToString();

            var patInfo = db.webportal_patient_info.Where(i => i.person_id == pid).AsEnumerable();
            var patAllergy = db.webportal_patient_allergies_new.Where(a => a.patient_id == pid).AsEnumerable();
            var patMedication = db.webportal_patient_medication.Where(a => a.patient_id == pid).AsEnumerable();
            var patDiagnosis = db.webportal_patient_diagnosis.Where(a => a.patient_id == pid).AsEnumerable();
            var patPrevSurg = db.webportal_patient_prev_surgeries.Where(a => a.patient_id == pid).AsEnumerable();
            var patPrevHosp = db.webportal_patient_prev_hospitalization.Where(a => a.patient_id == pid).OrderByDescending(a => a.visit_start_date_time).AsEnumerable();

            Chunk hr = new Chunk(new LineSeparator());

            Document doc = new Document();
            MemoryStream mst = new MemoryStream();

            PdfWriter pdfwriter = PdfWriter.GetInstance(doc, mst);
            pdfwriter.CloseStream = false;


            doc.Open();
            doc.Add(new Phrase("Out Patient Summary"));
            doc.Add(new Paragraph("\n"));

            foreach (var item in patInfo)
            {
                doc.Add(new Paragraph(item.last_name_l + "," + " " + item.first_name_l + " " + item.middle_name_l));
                doc.Add(new Paragraph(item.hospital_nr));
                doc.Add(new Paragraph(item.date_of_birth.Value.ToString("dd MMMM yyyy")));
                doc.Add(new Paragraph(item.age.ToString() + " y.o"));
                doc.Add(new Paragraph(item.gender));
            }


            doc.Add(hr);
            doc.Add(new Paragraph("\n"));

            doc.Add(new Phrase("Allergies"));
            PdfPTable tblAllergy = new PdfPTable(3);
            tblAllergy.WidthPercentage = 100;

            if (patAllergy.Count() != 0)
            {
                tblAllergy.AddCell(new Phrase("Allergen"));
                tblAllergy.AddCell(new Phrase("Allergic Reaction"));
                tblAllergy.AddCell(new Phrase("Allergy Status"));

                foreach (var item in patAllergy)
                {
                    tblAllergy.AddCell(item.cause);
                    tblAllergy.AddCell(item.reaction);
                    tblAllergy.AddCell(item.reaction_cause_status);
                }
            }
            else 
            {
                doc.Add(new Paragraph("No records found."));  
            }

            doc.Add(tblAllergy);
            doc.Add(new Paragraph("\n"));

            doc.Add(new Phrase("Medication"));
            PdfPTable tblMedic = new PdfPTable(2);
            tblMedic.WidthPercentage = 100;

            if (patMedication.Count() != 0)
            {
                tblMedic.AddCell(new Phrase("Date and Time Recorded"));
                tblMedic.AddCell(new Phrase("Medicine"));

                foreach (var item in patMedication)
                {
                    tblMedic.AddCell(item.note_date.ToString());
                    tblMedic.AddCell(item.details);
                }
            }
            else 
            {
                doc.Add(new Paragraph("No records found.")); 
            }

            doc.Add(tblMedic);
            doc.Add(new Paragraph("\n"));

            doc.Add(new Phrase("Diagnosis"));
            PdfPTable tblDiag = new PdfPTable(5);
            tblDiag.WidthPercentage = 100;

            if (patDiagnosis.Count() != 0)
            {
                tblDiag.AddCell(new Phrase("Date and Time"));
                tblDiag.AddCell(new Phrase("Diagnosis"));
                tblDiag.AddCell(new Phrase("Code"));
                tblDiag.AddCell(new Phrase("Coding System"));
                tblDiag.AddCell(new Phrase("Coding Type"));

                foreach (var item in patDiagnosis)
                {
                    tblDiag.AddCell(item.recorded_at_date_time.ToString());
                    tblDiag.AddCell(item.diagnosis);
                    tblDiag.AddCell(item.code);
                    tblDiag.AddCell(item.coding_system_rcd);
                    tblDiag.AddCell(item.coding_type);
                }
            }
            else
            {
                doc.Add(new Paragraph("No records found."));
            }

            doc.Add(tblDiag);
            doc.Add(new Paragraph("\n"));

            doc.Add(new Phrase("Previous Surgeries"));
            PdfPTable tblPrevSurg = new PdfPTable(1);
            tblPrevSurg.WidthPercentage = 100;

            if (patPrevSurg.Count() != 0)
            {
                tblPrevSurg.AddCell(new Phrase("Procedure"));

                foreach (var item in patPrevSurg)
                {
                    tblPrevSurg.AddCell(item.previous_surgeries);
                }
            }
            else
            {
                doc.Add(new Paragraph("No records found."));
            }

            doc.Add(tblPrevSurg);
            doc.Add(new Paragraph("\n"));

            doc.Add(new Phrase("Previous Hospitalizations"));
            PdfPTable tblPrevHosp = new PdfPTable(3);
            tblPrevHosp.WidthPercentage = 100;

            if (patPrevHosp.Count() != 0)
            {

                tblPrevHosp.AddCell(new Phrase("Visit Date"));
                tblPrevHosp.AddCell(new Phrase("Visit Type"));
                tblPrevHosp.AddCell(new Phrase("Primary Service"));

                foreach (var item in patPrevHosp)
                {
                    tblPrevHosp.AddCell(item.visit_start_date_time.ToString());
                    tblPrevHosp.AddCell(item.visit_type);
                    tblPrevHosp.AddCell(item.primary_service);
                }
            }
            else
            {
                doc.Add(new Paragraph("No records found."));
            }

            doc.Add(tblPrevHosp);
            

            doc.Add(new Paragraph("\n"));
            doc.Add(hr);

            doc.Add(new Paragraph(doc.BottomMargin, DateTime.Now.ToString()));
            doc.Close();

            mst.Flush();
            mst.Position = 0;

            Response.AddHeader("content-disposition", "inline; filename=OMCP.pdf");
            return File(mst, "application/pdf");
        }
    }
}