using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortal.Models
{
    public class OPSViewModel
    {
        public IEnumerable<webportal_patient_info> patient_info { get; set; }
    }

}