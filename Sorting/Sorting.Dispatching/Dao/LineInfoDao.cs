using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DB.Util;

namespace Sorting.Dispatching.Dao
{
    public class LineInfoDao : BaseDao
    {
        public DataSet GetAll()
        {
            string sql = "SELECT AREA_CODE, LINECODE SORTLINE_CODE, LINENAME SORTLINE_NAME, LINETYPE SORTLINE_TYPE, ABILITY, ProductTotal, PackCapacity, STATUS FROM CMD_LINEINFO ";
            return ExecuteQuery(sql, "CMD_LINEINFO");
        }
        public DataTable FindAll()
        {            
            string sql = "SELECT * FROM CMD_LINEINFO ";
            return ExecuteQuery(sql, "CMD_LINEINFO").Tables[0];
        }
        public DataTable FindAll(int startRecord, int pageSize, string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            string sql = "SELECT * FROM CMD_LINEINFO " + where;
            return ExecuteQuery(sql, "CMD_LINEINFO", startRecord, pageSize).Tables[0];
        }

        public int FindCount(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);

            string sql = "SELECT COUNT(*) FROM CMD_LINEINFO " + where;
            return (int)ExecuteScalar(sql);
        }

        public void UpdateEntity(string lineCode, string status)
        {
            SqlCreate sqlCreate = new SqlCreate("CMD_LINEINFO", SqlType.UPDATE);
            sqlCreate.AppendQuote("STATUS", status);
            sqlCreate.AppendWhereQuote("LINECODE", lineCode);
            ExecuteNonQuery(sqlCreate.GetSQL());
        }

        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <returns></returns>
        public DataSet GetAvailabeLine(string lineType)
        {
            string sql = "SELECT * FROM CMD_LINEINFO WHERE LINETYPE = '{0}' AND STATUS = '1'";
            return ExecuteQuery(string.Format(sql,lineType));
        }
    }
}
