using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    public class AdminSettingsController : Controller
    {
        private RetentionEntities db = new RetentionEntities();

        [Authorize(Roles = "Administrator")]
        public ActionResult Index(ResultModel model)
        {
            return View(db.PortalRetention.ToList());
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ResultModel model)
        {
                if ((model.retentionperiod <= 0) || (model.retentionperiod > 30))
                {
                    @ViewBag.Error = "Please enter a value greater than 0 and less than 30 days.";
                }
                else
                {
                    int period = model.retentionperiod * -1;
                    PortalRetention retention = new PortalRetention()
                    {
                        ID = Guid.NewGuid(),
                        Module = model.module,
                        Retention_Period = period,
                    };

                    db.PortalRetention.Add(retention);
                    db.SaveChanges();
                    return RedirectToAction("Index"); 
               }
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortalRetention portalRetention = db.PortalRetention.Find(id);
            if (portalRetention == null)
            {
                return HttpNotFound();
            }
            return View(portalRetention);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PortalRetention model, Guid id)
        {
            if ((model.Retention_Period <= 0) || (model.Retention_Period > 30))
            {
                @ViewBag.Error = "Please enter a value greater than 0 and less than 30 days.";
            }
            else
            {
                PortalRetention module = db.PortalRetention.First(a => a.ID == id);
                module.Retention_Period = model.Retention_Period * -1;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //public ActionResult Delete(Guid? id)
        //{
        //    return View(
        //}

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(Guid? id)
        {
            PortalRetention portalretention = db.PortalRetention.Find(id);
            db.PortalRetention.Remove(portalretention);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}