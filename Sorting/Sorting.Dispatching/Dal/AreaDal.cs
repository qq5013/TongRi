using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DB.Util;
using Sorting.Dispatching.Dao;

namespace Sorting.Dispatching.Dal
{
    public class AreaDal
    {
        public DataTable GetAll()
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                AreaDao areaDao = new AreaDao();
                table = areaDao.FindAll();
            }
            return table;
        }
        public DataTable GetAll(string filter)
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                AreaDao areaDao = new AreaDao();
                table = areaDao.FindAll(filter);
            }
            return table;
        }


        public int GetCount(string filter)
        {
            int count = 0;
            using (PersistentManager pm = new PersistentManager())
            {
                AreaDao areaDao = new AreaDao();
                count = areaDao.FindCount(filter);
            }
            return count;
        }

        public void Save(string sortID, string areaCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                AreaDao areaDao = new AreaDao();
                areaDao.UpdateEntity(sortID, areaCode);
            }
        }

        public void Insert(DataTable areaTable)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                AreaDao areaDao = new AreaDao();
                areaDao.BatchInsertArea(areaTable);
            }
        }
    }
}
