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
        [CategoryAttribute("ϵͳ����"), DescriptionAttribute("���ּ��ߴ���"), Chinese("�ּ��ߴ���")]
        public string LineCode
        {
            get { return lineCode; }
            set { lineCode = value; }
        }

        private string serverName;

        [CategoryAttribute("�������ݿ����Ӳ���"), DescriptionAttribute("���ݿ����������"), Chinese("����������")]
        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }

        private string dbName;

        [CategoryAttribute("�������ݿ����Ӳ���"), DescriptionAttribute("���ݿ�����"), Chinese("���ݿ���")]
        public string DBName
        {
            get { return dbName; }
            set { dbName = value; }
        }

        private string dbUser;

        [CategoryAttribute("�������ݿ����Ӳ���"), DescriptionAttribute("���ݿ������û���"), Chinese("�û���")]
        public string DBUser
        {
            get { return dbUser; }
            set { dbUser = value; }
        }
        private string password;

        [CategoryAttribute("�������ݿ����Ӳ���"), DescriptionAttribute("���ݿ���������"), Chinese("����")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string stockServerName;

        [CategoryAttribute("�ִ�ϵͳ���ݿ����Ӳ���"), DescriptionAttribute("���ݿ����������"), Chinese("����������")]
        public string StockServerName
        {
            get { return stockServerName; }
            set { stockServerName = value; }
        }

        private string stockDBName;

        [CategoryAttribute("�ִ�ϵͳ���ݿ����Ӳ���"), DescriptionAttribute("���ݿ�����"), Chinese("���ݿ���")]
        public string StockDBName
        {
            get { return stockDBName; }
            set { stockDBName = value; }
        }

        private string stockDBUser;

        [CategoryAttribute("�ִ�ϵͳ���ݿ����Ӳ���"), DescriptionAttribute("���ݿ������û���"), Chinese("�û���")]
        public string StockDBUser
        {
            get { return stockDBUser; }
            set { stockDBUser = value; }
        }
        private string stockPwassword;

        [CategoryAttribute("�ִ�ϵͳ���ݿ����Ӳ���"), DescriptionAttribute("���ݿ���������"), Chinese("����")]
        public string StockPwassword
        {
            get { return stockPwassword; }
            set { stockPwassword = value; }
        }

        private string logisticsUrl;
        [CategoryAttribute("����ϵͳ���Ӳ���"), DescriptionAttribute("����ϵͳWeb Service��ַ"), Chinese("Url")]
        public string LogisticsUrl
        {
            get { return logisticsUrl; }
            set { logisticsUrl = value; }
        }

        private string portName;

        [CategoryAttribute("LEDͨ�Ų���"), DescriptionAttribute("LED���ں�"), Chinese("���ں�")]
        public string PortName
        {
            get { return portName; }
            set { portName = value; }
        }

        private string ledTextLength;

        [CategoryAttribute("LEDͨ�Ų���"), DescriptionAttribute("LED��ʾ��󳤶�"), Chinese("��󳤶�")]
        public string LedTextLength
        {
            get { return ledTextLength; }
            set { ledTextLength = value; }
        }

        private string appendPara;

        [CategoryAttribute("LEDͨ�Ų���"), DescriptionAttribute("LED��ʾ��ʽ"), Chinese("��ʾ��ʽ")]
        public string AppendPara
        {
            get { return appendPara; }
            set { appendPara = value; }
        }

        private string fontColor;

        [CategoryAttribute("LEDͨ�Ų���"), DescriptionAttribute("LED��ʾ��ɫ"), Chinese("��ʾ��ɫ")]
        public string FontColor
        {
            get { return fontColor; }
            set { fontColor = value; }
        }

        private string fontColorEmpty;

        [CategoryAttribute("LEDͨ�Ų���"), DescriptionAttribute("ȱ��LED��ʾ��ɫ"), Chinese("ȱ����ʾ��ɫ")]
        public string FontColorEmpty
        {
            get { return fontColorEmpty; }
            set { fontColorEmpty = value; }
        }

        private string fontColorBreak;

        [CategoryAttribute("LEDͨ�Ų���"), DescriptionAttribute("����LED��ʾ��ɫ"), Chinese("������ʾ��ɫ")]
        public string FontColorBreak
        {
            get { return fontColorBreak; }
            set { fontColorBreak = value; }
        }

        private string speakCount;

        [CategoryAttribute("����ͨ�Ų���"), DescriptionAttribute("������������"), Chinese("��������")]
        public string SpeakCount
        {
            get { return speakCount; }
            set { speakCount = value; }
        }
    }
}
