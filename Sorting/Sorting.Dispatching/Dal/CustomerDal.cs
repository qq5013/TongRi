using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Sorting.Dispatching.Dao;
using DB.Util;

namespace Sorting.Dispatching.Dal
{
    public class CustomerDal
    {
        public DataTable GetAll()
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                CustomerDao customerDao = new CustomerDao();
                table = customerDao.FindAll();
            }
            return table;
        }
        public DataTable GetAll(string filter)
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                CustomerDao customerDao = new CustomerDao();
                table = customerDao.FindAll(filter);
            }
            return table;
        }
        public DataTable GetAll(int pageIndex, int pageSize, string filter)
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                CustomerDao customerDao = new CustomerDao();
                table = customerDao.FindAll((pageIndex - 1) * pageSize, pageSize, filter);
            }
            return table;
        }


        public int GetCount(string filter)
        {
            int count = 0;
            using (PersistentManager pm = new PersistentManager())
            {
                CustomerDao customerDao = new CustomerDao();
                count = customerDao.FindCount(filter);
            }
            return count;
        }
    }
}
