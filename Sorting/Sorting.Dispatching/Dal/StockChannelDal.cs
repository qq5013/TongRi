using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DB.Util;
using Sorting.Dispatching.Dao;

namespace Sorting.Dispatching.Dal
{
    public class StockChannelDal
    {
        public int GetCount(string filter)
        {
            int count = 0;
            using (PersistentManager pm = new PersistentManager())
            {
                StockChannelDao stockChannelDao = new StockChannelDao();
                count = stockChannelDao.FindCount(filter);
            }
            return count;
        }
        public DataTable GetAll()
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                StockChannelDao stockChannelDao = new StockChannelDao();
                table = stockChannelDao.FindAll();
            }
            return table;
        }
        public DataTable GetAll(string filter)
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                StockChannelDao stockChannelDao = new StockChannelDao();
                table = stockChannelDao.FindAll(filter);
            }
            return table;
        }
        public DataTable GetAll(int pageIndex, int pageSize, string filter)
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                StockChannelDao stockChannelDao = new StockChannelDao();
                table = stockChannelDao.FindAll((pageIndex - 1) * pageSize, pageSize, filter);
            }
            return table;
        }
        public void Save(string channelCode, string cigaretteCode, string cigaretteName, string status)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockChannelDao stockChannelDao = new StockChannelDao();
                stockChannelDao.UpdateEntity(channelCode, cigaretteCode, cigaretteName, status);
            }
        }
        public void Save(string channelCode, string cigaretteCode, string cigaretteName, string channelOrder,string status)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockChannelDao stockChannelDao = new StockChannelDao();
                stockChannelDao.UpdateEntity(channelCode, cigaretteCode, cigaretteName, channelOrder,status);
            }
        }
        public void Save(string channelCode, string cigaretteCode, string cigaretteName, int quantity, string status,string isStockIn)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockChannelDao stockChannelDao = new StockChannelDao();
                stockChannelDao.UpdateEntity(channelCode, cigaretteCode, cigaretteName, quantity, status, isStockIn);
            }
        }
    }
}
