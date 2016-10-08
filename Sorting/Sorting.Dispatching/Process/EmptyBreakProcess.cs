using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MCP;
using DB.Util;
using Sorting.Dispatching.Dao;
using Sorting.Dispatching.Util;

namespace Sorting.Dispatching.Process
{
    public class EmptyBreakProcess : AbstractProcess
    {
        private LED2015 led2015 = null;

        private Dictionary<int, string> isActiveLeds = new Dictionary<int, string>();
        
        //Dictionary<int,int> emptyAddress2 =  new Dictionary<int,int>();
        //Dictionary<int, int> emptyAddress3 = new Dictionary<int, int>();
        //Dictionary<int, int> breakAddress2 = new Dictionary<int, int>();
        //Dictionary<int, int> breakAddress3 = new Dictionary<int, int>();

        int emptyAddress2 = 0;
        int emptyAddress3 = 0;
        int breakAddress2 = 0;
        int breakAddress3 = 0;
        short FontColor = 1;
        short FontColorEmpty = 2;
        short FontColorBreak = 3;        

        public override void Initialize(Context context)
        {
            try
            {
                base.Initialize(context);

                //Microsoft.VisualBasic.Devices.Network network = new Microsoft.VisualBasic.Devices.Network();
                short ComNo = short.Parse(context.Attributes["LedCOM"].ToString());
                short LedTextLength = short.Parse(context.Attributes["LedTextLength"].ToString());
                short AppendPara = short.Parse(context.Attributes["AppendPara"].ToString());
                FontColor = short.Parse(context.Attributes["FontColor"].ToString());
                FontColorEmpty = short.Parse(context.Attributes["FontColorEmpty"].ToString());
                FontColorBreak = short.Parse(context.Attributes["FontColorBreak"].ToString());

                //Microsoft.VisualBasic.Devices.Computer pc = new Microsoft.VisualBasic.Devices.Computer();
                //foreach (string s in pc.Ports.SerialPortNames)
                //{
                //    this.comboBox1.Items.Add(s);
                //}

                //�жϴ���ͨѶ�Ƿ�����
                System.IO.Ports.SerialPort com = new System.IO.Ports.SerialPort("COM" + ComNo.ToString());
                com.Open();
                
                if(com.IsOpen)
                    com.Close();

                led2015 = new LED2015(ComNo, LedTextLength, AppendPara, FontColor);
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("LEDProcess ��ʼ��ʧ�ܣ�ԭ��{0}��", e.Message));
            }
        }

        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                object o = ObjectUtil.GetObject(stateItem.State);
                //if (o == null)
                //    return;
                //Logger.Info(stateItem.ItemName + "[" + o.ToString() + "]");

                switch (stateItem.ItemName)
                {
                    case "NewData"://���Ż���ť�¼�����  
                        // д�ղֲ�����ˮ��
                        //WriteChannelDataToPLC();
                        //д���¿�ʼ�ּ��־
                        WriteRestartDataToPLC();

                        //���������ݱ�־��
                        
                        //LED��ʾ�̵����ݺ;���Ʒ��
                        Show("3", false);
                        Show("2", false);

                        break;
                    case "Check"://���̵㰴ť�¼�����
                        //if (!restartState.IsRestart)
                        //{
                        //    object statea = Context.Services["SortPLC"].Read("Check3");
                        //    if (statea is Array)
                        //    {
                        //        Array array = (Array)statea;
                        //        if (array.Length == 15)
                        //        {
                        //            //LED��ʾ�̵����ݺ;���Ʒ��
                        //            int[] quantity = new int[15];
                        //            array.CopyTo(quantity, 0);
                        //            Show("3", true, quantity);
                        //        }
                        //    }
                        //    object stateb = Context.Services["SortPLC"].Read("Check2");
                        //    if (stateb is Array)
                        //    {
                        //        Array array = (Array)stateb;
                        //        if (array.Length == 70)
                        //        {
                        //            //LED��ʾ�̵����ݺ;���Ʒ��
                        //            int[] quantity = new int[70];
                        //            array.CopyTo(quantity, 0);
                        //            Show("2", true, quantity);
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    Show("3", true);
                        //    Show("2", true);
                        //}

                        break;
                    case "ChannelCode":
                        //ȱ�̱���

                        string channelCode = stateItem.State.ToString();
                        Show(channelCode);
                        break;
                    case "Start"://�ɿ�ʼ��ť�¼�����
                        

                        ////��ʱ�����������ּ𶩵������̣߳�����װ���������̡߳�                        
                        if (Context.Processes["OrderRequestProcess"] != null)
                        {
                            Context.Processes["OrderRequestProcess"].Resume();
                        }

                        if (Context.Processes["MergeRequestProcess"] != null)
                        {
                            Context.Processes["MergeRequestProcess"].Resume();
                        }

                        if (Context.Processes["PackRequestProcess"] != null)
                        {
                            Context.Processes["PackRequestProcess"].Resume();
                        }

                        //LED����ʾ�̵����ݣ�ֻ��ʾ����Ʒ��
                        Show("3", false);
                        Show("2", false);

                        //System.Threading.Thread.Sleep(2000);
                        //Context.ProcessDispatcher.WriteToProcess("OrderRequestProcess", "OrderRequest3", 0);
                        //Context.ProcessDispatcher.WriteToProcess("OrderRequestProcess", "OrderRequest2", 0);
                        break;
                    case "Empty3":
                        //ȱ�̱���

                        object oa = ObjectUtil.GetObject(stateItem.State);
                        int errAddress3 = Convert.ToInt32(oa);
                        //if (!emptyAddress3.ContainsKey(errAddress3))
                        //    emptyAddress3.Add(errAddress3, errAddress3);

                        if (errAddress3 == 0)
                            BreakShow("0", "3", emptyAddress3);
                        else if (errAddress3 > 0)
                        {
                            emptyAddress3 = errAddress3;
                            BreakShow("1", "3", emptyAddress3);
                        }

                        break;
                    case "Empty2":
                        //ȱ�̱���

                        object ob = ObjectUtil.GetObject(stateItem.State);
                        int errAddress2 = Convert.ToInt32(ob);

                        if (errAddress2 == 0)
                            BreakShow("0", "2", emptyAddress2);
                        else if (errAddress2 > 0)
                        {
                            emptyAddress2 = errAddress2;
                            BreakShow("1", "2", emptyAddress2);
                        }

                        break;
                    case "BreakDown3":
                        //ͨ�������̱���

                        object bd3 = ObjectUtil.GetObject(stateItem.State);
                        int bdAddress3 = Convert.ToInt32(bd3);

                        if (bdAddress3 == 0)
                            BreakShow("0", "3", emptyAddress3);
                        else if (bdAddress3 > 0)
                        {
                            breakAddress3 = bdAddress3;
                            BreakShow("2", "3", breakAddress3);
                        }

                        break;
                    case "BreakDown2":
                        //��ʽ�����̱���

                        object bd2 = ObjectUtil.GetObject(stateItem.State);
                        int bdAddress2 = Convert.ToInt32(bd2);

                        if (bdAddress2 == 0)
                            BreakShow("0", "2", breakAddress2);
                        else if (bdAddress2 > 0)
                        {
                            breakAddress2 = bdAddress2;
                            BreakShow("2", "2", breakAddress2);
                        }

                        break;

                }
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("LED ���²���ʧ�ܣ�ԭ��{0}�� {1}", e.Message, "LEDProcess.cs �кţ�224��"));
            }
        }
        private void BreakShow(string ShowType, string channelType,int channelAddress)
        {
            try
            {
                if (led2015 == null)
                {
                    Logger.Info("LED����ͨѶ�쳣�����鴮������");
                    return;
                }

                using (PersistentManager pm = new PersistentManager())
                {
                    OrderDao orderDao = new OrderDao();
                    ChannelDao channelDao = new ChannelDao();

                    string sortNo = "";
                    DataTable channelTable = null;

                    sortNo = orderDao.FindMaxSortedMaster(channelType);
                    channelTable = channelDao.FindChannelCode(channelType,channelAddress);
                    short fontColor = FontColor;
                    string YYmessage = "";
                    Dictionary<short, string> dic = new Dictionary<short, string>();
                    foreach (DataRow row in channelTable.Rows)
                    {
                        string text = "";
                        string channelName = row["CHANNELNAME"].ToString().Trim();
                        string cigaretteName = row["CIGARETTENAME"].ToString().Trim();

                        cigaretteName = cigaretteName.Replace("(", StringUtil.ToSBC(""));
                        cigaretteName = cigaretteName.Replace(")", StringUtil.ToSBC(""));
                        cigaretteName = cigaretteName.Replace("��", StringUtil.ToSBC(""));
                        cigaretteName = cigaretteName.Replace("��", StringUtil.ToSBC(""));
                        cigaretteName = cigaretteName.Replace(" ", "");
                        cigaretteName = cigaretteName.Replace("  ", "");

                        short LedNo = short.Parse(row["LEDNO"].ToString());
                        string info = "";
                        
                        if (ShowType == "1")
                        {
                            info = "ȱ";
                            YYmessage = LedNo + info + cigaretteName;
                            fontColor = FontColorEmpty;

                        }
                        else if (ShowType == "2")
                        {
                            info = "��";
                            YYmessage = LedNo + info + cigaretteName;
                            fontColor = FontColorBreak;
                        }

                        if (cigaretteName.Length == 0)
                            text = string.Format("{0} {1} {2}", "", "  ��  ", "");
                        else
                            text = string.Format("{0} {1}", info, cigaretteName);

                        if (!dic.ContainsKey(LedNo))
                            dic.Add(LedNo, text);
                    }
                    if (dic.Count > 0)
                        led2015.Show(dic, 1, 1, fontColor);

                    if (YYmessage.Length > 0)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            SpeakerUtil.SpeakerInfo(YYmessage);
                            System.Threading.Thread.Sleep(2000);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("LED SHOW ʧ�ܣ�ԭ��{0}��", e.Message));
            }
        }
        private void Show(string channelType, bool checkMode)
        {
            try
            {
                if (led2015 == null)
                {
                    Logger.Info("LED����ͨѶ�쳣�����鴮������");
                    return;
                }
                using (PersistentManager pm = new PersistentManager())
                {
                    OrderDao orderDao = new OrderDao();
                    ChannelDao channelDao = new ChannelDao();

                    string sortNo = "";
                    DataTable channelTable = null;

                    sortNo = orderDao.FindMaxSortedMaster(channelType);
                    channelTable = channelDao.FindChannelQuantity(sortNo, channelType);

                    Dictionary<short, string> dic = new Dictionary<short, string>();
                    foreach (DataRow row in channelTable.Rows)
                    {
                        string text = "";
                        string channelName = row["CHANNELNAME"].ToString().Trim();
                        string cigaretteName = row["CIGARETTENAME"].ToString().Trim();
                        string qty = StringUtil.ToSBC(Convert.ToString((Convert.ToInt32(row["REMAINQUANTITY"]) % 50)).PadRight(2, " "[0]));

                        cigaretteName = cigaretteName.Replace("(", StringUtil.ToSBC(""));
                        cigaretteName = cigaretteName.Replace(")", StringUtil.ToSBC(""));
                        cigaretteName = cigaretteName.Replace("��", StringUtil.ToSBC(""));
                        cigaretteName = cigaretteName.Replace("��", StringUtil.ToSBC(""));
                        cigaretteName = cigaretteName.Replace(" ", "");
                        cigaretteName = cigaretteName.Replace("  ", "");

                        short LedNo = short.Parse(row["LEDNO"].ToString());

                        
                        if (cigaretteName.Length == 0)
                            text = string.Format("{0} {1} {2}", "", "  ��  ", "");
                        else
                            text = string.Format("{0}{1}", "", cigaretteName);

                        if (!dic.ContainsKey(LedNo))
                            dic.Add(LedNo, text);
                    }
                    if (dic.Count > 0)
                        led2015.Show(dic, 1, 1, FontColor);
                }
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("LED SHOW ����ʧ�ܣ�ԭ��{0}�� {1}", e.Message, "LEDProcess.cs �кţ�258��"));
            }
        }
        private void Show(string channelCode)
        {
            try
            {
                if (led2015 == null)
                {
                    Logger.Info("LED����ͨѶ�쳣�����鴮������");
                    return;
                }
                using (PersistentManager pm = new PersistentManager())
                {
                    ChannelDao channelDao = new ChannelDao();

                    DataTable channelTable = channelDao.FindChannelCode(channelCode);

                    Dictionary<short, string> dic = new Dictionary<short, string>();
                    foreach (DataRow row in channelTable.Rows)
                    {
                        string text = "";
                        string channelName = row["CHANNELNAME"].ToString().Trim();
                        string cigaretteName = row["CIGARETTENAME"].ToString().Trim();

                        cigaretteName = cigaretteName.Replace("(", StringUtil.ToSBC(""));
                        cigaretteName = cigaretteName.Replace(")", StringUtil.ToSBC(""));
                        cigaretteName = cigaretteName.Replace("��", StringUtil.ToSBC(""));
                        cigaretteName = cigaretteName.Replace("��", StringUtil.ToSBC(""));
                        cigaretteName = cigaretteName.Replace(" ", "");
                        cigaretteName = cigaretteName.Replace("  ", "");

                        short LedNo = short.Parse(row["LEDNO"].ToString());

                        if (cigaretteName.Length == 0)
                            text = string.Format("{0} {1} {2}", "", "  ��  ", "");
                        else
                            text = string.Format("{0}{1}", "", cigaretteName);

                        if (!dic.ContainsKey(LedNo))
                            dic.Add(LedNo, text);
                    }
                    if (dic.Count > 0)
                        led2015.Show(dic, 1, 1, FontColor);
                }
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("LED SHOW ����ʧ�ܣ�ԭ��{0}�� {1}", e.Message, "LEDProcess.cs �кţ�258��"));
            }
        }
        private void Show(string channelType, bool checkMode, params int[] quantity)
        {
            try
            {
                using (PersistentManager pm = new PersistentManager())
                {
                    OrderDao orderDao = new OrderDao();
                    ChannelDao channelDao = new ChannelDao();

                    string sortNo = "";
                    DataTable channelTable = null;

                    sortNo = orderDao.FindMaxSortedMaster(channelType);
                    channelTable = channelDao.FindChannelQuantity(sortNo, channelType);
                    Dictionary<short, string> dic = new Dictionary<short, string>();
                    foreach (DataRow row in channelTable.Rows)
                    {
                        string text = "";
                        string channelName = row["CHANNELNAME"].ToString().Trim();
                        string cigaretteName = row["CIGARETTENAME"].ToString().Trim();
                        string qty = StringUtil.ToSBC(Convert.ToString((Convert.ToInt32(row["REMAINQUANTITY"]) % 50)).PadRight(2, " "[0]));

                        cigaretteName = cigaretteName.Replace("(", StringUtil.ToSBC(""));
                        cigaretteName = cigaretteName.Replace(")", StringUtil.ToSBC(""));
                        cigaretteName = cigaretteName.Replace("��", StringUtil.ToSBC(""));
                        cigaretteName = cigaretteName.Replace("��", StringUtil.ToSBC(""));
                        cigaretteName = cigaretteName.Replace(" ", "");
                        cigaretteName = cigaretteName.Replace("  ", "");

                        short LedNo = short.Parse(row["LEDNO"].ToString());

                        if (cigaretteName.Length == 0)
                        {
                            text = string.Format("{0} {1} {2}", "", "  ��  ", "");

                        }
                        else
                        {
                            if (checkMode && Convert.ToInt32((Convert.ToInt32(row["REMAINQUANTITY"]) % 50)) >= 0)
                            {
                                text = string.Format("{0}{1}", quantity, cigaretteName);
                            }
                            else
                            {
                                text = string.Format("{0}{1}", "", cigaretteName);
                            }
                        }
                        if (!dic.ContainsKey(LedNo))
                            dic.Add(LedNo, text);
                        
                    }
                    if (dic.Count > 0)
                        led2015.Show(dic, 1, 1);
                }
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("LED SHOW ����ʧ�ܣ�ԭ��{0}�� {1}", e.Message, "LEDProcess.cs �кţ�258��"));
            }
        }

        private void WriteChannelDataToPLC()
        {
            try
            {
                using (PersistentManager pm = new PersistentManager())
                {
                    ChannelDao channelDao = new ChannelDao();
                    DataTable channelTableA = channelDao.FindLastSortNo(1);//��ȡA�߻���
                    //DataTable channeltableB = channelDao.FindLastSortNo(2);//��ȡB�߻���

                    int[] channelDataA = new int[27];
                    //int[] channelDataB = new int[42];

                    for (int i = 0; i < channelTableA.Rows.Count; i++)
                    {
                        channelDataA[Convert.ToInt32(channelTableA.Rows[i]["CHANNELADDRESS"]) - 1] = Convert.ToInt32(channelTableA.Rows[i]["SORTNO"]);
                    }

                    //for (int i = 0; i < channeltableB.Rows.Count; i++)
                    //{
                    //    channelDataB[Convert.ToInt32(channeltableB.Rows[i]["CHANNELADDRESS"]) - 1] = Convert.ToInt32(channeltableB.Rows[i]["SORTNO"]);
                    //}

                    WriteToService("SortPLC", "ChannelDataA", channelDataA);
                    //WriteToService("SortPLC", "ChannelDataB", channelDataB);
                }
            }
            catch (Exception e)
            {
                Logger.Error("д�ղֲ���ʧ�ܣ�ԭ��" + e.Message);
            }
        }

        private void WriteRestartDataToPLC()
        {
            try
            {
                WriteToService("SortPLC", "RestartData", 1);
            }
            catch (Exception e)
            {
                Logger.Error("д���·ּ��־����ʧ�ܣ�ԭ��" + e.Message);
            }
        }
    }
}
