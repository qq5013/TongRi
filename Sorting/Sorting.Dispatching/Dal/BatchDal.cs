using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Sorting.Dispatching.Dao;
using DB.Util;

namespace Sorting.Dispatching.Dal
{
    public class BatchDal
    {
        public void AddBatch(string BatchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                batchDao.InsertEntity(BatchNo);
            }
        }
        public void AddBatch(string orderDate, string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                batchDao.InsertEntity(orderDate, batchNo);
            }
        }
        public DataTable GetAll()
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                table = batchDao.FindAll();
            }
            return table;
        }
        public DataTable GetAll(int pageIndex, int pageSize, string filter)
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                table = batchDao.FindAll((pageIndex - 1) * pageSize, pageSize, filter);
            }
            return table;
        }

        public int GetCount(string filter)
        {
            int count = 0;
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                count = batchDao.FindCount(filter);
            }
            return count;
        }
        public DataTable GetBatch(string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                return batchDao.FindBatch(batchNo);
            }
        }
        public DataTable GetBatch(string orderDate, string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                return batchDao.FindBatch(orderDate, batchNo);
            }
        }
        public DataTable GetBatchNo()
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                table = batchDao.FindBatch();
            }
            return table;
        }
        public DataTable GetBatchNo(string orderDate)
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                table = batchDao.FindBatch(orderDate);
            }
            return table;
        }
        public DataTable FindBatchByFilter(string orderDate, string filter)
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                table = batchDao.FindBatchByFilter(orderDate, filter);
            }
            return table;
        }
        public void SaveExecuter(string user, string ip, string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                batchDao.UpdateExecuter(user, ip, batchNo);
            }
        }
        public void SaveExecuter(string user, string ip, string orderDate, string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                batchDao.UpdateExecuter(user, ip, orderDate, batchNo);
            }
        }
        public void SaveUploadUser(string user, string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                batchDao.UpdateNoOnePro(batchNo, user);
            }
        }
        public void SaveUploadUser(string user, string orderDate, string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                batchDao.UpdateNoOnePro(orderDate, batchNo, user);
            }
        }

        public void Save(string orderDate, string sortBatch, string no1Batch,string no1UpLoadState)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                batchDao.UpdateEntity(orderDate,sortBatch,no1Batch,no1UpLoadState);
            }
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ip"></param>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void UpdateExecuter(string user, string ip, string orderDate, string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                batchDao.UpdateExecuter(user, ip, orderDate, batchNo);
            }
        }
        public void UpdateDownload(string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                batchDao.UpdateDownload(batchNo);
            }
        }
        public void UpdateDownload(string batchNo, string status)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                batchDao.UpdateDownload(batchNo, status);
            }
        }
        public void UpdateNoOnePro(string batchNo, string status)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                batchDao.UpdateNoOnePro(batchNo, status);
            }
        }
        public void UpdateNoOnePro(string orderDate, string batchNo, string user)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                batchDao.UpdateNoOnePro(orderDate, batchNo, user);
            }
        }
        public void UpdateUpload(string batchNo, string status)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                batchDao.UpdateUpload(batchNo, status);
            }
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="status"></param>
        public void UpdateIsValid(string batchNo, string status)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                batchDao.UpdateIsValid(batchNo, status);
            }
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ip"></param>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void UpdateExecuter(string user, string ip, string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                batchDao.UpdateExecuter(user, ip, batchNo);
            }
        }
        /// <summary>
        /// 判断是否有正在分拣的数据
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public bool IsExistsNoSorting()
        {
            bool isExists;
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                isExists = batchDao.IsExistsNoSorting();
            }
            return isExists;
        }
        /// <summary>
        /// 判断是否有正在分拣的数据
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public bool IsExistsStartSupply()
        {
            bool isExists;
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                isExists = batchDao.IsExistsStartSupply();
            }
            return isExists;
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ip"></param>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void InsertBatchDetail(string BreakType, string Flag)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                batchDao.InsertBatchDetail(BreakType, Flag);
            }
        }
        public DataTable GetBatchDetail(string BatchNo)
        {            
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                table = batchDao.GetBatchDetail(BatchNo);
            }
            return table;
        }
        internal DataTable GetBreakTotal(string BatchNo)
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                table = batchDao.GetBreakTotal(BatchNo);
            }
            return table;
        }
        /// <summary>
        /// 获取上一个优化批次
        /// </summary>
        /// <param name="BatchNo"></param>
        /// <returns></returns>
        public string GetPreBatchNo(string BatchNo)
        {
            string preBatchNo = "";
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                preBatchNo = batchDao.GetPreBatchNo(BatchNo);
            }
            return preBatchNo;
        }
        public void DeleteBatchData(string BatchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                batchDao.DeleteBatchData(BatchNo);
            }
        }
        /// <summary>
        /// 清理小于当天的所有数据
        /// </summary>
        /// <param name="BatchNo"></param>
        public void ClearBatchData()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                batchDao.ClearBatchData();
            }
        }
    }
}
