using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DB.Util;

namespace Sorting.Dispatching.Dao
{
   
    public class LineScheduleDao : BaseDao
    {
        public DataTable FindAll(int startRecord, int pageSize, string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            string sql = "SELECT A.*, B.ROUTENAME FROM SC_LINE A LEFT JOIN CMD_ROUTE B ON A.ROUTECODE = B.ROUTECODE " + where;
            return ExecuteQuery(sql, "SC_LINE", startRecord, pageSize).Tables[0];
        }

        public int FindCount(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);

            string sql = "SELECT COUNT(*) FROM SC_LINE " + where;
            return (int)ExecuteScalar(sql);
        }

        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="lineTable"></param>
        public void SaveLineSchedule(DataTable lineTable)
        {
            foreach (DataRow lineRow in lineTable.Rows)
            {
                SqlCreate sqlCreate = new SqlCreate("SC_LINE", SqlType.INSERT);
                sqlCreate.AppendQuote("LINECODE", lineRow["LINECODE"]);
                sqlCreate.AppendQuote("ROUTECODE", lineRow["ROUTECODE"]);
                sqlCreate.Append("QUANTITY", lineRow["QUANTITY"]);
                sqlCreate.AppendQuote("BATCHNO", lineRow["BATCHNO"]);
                sqlCreate.AppendQuote("ORDERDATE", lineRow["ORDERDATE"]);
                string sql = sqlCreate.GetSQL();
                ExecuteNonQuery(sql);
            }
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataSet FindAllLine(string batchNo)
        {
            string sql = "SELECT DISTINCT A.LINECODE FROM SC_LINE A" +
                            " LEFT JOIN CMD_LINEINFO B ON A.LINECODE = B.LINECODE" +
                            " WHERE A.BATCHNO = '{0}' AND B.LINETYPE = '2'";
            return ExecuteQuery(string.Format(sql, batchNo));
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataSet FindAllLine(string orderDate, string batchNo)
        {
            string sql = "SELECT DISTINCT A.LINECODE FROM SC_LINE A"+
                            " LEFT JOIN CMD_LINEINFO B ON A.LINECODE = B.LINECODE" +
                            " WHERE A.ORDERDATE = '{0}' AND A.BATCHNO = '{1}' AND B.LINETYPE = '2'";
            return ExecuteQuery(string.Format(sql, orderDate, batchNo));
        }

        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        public void DeleteHistory(string orderDate)
        {
            string sql = string.Format("DELETE FROM SC_LINE WHERE ORDERDATE < '{0}'", orderDate);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void DeleteSchedule(string batchNo)
        {
            string sql = string.Format("DELETE FROM SC_LINE WHERE BATCHNO='{0}'", batchNo);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void DeleteSchedule(string orderDate, string batchNo)
        {
            string sql = string.Format("DELETE FROM SC_LINE WHERE ORDERDATE = '{0}' AND BATCHNO='{1}'", orderDate, batchNo);
            ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <returns></returns>
        public string FindRoutes(string orderDate)
        {
            string result = null;
            string sql = string.Format("SELECT ROUTECODE FROM SC_LINE WHERE ORDERDATE = '{0}'", orderDate);
            DataTable routeTable = ExecuteQuery(sql).Tables[0];
            if (routeTable.Rows.Count == 0)
                result = "''";
            else
                for (int i = 0; i < routeTable.Rows.Count; i++)
                {
                    if (i == routeTable.Rows.Count - 1)
                        result += string.Format("'{0}'", routeTable.Rows[i]["ROUTECODE"]);
                    else
                        result += string.Format("'{0}',", routeTable.Rows[i]["ROUTECODE"]);
                }
            return result;
        }
    }
}
