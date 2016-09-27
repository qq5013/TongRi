using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DB.Util;

namespace Sorting.Dispatching.Dao
{
    public class ServerDao: BaseDao
    {
        public DataTable FindBatch(string lineCode)
        {
            string sql = string.Format("SELECT TOP 1 BATCHID,ORDERDATE,BATCHNO FROM CMD_BATCH WHERE ISUPTONOONEPRO='1' AND " +
                "BATCHID NOT IN (SELECT BATCHID FROM CMD_BATCHSTATUS WHERE LINECODE='{0}') ORDER BY ORDERDATE,BATCHNO", lineCode);

            sql = string.Format("SELECT TOP 1 BATCHID,ORDERDATE,BATCHNO FROM CMD_BATCH WHERE " +
                "BATCHID NOT IN (SELECT BATCHID FROM CMD_BATCHSTATUS WHERE LINECODE='{0}') ORDER BY ORDERDATE,BATCHNO", lineCode);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindChannel(string orderDate, string batchNo, string lineCode)
        {
            string sql = string.Format("SELECT A.* FROM SC_CHANNELUSED A LEFT JOIN CMD_CHANNEL B ON A.CHANNELID = B.CHANNELID WHERE ORDERDATE='{0}' AND BATCHNO={1} AND A.LINECODE='{2}'", orderDate, batchNo, lineCode);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindOrderMaster(string orderDate, string batchNo, string lineCode)
        {
            string sql = string.Format("SELECT * FROM SC_PALLETMASTER WHERE ORDERDATE='{0}' AND BATCHNO={1} AND LINECODE='{2}' AND STATUS = '0'", orderDate, batchNo, lineCode);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindOrderDetail(string orderDate, string batchNo, string lineCode)
        {
            string sql = string.Format("SELECT * FROM SC_PALLETMASTER WHERE ORDERDATE='{0}' AND BATCHNO={1} AND LINECODE='{2}'", orderDate, batchNo, lineCode);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindOrder(string orderDate, string batchNo, string lineCode)
        {
            string sql = string.Format("SELECT * FROM SC_ORDER WHERE ORDERDATE='{0}' AND BATCHNO={1} AND LINECODE='{2}'", orderDate, batchNo, lineCode);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindHandleSupply(string orderDate, string batchNo, string lineCode)
        {
            string sql = string.Format("SELECT * FROM SC_HANDLESUPPLY WHERE ORDERDATE='{0}' AND BATCHNO={1} AND LINECODE='{2}'", orderDate, batchNo, lineCode);
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        ///  Ö£Ð¡Áú 20110904 Ìí¼Ó
        /// </summary>
        /// <param name="orderDate"></param>
        /// <returns></returns>
        public DataTable FindOrderDateInfo(string orderDate)
        {
            string sql = string.Format("SELECT ORDERDATE,ORGCODE FROM SC_I_ORDERMASTER WHERE ORDERDATE='{0}' GROUP BY ORDERDATE,ORGCODE", orderDate);
            return ExecuteQuery(sql).Tables[0];
        }

        public void UpdateOrderStatus(string sortNo,string channelGroup)
        {
            string sql = "";
            if(channelGroup.Equals("A"))
                 sql = string.Format("UPDATE SC_PALLETMASTER SET STATUS = '1' WHERE SORTNO <= {0}", sortNo);
            else
                 sql = string.Format("UPDATE SC_PALLETMASTER SET STATUS1 = '1' WHERE SORTNO <= {0}", sortNo);
            ExecuteNonQuery(sql);
        }

        public void UpdateBatchStatus(string batchID, string lineCode)
        {
            SqlCreate sqlCreate = new SqlCreate("CMD_BATCHSTATUS", SqlType.INSERT);
            sqlCreate.Append("BATCHID", batchID);
            sqlCreate.AppendQuote("LINECODE", lineCode);
            ExecuteNonQuery(sqlCreate.GetSQL());
        }
    }
}
