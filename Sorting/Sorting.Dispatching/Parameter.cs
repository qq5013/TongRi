using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ParamUtil;

namespace Sorting.Dispatching
{
    public class Parameter: BaseObject
    {
        private string lineCode;
        [CategoryAttribute("系统参数"), DescriptionAttribute("本分拣线代码"), Chinese("分拣线代码")]
        public string LineCode
        {
            get { return lineCode; }
            set { lineCode = value; }
        }

        private string serverName;

        [CategoryAttribute("本机数据库连接参数"), DescriptionAttribute("数据库服务器名称"), Chinese("服务器名称")]
        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }

        private string dbName;

        [CategoryAttribute("本机数据库连接参数"), DescriptionAttribute("数据库名称"), Chinese("数据库名")]
        public string DBName
        {
            get { return dbName; }
            set { dbName = value; }
        }

        private string dbUser;

        [CategoryAttribute("本机数据库连接参数"), DescriptionAttribute("数据库连接用户名"), Chinese("用户名")]
        public string DBUser
        {
            get { return dbUser; }
            set { dbUser = value; }
        }
        private string password;

        [CategoryAttribute("本机数据库连接参数"), DescriptionAttribute("数据库连接密码"), Chinese("密码")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string stockServerName;

        [CategoryAttribute("仓储系统数据库连接参数"), DescriptionAttribute("数据库服务器名称"), Chinese("服务器名称")]
        public string StockServerName
        {
            get { return stockServerName; }
            set { stockServerName = value; }
        }

        private string stockDBName;

        [CategoryAttribute("仓储系统数据库连接参数"), DescriptionAttribute("数据库名称"), Chinese("数据库名")]
        public string StockDBName
        {
            get { return stockDBName; }
            set { stockDBName = value; }
        }

        private string stockDBUser;

        [CategoryAttribute("仓储系统数据库连接参数"), DescriptionAttribute("数据库连接用户名"), Chinese("用户名")]
        public string StockDBUser
        {
            get { return stockDBUser; }
            set { stockDBUser = value; }
        }
        private string stockPwassword;

        [CategoryAttribute("仓储系统数据库连接参数"), DescriptionAttribute("数据库连接密码"), Chinese("密码")]
        public string StockPwassword
        {
            get { return stockPwassword; }
            set { stockPwassword = value; }
        }

        private string logisticsUrl;
        [CategoryAttribute("物流系统连接参数"), DescriptionAttribute("物流系统Web Service地址"), Chinese("Url")]
        public string LogisticsUrl
        {
            get { return logisticsUrl; }
            set { logisticsUrl = value; }
        }

        private string portName;

        [CategoryAttribute("LED通信参数"), DescriptionAttribute("LED串口号"), Chinese("串口号")]
        public string PortName
        {
            get { return portName; }
            set { portName = value; }
        }

        private string ledTextLength;

        [CategoryAttribute("LED通信参数"), DescriptionAttribute("LED显示最大长度"), Chinese("最大长度")]
        public string LedTextLength
        {
            get { return ledTextLength; }
            set { ledTextLength = value; }
        }

        private string appendPara;

        [CategoryAttribute("LED通信参数"), DescriptionAttribute("LED显示方式"), Chinese("显示方式")]
        public string AppendPara
        {
            get { return appendPara; }
            set { appendPara = value; }
        }

        private string fontColor;

        [CategoryAttribute("LED通信参数"), DescriptionAttribute("LED显示颜色"), Chinese("显示颜色")]
        public string FontColor
        {
            get { return fontColor; }
            set { fontColor = value; }
        }

        private string fontColorEmpty;

        [CategoryAttribute("LED通信参数"), DescriptionAttribute("缺烟LED显示颜色"), Chinese("缺烟显示颜色")]
        public string FontColorEmpty
        {
            get { return fontColorEmpty; }
            set { fontColorEmpty = value; }
        }

        private string fontColorBreak;

        [CategoryAttribute("LED通信参数"), DescriptionAttribute("卡烟LED显示颜色"), Chinese("卡烟显示颜色")]
        public string FontColorBreak
        {
            get { return fontColorBreak; }
            set { fontColorBreak = value; }
        }

        private string speakCount;

        [CategoryAttribute("语音通信参数"), DescriptionAttribute("语音播报次数"), Chinese("播报次数")]
        public string SpeakCount
        {
            get { return speakCount; }
            set { speakCount = value; }
        }
    }
}
