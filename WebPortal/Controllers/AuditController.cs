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
    public class AuditController : Controller
    {
        [Authorize(Roles = "Administrator")]
        public ActionResult Index(string searchuser, DateTime? datefrom, DateTime? dateto, string filter, int? page)
        {
            using (AuditContext db = new AuditContext())
            {
                try
                {
                    var auditrecords = from a in db.PortalAudits select a;

                    if (searchuser != null)
                    {
                        page = 1;
                    }
                    else
                    {
                        searchuser = filter;
                    }

                    ViewBag.Filter = searchuser;

                    if (!String.IsNullOrEmpty(searchuser) && (datefrom != null && dateto != null))
                    {
                        auditrecords = auditrecords
                                      .Where(a => a.UserName.Contains(searchuser) && a.TimeStamp >= datefrom && a.TimeStamp <= dateto)
                                      .OrderBy(a => a.TimeStamp);
                    }
                    else

                    {
                        auditrecords = new AuditContext().PortalAudits.OrderBy(a => a.TimeStamp);
                    }

                    int pageSize = 5;
                    int pageNumber = (page ?? 1);
                    return View(auditrecords.ToPagedList(pageNumber, pageSize));
                }

                catch (Exception e)
                {
                    return View("~/Views/Shared/Errors.cshtml", e);
                }
            }
        }
    }
}