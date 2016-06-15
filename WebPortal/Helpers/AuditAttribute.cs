using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPortal.DataModels;
using System.Net;
namespace WebPortal.Models
{
    public class AuditAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext auditContext)
        {
            var req = auditContext.HttpContext.Request;

            PortalAudit aud = new PortalAudit()
            {
                Audit_Id = Guid.NewGuid(),
                UserName = (req.IsAuthenticated) ? auditContext.HttpContext.User.Identity.Name : "Anonymous",
                //Hostname = Dns.GetHostEntry(req.UserHostAddress).HostName,
                IPAddress = req.ServerVariables["HTTP_X_FORWADED_FOR"] ?? req.UserHostAddress,
                PageAccessed = req.RawUrl,
                TimeStamp = DateTime.Now                
            };

            AuditContext ctx = new AuditContext();
            ctx.PortalAudits.Add(aud);
            ctx.SaveChanges();

            base.OnActionExecuting(auditContext);
        }
    }
}