using System;
using System.Collections.Generic;
using System.Text;
using DB.Util;
using System.Data;

namespace Sorting.Dispatching.Dao
{
    public class HandleSortOrderDao : BaseDao
    {
        internal int FindCount(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);

            string sql = "SELECT COUNT(*) FROM SC_HANDLE_SORT_ORDER " + where;
            return (int)ExecuteScalar(sql);
        }

        internal DataTable FindAll(int startRecord, int pageSize, string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);

            string sql = "SELECT * FROM SC_HANDLE_SORT_ORDER " + where;
            return ExecuteQuery(sql, "SC_HANDLE_SORT_ORDER", startRecord, pageSize).Tables[0];
        }

        internal void InsertEntity(string orderDate, string orderId)
        {
            SqlCreate sqlCreate = new SqlCreate("SC_HANDLE_SORT_ORDER", SqlType.INSERT);
            sqlCreate.AppendQuote("ORDERDATE", orderDate);
            sqlCreate.AppendQuote("ORDERID", orderId);
            ExecuteNonQuery(sqlCreate.GetSQL());
        }
        internal void InsertEntity(string orderDate, string batchNo,string orderId)
        {
            SqlCreate sqlCreate = new SqlCreate("SC_HANDLE_SORT_ORDER", SqlType.INSERT);
            sqlCreate.AppendQuote("ORDERDATE", orderDate);
            sqlCreate.AppendQuote("BATCHNO", batchNo);
            sqlCreate.AppendQuote("ORDERID", orderId);
            ExecuteNonQuery(sqlCreate.GetSQL());
        }
        internal void DeleteEntity(string batchNo, string orderId)
        {
            string sql = "DELETE SC_HANDLE_SORT_ORDER WHERE BATCHNO = '{0}' AND ORDERID = '{1}'";
            ExecuteNonQuery(string.Format(sql, batchNo, orderId));
        }
        internal void UpdateEntity(string orderDate, string oldOrderId, string newOrderId)
        {
            string sql = "UPDATE SC_HANDLE_SORT_ORDER SET ORDERID = '{0}' WHERE ORDERDATE = '{1}' AND ORDERID = '{2}'";
            ExecuteNonQuery(string.Format(sql,newOrderId,orderDate,oldOrderId));
        }        

        /// <summary>
        /// ����������ѯ��������¼�ڷּ𶩵�ϸ������� 2011-12-11 wu
        /// </summary>
        /// <param name="orderId">������</param>
        /// <param name="orderDate">��������</param>
        /// <param name="batchNo">���κ�</param>
        /// <param name="cigaretteCode">���̴���</param>
        internal int GetQuantityByValue(string orderId, string orderDate, string batchNo, string cigaretteCode)
        {
            string sql = "SELECT ORDER_QUANTITY FROM SC_I_ORDERDETAIL WHERE ORDERID='{0}' AND ORDERDATE='{1}' AND BATCHNO='{2}'AND CIGARETTECODE='{3}'";
            return (int)ExecuteScalar(string.Format(sql, orderId, orderDate, batchNo, cigaretteCode));
        }

        /// <summary>
        /// ���������޸ĸļ�¼�ķּ����� 2011-12-11 wu
        /// </summary>
        /// <param name="quantity">ʵ�ʷּ�����</param>
        /// <param name="orderId">������</param>
        /// <param name="orderDate">��������</param>
        /// <param name="batchNo">���κ�</param>
        /// <param name="cigaretteCode">���̴���</param>
        internal void UpdateSortQuantity(int quantity, string orderId, string orderDate, string batchNo, string cigaretteCode)
        {
            string sql = "UPDATE SC_I_ORDERDETAIL SET QUANTITY={0} WHERE ORDERID='{1}' AND ORDERDATE='{2}' AND BATCHNO='{3}'AND CIGARETTECODE='{4}'";
            ExecuteNonQuery(string.Format(sql, quantity, orderId, orderDate, batchNo, cigaretteCode));
        }
    }
}
