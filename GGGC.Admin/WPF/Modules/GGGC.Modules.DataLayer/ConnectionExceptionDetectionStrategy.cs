using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling;
using Microsoft.Practices.TransientFaultHandling;

namespace GGGC.Modules.DataLayer
{
    public class ConnectionExceptionDetectionStrategy : ITransientErrorDetectionStrategy
    {

        public bool IsTransient(Exception ex)
        {
            // Detect a network connection error
            bool isTransient = (ex is SqlException
                && ((SqlException)ex).Class == 20
                && ((SqlException)ex).Number == 53);

            //// If error is different implement the default strategy
            //if (!isTransient)
            //{
            //    // Retrieve the retry mgr of the configuration
            //    var retryMgr = EnterpriseLibraryContainer.Current.GetInstance<RetryManager>();
            //    RetryPolicy rp = retryMgr.GetDefaultSqlConnectionRetryPolicy();
            //    isTransient = rp.ErrorDetectionStrategy.IsTransient(ex);
            //}
            return isTransient;
        }

    }
}
