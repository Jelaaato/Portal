using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using WebPortal.DataModels;

namespace WebPortal.DataModels
{
    public class PortalDbConfiguration : DbConfiguration
    {
        public PortalDbConfiguration()
        {
            SetExecutionStrategy("System.Data.EntityClient", () => new PortalExecutionStrategy());
        }
    }
}