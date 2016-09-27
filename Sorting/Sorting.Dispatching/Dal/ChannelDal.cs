using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Sorting.Dispatching.Dao;
using DB.Util;

namespace Sorting.Dispatching.Dal
{
    public class ChannelDal
    {
        public DataTable GetAll()
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                table = channelDao.FindAll();
            }
            return table;
        }
        public DataTable GetAll(string filter)
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                table = channelDao.FindAll(filter);
            }
            return table;
        }
        public DataTable GetAll(int pageIndex, int pageSize, string filter)
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                table = channelDao.FindAll((pageIndex - 1) * pageSize, pageSize, filter);
            }
            return table;
        }


        public int GetCount(string filter)
        {
            int count = 0;
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                count = channelDao.FindCount(filter);
            }
            return count;
        }
        public void Save(string channelCode, string cigaretteCode, string cigaretteName, string status)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                channelDao.UpdateEntity(channelCode, cigaretteCode, cigaretteName, status);
            }
        }
        public void Save(string channelCode, string productCode, string productName, string channelOrder, string status)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                channelDao.UpdateEntity(channelCode, productCode, productName, channelOrder, status);
            }
        }
        public DataTable GetChannel()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindChannel();
            }
        }
        public DataSet GetChannels()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindChannels();
            }
        }
        public DataTable GetChannel(string sortNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindChannelQuantity(sortNo);
            }
        }
        public DataTable GetChannel(string sortNo, string status)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindChannelQuantity(sortNo,status);
            }
        }
        public DataTable GetChannelBalance()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindChannelBalance();
            }
        }
        public DataTable FindAllChannelBalance()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindAllChannelBalance();
            }
        }
        public DataTable GetEmptyChannel(string channelCode,string channelType, int channelGroup)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindEmptyChannel(channelCode,channelType, channelGroup);
            }
        }
        
        public DataTable GetChannelCode(string channelcode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindChannelCode(channelcode);
            }
        }

        public DataTable GetChannelQuantity(string status)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                string sortNo = orderDao.FindLastSortNo(status);

                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindChannelQuantity(sortNo,status);
            }
        }

        public bool ExechangeChannel(string batchNo,string sourceChannel, string targetChannel, out int sourceChannelAddress, out int targetChannelAddress)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                OrderDao orderDao = new OrderDao();
                SupplyDao supplyDao = new SupplyDao();
                try
                {
                    pm.BeginTransaction();
                    DataTable sourceChannelTable = channelDao.FindChannel(batchNo,sourceChannel);//获取欲交换的烟道
                    DataTable targetChannelTable = channelDao.FindChannel(batchNo,targetChannel);//获取要交换的目的烟道

                    sourceChannelAddress = Convert.ToInt32(sourceChannelTable.Rows[0]["CHANNELADDRESS"]);
                    targetChannelAddress = Convert.ToInt32(targetChannelTable.Rows[0]["CHANNELADDRESS"]);

                    channelDao.UpdateChannel(batchNo,targetChannel,
                        sourceChannelTable.Rows[0]["CIGARETTECODE"].ToString(),
                        sourceChannelTable.Rows[0]["CIGARETTENAME"].ToString(), 
                        Convert.ToInt32(sourceChannelTable.Rows[0]["QUANTITY"]),
                        Convert.ToInt32(sourceChannelTable.Rows[0]["GROUPNO"]),
                        sourceChannelTable.Rows[0]["SORTNO"].ToString());

                    channelDao.UpdateChannel(batchNo,sourceChannel,
                        targetChannelTable.Rows[0]["CIGARETTECODE"].ToString(),
                        targetChannelTable.Rows[0]["CIGARETTENAME"].ToString(),
                        Convert.ToInt32(targetChannelTable.Rows[0]["QUANTITY"]),
                        Convert.ToInt32(sourceChannelTable.Rows[0]["GROUPNO"]),
                        targetChannelTable.Rows[0]["SORTNO"].ToString());

                    orderDao.UpdateChannel(batchNo,sourceChannel, "0000");
                    orderDao.UpdateChannel(batchNo,targetChannel, sourceChannel);
                    orderDao.UpdateChannel(batchNo,"0000", targetChannel);
                    //尾数表烟道交换
                    orderDao.UpdateChannelBalance(batchNo, sourceChannel, "0000", "", 0);
                    orderDao.UpdateChannelBalance(batchNo, targetChannel, sourceChannel, sourceChannelTable.Rows[0]["CHANNELNAME"].ToString(), Convert.ToInt32(sourceChannelTable.Rows[0]["CHANNELORDER"]));
                    orderDao.UpdateChannelBalance(batchNo, "0000", targetChannel, targetChannelTable.Rows[0]["CHANNELNAME"].ToString(), Convert.ToInt32(targetChannelTable.Rows[0]["CHANNELORDER"]));

                    //补货目标烟道也要交换
                    supplyDao.UpdateChannel(batchNo,sourceChannel, "0000");
                    supplyDao.UpdateChannel(batchNo,targetChannel, sourceChannel);
                    supplyDao.UpdateChannel(batchNo,"0000", targetChannel);
                    //补货目标地址变更
                    supplyDao.UpdateStockChannelUsed(batchNo, sourceChannel, "0000");
                    supplyDao.UpdateStockChannelUsed(batchNo, targetChannel, sourceChannel);
                    supplyDao.UpdateStockChannelUsed(batchNo, "0000", targetChannel);
                    //补货目标地址变更
                    supplyDao.UpdateStockChannel(batchNo, sourceChannel, "0000");
                    supplyDao.UpdateStockChannel(batchNo, targetChannel, sourceChannel);
                    supplyDao.UpdateStockChannel(batchNo, "0000", targetChannel);
                    pm.Commit();
                    return true;
                }
                catch
                {                    
                    pm.Rollback();
                    sourceChannelAddress = 0;
                    targetChannelAddress = 0;
                    return false;
                }
            }
        }
        public void ClearChannelBalance(int bNo, string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                channelDao.ClearChannelBalance(bNo, batchNo);
            }
        }
    }
}
