using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DB.Util;

namespace Sorting.Dispatching.Dao
{
    class HandleSupplyDao : BaseDao
    {
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        public void DeleteHistory(string orderDate)
        {
            string sql = string.Format("DELETE FROM SC_HANDLESUPPLY WHERE ORDERDATE < '{0}'", orderDate);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void DeleteHandleSupply(string batchNo)
        {
            string sql = string.Format("DELETE FROM SC_HANDLESUPPLY WHERE BATCHNO='{0}'", batchNo);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void DeleteHandleSupply(string orderDate, string batchNo)
        {
            string sql = string.Format("DELETE FROM SC_HANDLESUPPLY WHERE ORDERDATE = '{0}' AND BATCHNO='{1}'", orderDate, batchNo);
            ExecuteNonQuery(sql);
        }
    }
}
