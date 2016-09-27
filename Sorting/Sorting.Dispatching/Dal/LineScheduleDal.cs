using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DB.Util;
using Sorting.Dispatching.Dao;

namespace Sorting.Dispatching.Dal
{
    public class LineScheduleDal
    {
        public DataTable GetAll(int pageIndex, int pageSize, string filter)
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                LineScheduleDao lsDao = new LineScheduleDao();
                table = lsDao.FindAll((pageIndex - 1) * pageSize, pageSize, filter);
            }
            return table;
        }


        public int GetCount(string filter)
        {
            int count = 0;
            using (PersistentManager pm = new PersistentManager())
            {
                LineScheduleDao lsDao = new LineScheduleDao();
                count = lsDao.FindCount(filter);
            }
            return count;
        }
    }
}
