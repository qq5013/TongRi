using System;
using System.Collections.Generic;
using System.Text;
using Sorting.Dispatching.Dao;
using System.Data;
using DB.Util;

namespace Sorting.Dispatching.Dal
{
    public class DialogDataDal
    {
        public DataSet GetData(string procName, StoredProcParameter param)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysSelectDialogDao dao = new SysSelectDialogDao();
                return dao.ExecuteProcedure(procName, param);
            }
        }

        public int GetRowCount(string TableView, string filter)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                SysSelectDialogDao dao = new SysSelectDialogDao();
                return dao.GetRowCount(TableView, filter);
            }
        }
    }
}
