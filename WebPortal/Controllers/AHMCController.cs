using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebPortal.DataModels;
using WebPortal.Models;
using Microsoft.AspNet.Identity;

namespace WebPortal.Controllers
{
    public class AHMCController : Controller
    {
        [Audit]
        [Authorize]
        public ActionResult Index()
        {
            PatientUserEntities patientdb = new PatientUserEntities();
            EmployeeandDoctorEntities empdocdb = new EmployeeandDoctorEntities();
            var currentuser = User.Identity.GetUserName().ToString();

            bool returnvalue = false;
            bool successful = false;

            int retry = 0;

            while (!successful && retry < 4)
            {
                try
                {
                    do
                    {
                        if (User.IsInRole("Patient"))
                        {
                            var displaycurrentuser = (from pn in patientdb.person_formatted_name_iview
                                                      join ph in patientdb.patient_hospital_usage
                                                      on pn.person_id equals ph.patient_id
                                                      select new
                                                      {
                                                          Name = pn.display_name_l,
                                                          ph.visible_patient_id
                                                      }).Where(a => a.visible_patient_id == currentuser).FirstOrDefault();
                            if (displaycurrentuser != null)
                            {
                                returnvalue = true;
                                Session["displaycurrentuser"] = displaycurrentuser.Name;
                                successful = true;
                            }
                            else
                            {
                                returnvalue = false;
                            }
                        }
                        else
                        {
                            var displaycurrentuser = (from en in empdocdb.employee_formatted_name_iview_nl_view
                                                      join e in empdocdb.employee
                                                      on en.person_id equals e.employee_id
                                                      select new
                                                      {
                                                          Name = en.display_name_l,
                                                          e.employee_nr
                                                      }).Where(a => a.employee_nr.ToString() == currentuser).FirstOrDefault();
                            if (displaycurrentuser != null)
                            {
                                returnvalue = true;
                                Session["displaycurrentuser"] = displaycurrentuser.Name;
                                successful = true;
                            }
                            else
                            {
                                returnvalue = false;
                            }
                        }
                    }
                    while (returnvalue == false);
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            

            return View();
        }

        //private Dictionary<string, object> GetData(string ActionName)
        //{
        //    Dictionary<string, object> dta = new Dictionary<string, object>();
        //    dta.Add("Action", ActionName);
        //    dta.Add("User", HttpContext.User.Identity.Name);
        //    dta.Add("Authenticated", HttpContext.User.Identity.IsAuthenticated);
        //    dta.Add("Authentication Type", HttpContext.User.Identity.AuthenticationType);
        //    dta.Add("In Standard Users Role", HttpContext.User.IsInRole("Standard Users"));
        //    dta.Add("In Administrators Role", HttpContext.User.IsInRole("Administrators"));
        //    return dta;
        //}
    }
}