using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DB.Util;

namespace Sorting.Dispatching.Dao
{
    public class CustomerDao : BaseDao
    {
        public DataTable FindAll()
        {
            string sql = "SELECT * FROM V_CMD_CUSTOMER ORDER BY SORTID,ROUTECODE";
            return ExecuteQuery(sql, "V_CMD_CUSTOMER").Tables[0];
        }
        public DataTable FindAll(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            string sql = "SELECT * FROM V_CMD_CUSTOMER " + where + " ORDER BY ROUTECODE, SORTID";
            return ExecuteQuery(sql, "V_CMD_CUSTOMER").Tables[0];
        }
        public DataTable FindAll(int startRecord, int pageSize, string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            string sql = "SELECT * FROM V_CMD_CUSTOMER " + where + " ORDER BY ROUTECODE, SORTID";
            return ExecuteQuery(sql, "V_CMD_CUSTOMER", startRecord, pageSize).Tables[0];
        }


        public int FindCount(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);

            string sql = "SELECT COUNT(*) FROM V_CMD_CUSTOMER " + where;
            return (int)ExecuteScalar(sql);
        }

        public void BatchInsertCustomer(DataTable dtData)
        {
            BatchInsert(dtData, "CMD_CUSTOMER");
        }

        public void Clear()
        {
            string sql = "TRUNCATE TABLE CMD_CUSTOMER";
            ExecuteNonQuery(sql);
        }

        internal void SynchronizeCustomer(DataTable customerTable)
        {
            foreach (DataRow row in customerTable.Rows)
            {
                string sql = "IF '{0}' IN (SELECT CUSTOMERCODE FROM CMD_CUSTOMER) " +
                                " BEGIN " +
                                    " UPDATE CMD_CUSTOMER SET CUSTOMERDESC='{1}',CUSTOMERNAME = '{2}',ROUTECODE = '{3}',LICENSENO = '{4}',SORTID = {5}, " +
                                    " TELNO = '{6}' ,ADDRESS = '{7}',DEPTCODE = '{8}',DEPTNAME = '{9}',BATCHNO = '' " +
                                    " WHERE CUSTOMERCODE = '{0}' " +
                                " END " +
                             "ELSE " +
                                " BEGIN " +
                                    " INSERT CMD_CUSTOMER(CUSTOMERCODE,CUSTOMERDESC,CUSTOMERNAME,ROUTECODE,LICENSENO,SORTID,TELNO,ADDRESS,DEPTCODE,DEPTNAME,BATCHNO) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','') " +
                                " END";
                sql = string.Format(sql, row["CUSTOMER_CODE"], row["CUSTOMER_DESC"], row["CUSTOMER_DESC"], row["ROUTE_CODE"], row["SHORT_CODE"], row["SEND_SEQ"], row["TEL"], row["ADDRESS"], row["DEPT_CODE"], row["DEPT_NAME"]);
                ExecuteNonQuery(sql);
            }
        }
        internal void SynchronizeCustomer(DataTable customerTable,string batchNo)
        {
            foreach (DataRow row in customerTable.Rows)
            {
                string sql = "IF '{0}' IN (SELECT CUSTOMERCODE FROM CMD_CUSTOMER) " +
                                " BEGIN " +
                                    " UPDATE CMD_CUSTOMER SET CUSTOMERDESC='{1}',CUSTOMERNAME = '{2}',ROUTECODE = '{3}',LICENSENO = '{4}',SORTID = {5}, " +
                                    " TELNO = '{6}' ,ADDRESS = '{7}',N_CUST_CODE='{8}',DEPTCODE = '{9}',DEPTNAME = '{10}',BATCHNO = '{11}' " +
                                    " WHERE CUSTOMERCODE = '{0}' " +
                                " END " +
                             "ELSE " +
                                " BEGIN " +
                                    " INSERT CMD_CUSTOMER(CUSTOMERCODE,CUSTOMERDESC,CUSTOMERNAME,ROUTECODE,LICENSENO,SORTID,TELNO,ADDRESS,N_CUST_CODE,DEPTCODE,DEPTNAME,BATCHNO) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}') " +
                                " END";
                sql = string.Format(sql, row["CUSTOMER_CODE"], row["CUSTOMER_DESC"], row["CUSTOMER_DESC"], row["ROUTE_CODE"], row["SHORT_CODE"], row["SEND_SEQ"], row["TEL"], row["ADDRESS"], row["CUSTOMER_CODE"], row["DEPT_CODE"], row["DEPT_NAME"], batchNo);
                ExecuteNonQuery(sql);
            }
        }
    }
}
