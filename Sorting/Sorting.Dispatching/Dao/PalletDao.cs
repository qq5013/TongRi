using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DB.Util;

namespace Sorting.Dispatching.Dao
{
    public class PalletDao : BaseDao
    {
        public void SavePalletMaster(DataTable masterTable)
        {
            BatchInsert(masterTable, "SC_PALLETMASTER");
        }

        public void UpdatePalletMaster(DataTable masterTable)
        {
            string sql = "UPDATE SC_PALLETMASTER SET PQUANTITY = {0}, QUANTITY = {1} WHERE ORDERDATE = '{2}' AND SORTNO = {3} AND BATCHNO={4} AND LINECODE={5}";
            foreach (DataRow masterRow in masterTable.Rows)
            {
                string s = string.Format(sql, masterRow["PQUANTITY"], masterRow["QUANTITY"], masterRow["ORDERDATE"], masterRow["SORTNO"], masterRow["BATCHNO"], masterRow["LINECODE"]);
                ExecuteNonQuery(s);
            }
        }

        public void SavePalletDetail(DataTable detailTable)
        {
            //BatchInsert(detailTable, "AS_SC_PALLETDETAIL");
        }

        public DataSet FindPalletMaster(string orderDate, string batchNo, string lineCode)
        {
            string sql = "SELECT * FROM SC_PALLETMASTER WHERE LINECODE = '{0}' AND ORDERDATE = '{1}' AND BATCHNO = '{2}' ORDER BY SORTNO";
            return ExecuteQuery(string.Format(sql, lineCode, orderDate, batchNo));
        }
    }
}
