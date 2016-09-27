using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Sorting.Dispatching.Dao;
using DB.Util;

namespace Sorting.Dispatching.Dal
{
    public class RouteDal
    {
        public DataTable GetAll()
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                RouteDao routedao = new RouteDao();
                table = routedao.FindAll();
            }
            return table;
        }
        public DataTable GetAll(string filter)
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                RouteDao routedao = new RouteDao();
                table = routedao.FindAll(filter);
            }
            return table;
        }
        public DataTable GetAll(int pageIndex, int pageSize, string filter)
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                RouteDao routedao = new RouteDao();
                table = routedao.FindAll((pageIndex - 1) * pageSize, pageSize, filter);
            }
            return table;
        }


        public int GetCount(string filter)
        {
            int count = 0;
            using (PersistentManager pm = new PersistentManager())
            {
                RouteDao routedao = new RouteDao();
                count = routedao.FindCount(filter);
            }
            return count;
        }

        public void Save(string sortID,string lineCode,string routeCode,string isSort)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                RouteDao routeDao = new RouteDao();
                routeDao.UpdateEntity(sortID, lineCode, routeCode, isSort);
            }
        }
    }
}
