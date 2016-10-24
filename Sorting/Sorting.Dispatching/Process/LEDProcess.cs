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
    public class LEDProcess : AbstractProcess
    {
        [Serializable]
        public class RestartState
        {
            public bool IsRestart = false;
        }

        private RestartState restartState = new RestartState();
        private LED2015 led2015 = null;

        private Dictionary<int, string> isActiveLeds = new Dictionary<int, string>();

        Dictionary<int, int> emptyAddress2 = new Dictionary<int, int>();
        Dictionary<int, int> emptyAddress3 = new Dictionary<int, int>();
        Dictionary<int, int> breakAddress2 = new Dictionary<int, int>();
        Dictionary<int, int> breakAddress3 = new Dictionary<int, int>();

        short FontColor = 1;
        short FontColorEmpty = 2;
        short FontColorBreak = 3;
        int SpeakCount = 5;

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
                SpeakCount = int.Parse(context.Attributes["SpeakCount"].ToString());

                //判断串口通讯是否正常
                System.IO.Ports.SerialPort com = new System.IO.Ports.SerialPort("COM" + ComNo.ToString());
                com.Open();
                
                if(com.IsOpen)
                    com.Close();


                restartState = Util.SerializableUtil.Deserialize<RestartState>(true, @".\RestartState.sl");

                led2015 = new LED2015(ComNo, LedTextLength, AppendPara, FontColor);
            }
            catch (Exception e)
            {
                //Logger.Error(string.Format("LEDProcess 初始化失败！原因：{0}！", e.Message));
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
                    case "NewData"://由优化按钮事件发出  
                        //写重新开始分拣标志
                        WriteRestartDataToPLC();

                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("LED 更新操作失败！原因：{0}！ {1}", e.Message, "LEDProcess.cs 行号：224！"));
            }
        }        

        private void WriteRestartDataToPLC()
        {
            try
            {
                WriteToService("SortPLC", "RestartSign", 1);
                int[] channelStatus = new int[100];
                ChannelDao dao = new ChannelDao();
                DataTable dt = dao.FindAllChannel();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    channelStatus[i] = int.Parse(dt.Rows[i]["Status"].ToString());
                }
                WriteToService("SortPLC", "StartSign", channelStatus);
                //WriteToService("SortPLC", "MergeFinish", 0);
                //WriteToService("SortPLC", "RestartData", 1);
            }
            catch (Exception e)
            {
                Logger.Error("写重新分拣标志操作失败！原因：" + e.Message);
            }
        }
    }
}
