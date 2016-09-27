using System;
using System.Collections.Generic;
using System.Text;
using DB.Util;
using System.Data;
namespace Sorting.Dispatching.Dao
{
    public class ChannelScheduleDao : BaseDao
    {
        public DataTable FindAll(int startRecord, int pageSize, string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            string sql = "SELECT *,QUANTITY / 50 JQUANTITY, QUANTITY % 50 LQUANTITY FROM SC_CHANNELUSED " + where;
            sql += " ORDER BY ORDERDATE, BATCHNO, LINECODE, CHANNELCODE";
            return ExecuteQuery(sql, "CMD_AREA", startRecord, pageSize).Tables[0];
        }

        public int FindCount(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);

            string sql = "SELECT COUNT(*) FROM SC_CHANNELUSED " + where;
            return (int)ExecuteScalar(sql);
        }

        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        public void DeleteHistory(string orderDate)
        {
            string sql = string.Format("DELETE FROM SC_CHANNELUSED WHERE ORDERDATE < '{0}'", orderDate); 
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void DeleteSchedule()
        {
            string sql = "DELETE FROM SC_CHANNELUSEDH WHERE BATCHNO IN (SELECT DISTINCT BATCHNO FROM SC_CHANNELUSED)";
            ExecuteNonQuery(sql);

            sql = "INSERT INTO SC_CHANNELUSEDH SELECT * FROM SC_CHANNELUSED";
            ExecuteNonQuery(sql);

            sql = string.Format("DELETE FROM SC_CHANNELUSED");
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void DeleteSchedule(string batchNo)
        {
            string sql = string.Format("DELETE FROM SC_CHANNELUSED WHERE BATCHNO = '{0}'", batchNo);
            ExecuteNonQuery(sql);            
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void DeleteSchedule(string orderDate, string batchNo)
        {
            string sql = string.Format("DELETE FROM SC_CHANNELUSED WHERE ORDERDATE = '{0}' AND BATCHNO = '{1}'", orderDate, batchNo);
            ExecuteNonQuery(sql);
            sql = string.Format("DELETE FROM SC_BALANCE WHERE ORDERDATE = '{0}' AND BATCHNO = '{1}'", orderDate, batchNo);
            ExecuteNonQuery(sql);
        }
    }
}
