using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebPortal.DataModels
{
    public class PortalExecutionStrategy : DbExecutionStrategy
    {
        public PortalExecutionStrategy()
        { 
        
        }

        public PortalExecutionStrategy(int maxRetryCount, TimeSpan maxDelay)
            : base(maxRetryCount, maxDelay)
        { 
        
        }

        protected override bool ShouldRetryOn(Exception exception)
        {
            bool retry = false;

            SqlException sqlex = exception as SqlException;
            if (sqlex != null)
            {
                int[] sqlexError =
                {
                    1205, //deadlock
                    -2, //timeout
                    922, //database is in recovery
                    65535, // connection closed by peer
                    3065 //cannot execute select query
                };
                if (sqlex.Errors.Cast<SqlError>().Any(x => sqlexError.Contains(x.Number)))
                {
                    retry = true;
                }
                else 
                {
                    
                }
            }
            if (exception is TimeoutException)
            {
                retry = true;
            }
            return retry;
        }

    }
}