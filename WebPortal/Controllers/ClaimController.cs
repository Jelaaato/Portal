using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace WebPortal.Controllers
{
    public class ClaimController : Controller
    {
        // GET: Claim
        public ActionResult Index()
        {
            ClaimsIdentity _claimident = HttpContext.User.Identity as ClaimsIdentity;
            if (_claimident == null)
            {
                return View("Error", new string[] { "There are no claims available" });
            }
            else 
            {
                return View(_claimident.Claims);    
            }
        }
    }
}