using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DB.Util;
using Sorting.Dispatching.Dao;

namespace Sorting.Dispatching.Dal
{
    public class UploadDataDal
    {
        /// <summary>
        /// 查询上报信息
        /// </summary>
        /// <returns></returns>
        public DataTable FindSortUploadInfo()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDataDao udao = new UploadDataDao();
                return udao.FindSortUploadInfo();
            }
        }

        public DataTable FindEfficiency()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDataDao udao = new UploadDataDao();
                return udao.FindEfficiency();
            }
        }

        /// <summary>
        /// 获取上报情况表数据
        /// </summary>
        /// <param name="orderDate"></param>
        /// <returns></returns>
        public DataTable GetSortUploadData(string status)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDataDao udao = new UploadDataDao();
                return udao.GetSortUploadData(status);
            }
        }

        /// <summary>
        /// 插入上报情况表
        /// </summary>
        /// <param name="orderDate"></param>
        /// <returns></returns>
        public void GetDispatchingUploadData()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                UploadDataDao udao = new UploadDataDao();
                DataTable sotringTable = udao.GetSortUploadData();
                udao.InsertDispatchingDate(sotringTable);
            }
        }
       
        /// <summary>
        /// 上报分拣情况表数据
        /// </summary>
        /// <param name="sortTable"></param>
        public void UploadDispatchingData(DataTable sortTable)
        {
            foreach (DataRow row in sortTable.Rows)
            {
                using (PersistentManager pm = new PersistentManager("DB2Connection"))
                {
                    UploadDataDao udao = new UploadDataDao();
                    udao.SetPersistentManager(pm);
                    string sql = string.Format("INSERT INTO DWV_IORD_SORT_STATUS(SORT_BILL_ID,ORG_CODE,Dispatching_CODE,SORT_DATE,SORT_SPEC," +
                            "SORT_QUANTITY,SORT_ORDER_NUM,SORT_BEGIN_DATE,SORT_END_DATE,SORT_COST_TIME,IsImport)VALUES('{0}','{1}','{2}','{3}',{4},{5},{6},'{7}','{8}',{9},'{10}')",
                            row["SORT_BILL_ID"], row["ORG_CODE"], row["Dispatching_CODE"], row["SORT_DATE"], row["SORT_SPEC"], row["SORT_QUANTITY"], row["SORT_ORDER_NUM"],
                            row["SORT_BEGIN_DATE"], row["SORT_END_DATE"], row["SORT_COST_TIME"], row["IsImport"]);
                    udao.setDate(sql);
                }
                using (PersistentManager pm = new PersistentManager())
                {
                    UploadDataDao udao = new UploadDataDao();
                    string sql = string.Format("UPDATE DWV_IORD_SORT_STATUS SET IsImport=1 WHERE SORT_BILL_ID='{0}'", row["SORT_BILL_ID"]);
                    udao.setDate(sql);
                }
            }
        }
    }
}
