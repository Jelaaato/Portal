using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPortal.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.Owin.Security;
using WebPortal.DataModels;
using System.IO;

namespace WebPortal.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}