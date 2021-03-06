using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DB.Util;

namespace Sorting.Dispatching.Dao
{
    public class StockChannelDao: BaseDao
    {
        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <returns></returns>
        public DataTable FindChannel()
        {
            string sql = "SELECT * FROM CMD_STOCKCHANNEL WHERE (CHANNELTYPE = '1' OR CHANNELTYPE ='2') AND STATUS='1' ORDER BY ORDERNO";
            return ExecuteQuery(sql).Tables[0];
        }

        public int FindCount(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);

            string sql = "SELECT COUNT(*) FROM CMD_STOCKCHANNEL " + where;
            return (int)ExecuteScalar(sql);
        }
        public DataTable FindAll()
        {
           
            string sql = "SELECT * FROM CMD_STOCKCHANNEL";
            return ExecuteQuery(sql, "CMD_CHANNEL").Tables[0];
        }
        public DataTable FindAll(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            string sql = "SELECT * FROM CMD_STOCKCHANNEL";
            return ExecuteQuery(sql, "CMD_CHANNEL").Tables[0];
        }
        public DataTable FindAll(int startRecord, int pageSize, string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            string sql = "SELECT * FROM CMD_STOCKCHANNEL " + where;
            return ExecuteQuery(sql, "CMD_CHANNEL", startRecord, pageSize).Tables[0];
        }
        public void UpdateEntity(string channelCode, string cigaretteCode, string cigaretteName, string status)
        {
            SqlCreate sqlCreate = new SqlCreate("CMD_STOCKCHANNEL", SqlType.UPDATE);
            sqlCreate.AppendQuote("CIGARETTECODE", cigaretteCode.Trim());
            sqlCreate.AppendQuote("CIGARETTENAME", cigaretteName.Trim());
            sqlCreate.AppendQuote("STATUS", status);
            sqlCreate.AppendWhereQuote("CHANNELCODE", channelCode);
            ExecuteNonQuery(sqlCreate.GetSQL());
        }
        public void UpdateEntity(string channelCode, string cigaretteCode, string cigaretteName, string channelOrder,string status)
        {
            SqlCreate sqlCreate = new SqlCreate("CMD_STOCKCHANNEL", SqlType.UPDATE);
            sqlCreate.AppendQuote("CIGARETTECODE", cigaretteCode.Trim());
            sqlCreate.AppendQuote("CIGARETTENAME", cigaretteName.Trim());
            sqlCreate.Append("ORDERNO", channelOrder);
            sqlCreate.AppendQuote("STATUS", status);
            sqlCreate.AppendWhereQuote("CHANNELCODE", channelCode);
            ExecuteNonQuery(sqlCreate.GetSQL());
        }
        public void UpdateEntity(string channelCode, string cigaretteCode, string cigaretteName, int quantity, string status, string isStockIn)
        {
            SqlCreate sqlCreate = new SqlCreate("CMD_STOCKCHANNEL", SqlType.UPDATE);
            sqlCreate.AppendQuote("CIGARETTECODE", cigaretteCode.Trim());
            sqlCreate.AppendQuote("CIGARETTENAME", cigaretteName.Trim());
            sqlCreate.AppendQuote("REMAINQUANTITY", quantity);
            sqlCreate.AppendQuote("STATUS", status);
            sqlCreate.AppendQuote("ISSTOCKIN", isStockIn);
            sqlCreate.AppendWhereQuote("CHANNELCODE", channelCode);
            ExecuteNonQuery(sqlCreate.GetSQL());
        }

        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="channelTable"></param>
        public void UpdateChannel(DataTable channelTable)
        {
            foreach (DataRow row in channelTable.Rows)
            {
                SqlCreate sqlCreate = new SqlCreate("CMD_STOCKCHANNEL", SqlType.UPDATE);
                sqlCreate.AppendQuote("PRODUCTCODE", row["PRODUCTCODE"]);
                sqlCreate.AppendQuote("PRODUCTNAME", row["PRODUCTNAME"]);
                sqlCreate.Append("QUANTITY", row["QUANTITY"]);
                sqlCreate.AppendQuote("TOCHANNELCODE", row["TOCHANNELCODE"]);  
                sqlCreate.AppendWhereQuote("CHANNELCODE", row["CHANNELCODE"]);
                ExecuteNonQuery(sqlCreate.GetSQL());
            }
        }

        /// <summary>
        /// 2010-11-21
        /// </summary>
        public void ClearCigarette()
        {
            string sql = "UPDATE CMD_STOCKCHANNEL SET ProductCode='',ProductName='',Quantity=0,ToChannelCode='' WHERE RemainQuantity = 0";
            ExecuteNonQuery(sql);
        }



        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="mixTable"></param>
        public void InsertMixChannel(DataTable mixTable)
        {
            foreach (DataRow row in mixTable.Rows)
            {
                SqlCreate sqlCreate = new SqlCreate("SC_STOCKMIXCHANNEL", SqlType.INSERT);
                sqlCreate.AppendQuote("ORDERDATE", row["ORDERDATE"]);
                sqlCreate.AppendQuote("BATCHNO", row["BATCHNO"]);
                sqlCreate.AppendQuote("CHANNELCODE", row["CHANNELCODE"]);
                sqlCreate.AppendQuote("CIGARETTECODE", row["CIGARETTECODE"]);
                sqlCreate.AppendQuote("CIGARETTENAME", row["CIGARETTENAME"]);
                ExecuteNonQuery(sqlCreate.GetSQL());
            }
        }

        internal void InsertStockChannelUsed(string orderDate, string batchNo, DataTable channelTable)
        {
            foreach (DataRow row in channelTable.Rows)
            {
                SqlCreate sqlCreate = new SqlCreate("SC_STOCKCHANNELUSED", SqlType.INSERT);
                sqlCreate.AppendQuote("ORDERDATE", orderDate);
                sqlCreate.AppendQuote("BATCHNO", batchNo);

                sqlCreate.AppendQuote("CHANNELCODE", row["CHANNELCODE"]);
                sqlCreate.AppendQuote("CHANNELNAME", row["CHANNELNAME"]);
                sqlCreate.AppendQuote("CHANNELTYPE", row["CHANNELTYPE"]);

                sqlCreate.AppendQuote("PRODUCTCODE", row["PRODUCTCODE"]);
                sqlCreate.AppendQuote("PRODUCTNAME", row["PRODUCTNAME"]);

                sqlCreate.Append("QUANTITY", row["QUANTITY"]);
                sqlCreate.Append("REMAINQUANTITY", row["REMAINQUANTITY"]);

                sqlCreate.Append("ORDERNO", row["ORDERNO"]);
                sqlCreate.Append("LEDNO", row["LEDNO"]);

                sqlCreate.AppendQuote("STATUS", row["STATUS"]);
                sqlCreate.AppendQuote("ISSTOCKIN", row["ISSTOCKIN"]);
                sqlCreate.AppendQuote("CHANNELADDRESS", row["CHANNELADDRESS"]);
                sqlCreate.AppendQuote("TOCHANNELCODE", row["TOCHANNELCODE"]);

                ExecuteNonQuery(sqlCreate.GetSQL());
            }
        }

        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        public void DeleteHistory(string orderDate)
        {
            string sql = string.Format("DELETE FROM SC_STOCKMIXCHANNEL WHERE ORDERDATE < '{0}'", orderDate);
            ExecuteNonQuery(sql);

            sql = string.Format("DELETE FROM SC_STOCKCHANNELUSED WHERE ORDERDATE < '{0}'", orderDate);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void DeleteSchedule(string batchNo)
        {
            string sql = string.Format("DELETE FROM SC_STOCKMIXCHANNEL WHERE BATCHNO='{0}'", batchNo);
            ExecuteNonQuery(sql);

            sql = string.Format("DELETE FROM SC_STOCKCHANNELUSED WHERE BATCHNO='{0}'", batchNo);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void DeleteSchedule(string orderDate, string batchNo)
        {
            string sql = string.Format("DELETE FROM SC_STOCKMIXCHANNEL WHERE ORDERDATE = '{0}' AND BATCHNO='{1}'", orderDate, batchNo);
            ExecuteNonQuery(sql);

            sql = string.Format("DELETE FROM SC_STOCKCHANNELUSED WHERE ORDERDATE = '{0}' AND BATCHNO='{1}'", orderDate, batchNo);
            ExecuteNonQuery(sql);
        }
    }
}
