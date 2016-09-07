using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPortal.Models;

namespace WebPortal.Helpers
{
    public class OPSMethods
    {
        private OPSEntities db = new OPSEntities();

        public IEnumerable<webportal_patient_info> GetInfo(Guid id)
        {
            var pat_id = id.ToString();
            var patInfo = db.webportal_patient_info.Where(a => a.hospital_nr == pat_id).AsEnumerable();

            return patInfo;
        }
    }
}