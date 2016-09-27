using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Sorting.Dispatching.Dao;
using DB.Util;

namespace Sorting.Dispatching.Dal
{
    public class HandleSortOrderDal
    {
        public int GetCount(string filter)
        {
            int count = 0;
            using (PersistentManager pm = new PersistentManager())
            {
                HandleSortOrderDao handleSortOrderDao = new HandleSortOrderDao();
                count = handleSortOrderDao.FindCount(filter);
            }
            return count;
        }

        public DataTable GetAll(int pageIndex, int PagingSize, string filter)
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                HandleSortOrderDao handleSortOrderDao = new HandleSortOrderDao();
                table = handleSortOrderDao.FindAll((pageIndex - 1) * PagingSize, PagingSize, filter);
            }
            return table;
        }

        public void Insert(string orderDate, string orderId)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                HandleSortOrderDao handleSortOrderDao = new HandleSortOrderDao();
                handleSortOrderDao.InsertEntity(orderDate, orderId);
            }
        }
        public void Insert(string orderDate, string batchNo,string orderId)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                HandleSortOrderDao handleSortOrderDao = new HandleSortOrderDao();
                handleSortOrderDao.InsertEntity(orderDate, batchNo,orderId);
            }
        }
        internal void Delete(string batchNo, string orderId)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                HandleSortOrderDao handleSortOrderDao = new HandleSortOrderDao();
                handleSortOrderDao.DeleteEntity(batchNo, orderId);
            }
        }
        public void Save(string orderDate, string oldOrderId, string newOrderId)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                HandleSortOrderDao handleSortOrderDao = new HandleSortOrderDao();
                handleSortOrderDao.UpdateEntity(orderDate, oldOrderId, newOrderId);
            }
        }
       
        /// <summary>
        /// ����������ѯ��������¼�ڷּ𶩵�ϸ������� 2011-12-11 wu
        /// </summary>
        /// <param name="orderId">������</param>
        /// <param name="orderDate">��������</param>
        /// <param name="batchNo">���κ�</param>
        /// <param name="cigaretteCode">���̴���</param>
        public int GetQuantityByValue(string orderId, string orderDate, string batchNo, string cigaretteCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                HandleSortOrderDao handleSortOrderDao = new HandleSortOrderDao();
                return handleSortOrderDao.GetQuantityByValue(orderId, orderDate, batchNo, cigaretteCode);
            }
        }

        /// <summary>
        ///  ���������޸ĸļ�¼�ķּ����� 2011-12-11 wu
        /// </summary>
        /// <param name="quantity">ʵ�ʷּ�����</param>
        /// <param name="orderId">������</param>
        /// <param name="orderDate">��������</param>
        /// <param name="batchNo">���κ�</param>
        /// <param name="cigaretteCode">���̴���</param>
        public void updateSortQuantity(int quantity, string orderId, string orderDate, string batchNo, string cigaretteCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                HandleSortOrderDao handleSortOrderDao = new HandleSortOrderDao();
                handleSortOrderDao.UpdateSortQuantity(quantity, orderId, orderDate, batchNo, cigaretteCode);
            }
        }
    }
}
