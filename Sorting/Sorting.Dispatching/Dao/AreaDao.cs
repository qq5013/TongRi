using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DB.Util;

namespace Sorting.Dispatching.Dao
{
    public class AreaDao : BaseDao
    {
        /// <summary>
        /// 分布查询满足条件的所有记录
        /// </summary>
        /// <param name="startRecord"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataTable FindAll()
        {
            string sql = "SELECT * FROM CMD_AREA ";
            return ExecuteQuery(sql, "CMD_AREA").Tables[0];
        }
        /// <summary>
        /// 分布查询满足条件的所有记录
        /// </summary>
        /// <param name="startRecord"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataTable FindAll(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            string sql = "SELECT * FROM CMD_AREA " + where;
            return ExecuteQuery(sql, "CMD_AREA").Tables[0];
        }


        /// <summary>
        /// 查询满足条件记录条数
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int FindCount(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);

            string sql = "SELECT COUNT(*) FROM CMD_AREA " + where;
            return (int)ExecuteScalar(sql);
        }

        

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dataSet"></param>
        public void UpdateEntity(string sortID, string areaCode)
        {
            SqlCreate sqlCreate = new SqlCreate("CMD_AREA", SqlType.UPDATE);
            sqlCreate.Append("SORTID", sortID);
            sqlCreate.AppendWhereQuote("AREACODE", areaCode);
            ExecuteNonQuery(sqlCreate.GetSQL());
        }

        /// <summary>
        /// 批次插入
        /// </summary>
        /// <param name="dtData"></param>
        /// <param name="tableName"></param>
        public void BatchInsertArea(DataTable dtData)
        {
            BatchInsert(dtData, "CMD_AREA");
        }

        public void Clear()
        {
            string sql = "TRUNCATE TABLE CMD_AREA";
            ExecuteNonQuery(sql);
        }

        internal void SynchronizeArea(DataTable areaTable)
        {
            foreach (DataRow row in areaTable.Rows)
            {
                string sql = "IF '{0}' IN (SELECT AREACODE FROM CMD_AREA) " +
                                "BEGIN " +
                                    "UPDATE CMD_AREA SET AREANAME = '{1}' WHERE AREACODE = '{0}' " +
                                "END " +
                             "ELSE " +
                                "BEGIN " +
                                    "INSERT CMD_AREA VALUES ('{0}','{1}','{2}') " +
                                "END";
                sql = string.Format(sql, row["AREACODE"], row["AREANAME"], row["SORTID"]);
                ExecuteNonQuery(sql);
            }
        }
    }
}
