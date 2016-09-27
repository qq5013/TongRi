using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DB.Util;

namespace Sorting.Dispatching.Dao
{
    public class SysParameterDao : BaseDao
    {
        public Dictionary<string, string> FindParameters()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            DataTable table = ExecuteQuery("SELECT * FROM SC_SYS_PARAMETER").Tables[0];
            foreach (DataRow row in table.Rows)
            {
                d.Add(row["PARAMETERNAME"].ToString(), row["PARAMETERVALUE"].ToString());
            }
            return d;
        }

        public void UpdateEntity(Dictionary<string, string> parameters)
        {
            foreach (string key in parameters.Keys)
            {
                SqlCreate sqlCreate = new SqlCreate("SC_SYS_PARAMETER", SqlType.UPDATE);
                sqlCreate.AppendQuote("PARAMETERVALUE", parameters[key]);
                sqlCreate.AppendWhereQuote("PARAMETERNAME", key);
                ExecuteNonQuery(sqlCreate.GetSQL());
            }
        }

        /// <summary>
        /// 2011-11-21 wu
        /// </summary>
        /// <param name="parameterValue"></param>
        /// <param name="parameterName"></param>
        public void UpdateParameter(string parameterValue, string parameterName)
        {
            string sql = string.Format("UPDATE SC_SYS_PARAMETER SET PARAMETERVALUE ='{0}' WHERE PARAMETERNAME='{1}'", parameterValue, parameterName);
            ExecuteNonQuery(sql);
        }
    }
}
