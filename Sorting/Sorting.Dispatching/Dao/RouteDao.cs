using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DB.Util;

namespace Sorting.Dispatching.Dao
{
    public class RouteDao : BaseDao
    {
        public DataTable FindAll()
        {            
            string sql = "SELECT * FROM V_CMD_ROUTE ORDER BY AREACODE,ROUTECODE";
            return ExecuteQuery(sql, "V_CMD_ROUTE").Tables[0];
        }
        public DataTable FindAll(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            string sql = "SELECT * FROM V_CMD_ROUTE " + where + " ORDER BY AREACODE,ROUTECODE";
            return ExecuteQuery(sql, "V_CMD_ROUTE").Tables[0];
        }
        public DataTable FindAll(int startRecord, int pageSize, string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            string sql = "SELECT * FROM V_CMD_ROUTE " + where + " ORDER BY AREACODE,ROUTECODE";
            return ExecuteQuery(sql, "V_CMD_ROUTE", startRecord, pageSize).Tables[0];
        }


        public int FindCount(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);

            string sql = "SELECT COUNT(*) FROM V_CMD_ROUTE " + where;
            return (int)ExecuteScalar(sql);
        }

        public void BatchInsertRoute(DataTable dtData)
        {
            BatchInsert(dtData, "CMD_ROUTE");
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dataSet"></param>
        public void UpdateEntity(string sortID,string lineCode,string routeCode,string isSort)
        {
            SqlCreate sqlCreate = new SqlCreate("CMD_ROUTE", SqlType.UPDATE);
            sqlCreate.Append("SORTID", sortID);
            sqlCreate.AppendQuote("LINECODE", lineCode);
            sqlCreate.AppendQuote("ISSORT", isSort);
            sqlCreate.AppendWhereQuote("ROUTECODE", routeCode);
            
            ExecuteNonQuery(sqlCreate.GetSQL());
        }

        public void Clear()
        {
            string sql = "TRUNCATE TABLE CMD_ROUTE";
            ExecuteNonQuery(sql);
        }

        internal void SynchronizeRoute(DataTable routeTable)
        {
            foreach (DataRow row in routeTable.Rows)
            {
                string sql = "IF '{0}' IN (SELECT ROUTECODE FROM CMD_ROUTE) " +
                                "BEGIN " +
                                    "UPDATE CMD_ROUTE SET ROUTENAME = '{1}',VEHICLESIGN = '{2}',VEHICLENAME='{3}',VEHICLETYPE='{4}',DLVMANCODE='{5}',DLVMANNAME='{6}',DELIVERY='{7}',BATCHNO='{8}',DELIVERYDATE='{9}' WHERE ROUTECODE = '{0}' " +
                                "END " +
                             "ELSE " +
                                "BEGIN " +
                                    "INSERT CMD_ROUTE(ROUTECODE, ROUTENAME, VEHICLESIGN, VEHICLENAME, VEHICLETYPE, DLVMANCODE, DLVMANNAME, DELIVERY, LINECODE, BATCHNO,DELIVERYDATE,ISSORT) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','3','{8}','{9}','1') " +
                                "END";
                sql = string.Format(sql, row["ROUTE_CODE"], row["ROUTE_NAME"], row["VEHICLE_SIGN"], row["VEHICLE_NAME"], row["VEHICLE_TYPE"], row["DLVMAN_CODE"], row["DLVMAN_NAME"], row["DLVMAN_NAME"], row["BATCH_NO"], row["DELIVERY_DATE"]);
                ExecuteNonQuery(sql);
            }
        }
        internal void UpdateRouteSortID(string batchNo)
        {
            //更新线路顺序
            string sql = @"UPDATE CMD_ROUTE SET SORTID =TMP.SORTID 
                           FROM CMD_ROUTE INNER JOIN 
                            (SELECT BATCHNO,ROUTECODE,ISNULL(MAX(SORTID),0)+
                            (SELECT ISNULL(SUM(1),0) FROM CMD_ROUTE R1 WHERE ROUTECODE<=R2.ROUTECODE AND BATCHNO=R2.BATCHNO) SORTID 
                            FROM CMD_ROUTE R2 GROUP BY BATCHNO,ROUTECODE) TMP
                            ON CMD_ROUTE.BATCHNO=TMP.BATCHNO AND CMD_ROUTE.ROUTECODE=TMP.ROUTECODE
                            WHERE CMD_ROUTE.SORTID IS NULL AND CMD_ROUTE.BATCHNO='{0}' ";
            sql = string.Format(sql, batchNo);
            ExecuteNonQuery(sql);

            //更新是否分拣
            sql = string.Format("UPDATE CMD_ROUTE SET ISSORT ='1' WHERE ISSORT IS NULL AND BATCHNO='{0}'" , batchNo);
            ExecuteNonQuery(sql);
        }
    }
}
