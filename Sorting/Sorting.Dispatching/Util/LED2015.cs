using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AxSEND232OCXLib;


namespace Sorting.Dispatching.Util
{
    public class LED2015
    {
        private AxSEND232OCXLib.AxSend232OCX m_active;
        private short ComNo = 1;
        private short TextLength = 5;
        private object locker = new object();
        private short AppendPara = 64;
        private short FontColor = 1;
        public LED2015()
        {
            m_active = new AxSEND232OCXLib.AxSend232OCX();
            m_active.CreateControl();            
        }
        public LED2015(short ComNo, short TextLength, short appendPara, short fontColor)
        {
            m_active = new AxSEND232OCXLib.AxSend232OCX();
            m_active.CreateControl();
            this.ComNo = ComNo;
            this.TextLength = TextLength;
            this.AppendPara = appendPara;
            this.FontColor = fontColor;

            this.m_active.SetProtocolType(0);
            this.m_active.SetSendType(0);
            this.m_active.SetTiaoPingInfo(this.TextLength, this.ComNo, 2, 2);
            this.m_active.SetHasReturn(1);
            this.m_active.SetTiaoPingMoveInfo(1, 1, 5, this.AppendPara, 0, 0, this.FontColor);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PingHao"></param>
        /// <param name="IsFirst"></param>
        /// <param name="IsLast"></param>
        /// <param name="ShowText"></param>
        /// <returns></returns>
        public short Show(short PingHao, short IsFirst, short IsLast, string ShowText)
        {
            lock (locker)
            {
                string showtext = ShowText;
                short len = (short)ShowText.Length;
                if (len > 5)
                {
                    len = 5;
                    showtext = ShowText.Substring(0, 5);
                }                
                this.m_active.StartComm();
                short s = this.m_active.SendContent1(PingHao, IsFirst, IsLast, showtext, len);
                this.m_active.StopComm();
                return s;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PingHao"></param>
        /// <param name="IsFirst"></param>
        /// <param name="IsLast"></param>
        /// <param name="ShowText"></param>
        /// <returns></returns>
        public short Show(short PingHao, short IsFirst, short IsLast, string ShowText,short FontColor)
        {
            lock (locker)
            {
                string showtext = ShowText;
                short len = (short)ShowText.Length;
                if (len > 5)
                {
                    len = 5;
                    showtext = ShowText.Substring(0, 5);
                }
                this.m_active.SetTiaoPingMoveInfo(1, 1, 5, this.AppendPara, 0, 0, FontColor);
                this.m_active.StartComm();
                short s = this.m_active.SendContent1(PingHao, IsFirst, IsLast, showtext, len);
                this.m_active.StopComm();
                return s;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PingHao"></param>
        /// <param name="IsFirst"></param>
        /// <param name="IsLast"></param>
        /// <param name="ShowText"></param>
        /// <returns></returns>
        public short Show(Dictionary<short,string> dic, short IsFirst, short IsLast)
        {
            lock (locker)
            {
                string message = "";
                short comResult = this.m_active.StartComm();
                if (comResult == 0)
                {
                    message = "COM" + this.ComNo + "串口打开失败，请确认串口通讯是否正常";
                    Exception ex = new Exception(message);
                    throw ex;
                }
                short st = 0;
                foreach (KeyValuePair<short, string> d in dic)
                {
                    string showtext = d.Value;
                    short len = (short)d.Value.Length;
                    if (len > 5)
                    {
                        len = 5;
                        showtext = showtext.Substring(0, 5);
                    }
                    short s = this.m_active.SendContent1(d.Key, IsFirst, IsLast, showtext, len);
                    if (s == 1)
                        st = s;
                    else
                    {
                        if (s == -1)
                            message = "地址回传错误";
                        else if (s == -2)
                            message = "校验和错";
                        else if (s == -3)
                            message = "串口没打开";
                        else if (s == -4)
                            message = "LAN发送数据错误";
                        else if (s == -5)
                            message = "LAN不能使用协议V1.0";

                        comResult = this.m_active.StopComm();
                        
                        Exception ex = new Exception(message);
                        throw ex;
                    }

                }
                comResult = this.m_active.StopComm();
                return st;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PingHao"></param>
        /// <param name="IsFirst"></param>
        /// <param name="IsLast"></param>
        /// <param name="ShowText"></param>
        /// <returns></returns>
        public short Show(Dictionary<short, string> dic, short IsFirst, short IsLast,short FontColor)
        {
            lock (locker)
            {
                string message = "";
                //1红色 2绿色 3黄色
                this.m_active.SetTiaoPingMoveInfo(1, 1, 5, this.AppendPara, 0, 0, FontColor);
                short comResult = this.m_active.StartComm();
                if (comResult == 0)
                {
                    message = "COM" + this.ComNo + "串口打开失败，请确认串口通讯是否正常";
                    Exception ex = new Exception(message);
                    throw ex;
                }
                short st = 0;
                foreach (KeyValuePair<short, string> d in dic)
                {
                    string showtext = d.Value;
                    short len = (short)d.Value.Length;
                    if (len > 5)
                    {
                        len = 5;
                        showtext = showtext.Substring(0, 5);
                    }
                    short s = this.m_active.SendContent1(d.Key, IsFirst, IsLast, showtext, len);
                    if (s == 1)
                        st = s;
                    else
                    {
                        if (s == -1)
                            message = "地址回传错误";
                        else if (s == -2)
                            message = "校验和错";
                        else if (s == -3)
                            message = "串口没打开";
                        else if (s == -4)
                            message = "LAN发送数据错误";
                        else if (s == -5)
                            message = "LAN不能使用协议V1.0";

                        comResult = this.m_active.StopComm();
                        Exception ex = new Exception(message);
                        throw ex;
                    }

                }
                comResult = this.m_active.StopComm();
                return st;
            }
        }
    }
}

//        二)本控件提供10个方法:
//A)short SetProtocolType(  short iProtocolType );
//设置通讯时使用的通讯协议.
//iProtocolType=0:为老版本协议, 需要在发送屏号和数据的时候设置奇偶校验位
//iProtocolType=1:为适应使用TCP/IP或Modem通讯方式，去掉了奇偶校验位的设置，加入了帧头，因此本ocxV2.3以前的版本只能使用老版本的协议
//        V2.3(包括V2.3)以后的版本两种协议都可以适用。

//当条屏上电时如果显示"Prot. V2.0"的字样，则用户必须设置iProtocolType=1。 如果条屏上电时显示"Prot. V1.0"或不显示"Prot."的字样，则用户必须设置iProtocolType=0。如果用户不调用本函数则本OCX默认为使用老版本的通讯协议（iProtocolType=0）。如果您不清楚如何设置，请咨询厂家。

//B)short SetSendType( short iSendType );
//设置通讯方式
//iSendType=0:为使用Rs232通讯
//iSendType=1:为使用TCP/IP通讯。 当使用TCP/IP通讯时必须使用新的协议(在A函数中设置通讯协议为1)

//如果使用TCP/IP通讯，必须调用函数C来设置对方的IP 地址和IP端口

//C)short SetLANSendInfo( LPCTSTR pstrIP, long lIPPort, long lTimeOutForConnect );
//pstrIP为对方的IP地址, 比如192.168.1.123
//lIPPort为连接的端口, 程序默认为5005
//lTimeOutForConnect:为连接时的超时错时间, 单位为秒, 程序默认为5秒

//如果为使用RS232通讯，则可不调用本函数

//关于如何设置Netjet Box控制盒的IP Address盒IP Port及它和屏通讯的波特率请参考NetJetSetting Manual.doc

//D)short SetTiaoPingInfo(short TiaoPingZiShuPara, short ComportPara, short IsSingleColorPara, short BaudPara) 
//设置条屏信息和发送参数信息
//    1)TiaoPingZiShuPara:条屏字数,如果用户为10字条屏则该参数等于10
//    2)ComportPara:发送时使用的端口,等于1为COM1,等于2为COM2
//    3)IsSingleColor:等于1则为单色屏,等于2为双色屏
//    4)BaudPara:发送时使用的波特率 1:2400bps,2:9600bps 用户一般取值为1,除非用户有特殊要求。当条屏上电的时候将显示条屏的屏号和使用的波特率，此处设置的值必须和条屏显示的使用的波特率一致

//E)short SetHasReturn(short HasReturnPara) 
//参数1)HasReturnPara:等于1:表示在发送过程中将检测条屏的回传以确保发送是否已经成功
//           等于0:表示发送过程中将不检测条屏的回传而默认发送成功
//如果用户不调用该方法则OCX将不会检测条屏的回传值
//当发送检查回传值时将提高发送的可靠性
//至于用户调用该函数时是应该设置该值为1还是0，必须查看条屏下端的版本，用户如果不清楚请和厂家联系。

//F)short CSend232OCXCtrl::StartComm() 
//返回值: 对于rs232 : 失败返回0, 成功返回端口号(1,2,...)
//           : 对于LAN : 返回-1 => IP为空, 0:连接失败, 1:连接成功, TCP/IP连接的超时时间默认为5秒

//该函数使用SetTiaoPingInfo中指定的端口来初始化串口,因此该函数必须在SetTiaoPingInfo函数调用后才能调用

//G)short CSend232OCXCtrl::StopComm() 
//关闭串口.return 1:原来没打开,2:原来打开现成功关闭,

//H)short SetTiaoPingMoveInfo(short YinRuPara, short YinChuPara, short SpeedPara, short AppendPara, short CartoonPara, short MuJianDelayPara, short FontTypePara) 
//设置条屏内容显示方式
//    1)YinRuPara:为该幕的引入方式,值为0-15,相应的方式参见附录.
//    2)YinChuPara:为该幕的引出方式,值为0-15,相应的方式参见附录.
//    3)SpeedPara:该幕移动时的移动速度,值为0-7,0最快
//    4)AppendPara:附加方式,参见附录

//        动画   停止   分割    时间   连续   暂停     保留   闪烁
//         D7     D6     D5      D4     D3     D2       D1     D0
//        (高位)						  (低位)

//    相应的位为1表示相应的附加方式有效,可一次指定多位有效
//        D7:动画,当该位=1时第六个参数才有效
//        D6:停止
//        D5:分割
//        D4:时间
//        D3:连续
//        D2:暂停,当该位=1时第七个参数才有效
//        D1:保留未用
//        D0:闪烁
//    5)CartoonPara:动画方式,值为0-6共七个动画,参数Cartoon指定该幕的动画方式，该值只有当AppendPara指定为动画方式时才有效
//    6)MuJianDelayPara:该幕在屏上停留的时间,值为0-19秒.该值只有在Append指定为暂停方式是才有效
//    7)FontTypePara:单色条屏 1:细体正常，2：细体反白，3：粗体正常，4：粗体反白
//           双色条屏 1：红色，2：绿色，3：黄色

//I)short AdjustTime(short PingHao)
//校准条屏时间
//    1)PingHao:屏号,为0则广播发送,即所有连网的屏都接收,如果要某一条
//        屏接收则指定相应的屏的屏号.
//返回 -1:地址回传错误, -2:检验和错, -3:串口没打开, -4:LAN发送数据错误, -5:LAN不能使用协议V1.0, 1:发送正确

//J)short SendContent1(short PingHao, short IsFirst, short IsLast, LPCTSTR Buf, short BufLen) 
//发送内容到条屏上去
//    1)PingHao:屏号,为0则广播发送,即所有连网的屏都接收,如果要某一条
//        屏接收则指定相应的屏的屏号.
//    2)IsFirst:如果为第一幕则等于1,否则其他的幕等于0.(一定要正确设置该参数,否则显示将不正常)
//    3)IsLast:如果发送最后一幕则等于1,否则等于0.(一定要正确设置该参数,否则条屏将不能正常结束)
//        如果用户发送只有一幕内容则IsFirst=1,IsLast=1;
//    4)Buf:用户发送的字符串
//    5)BufLen:用户发送的字符串的字符个数

//返回 -1:地址回传错误, -2:检验和错, -3:串口没打开, -4:LAN发送数据错误, -5:LAN不能使用协议V1.0, 1:发送正确

//设置发送时是否接收条屏的回传值
//三)使用过程:
//    1)SetProtocolType(0);//设置使用哪种通讯协议, 0:老协议, 1:新协议
//    2)SetSendType( 0);//设置使用哪种通讯方式, 0:RS232, 1:TCP/IP
//    3)SetLANSendInfo( "192.168.0.1", 5005, 5 );//当使用TCP/IP通讯时调用本函数设置连接的IP地址和端口

//    4)用户在使用本控件时必须先调用SetTiaoPingInfo方法指定条屏的长度,发送的端口,波特率及是单色屏还是双色屏
//    SetTiaoPingInfo(10,1,1,1);//为10字单色条屏,发送时使用2400的波特率通过COM1发送

//    5)调用方法SetHasReturn来设置发送时是否检查回传值(如果用户不清楚该方法如何设置，请联系厂家)

//    6)调用方法StartComm来初始化串口

//    7)如果用户要校准1号屏的时间:
//        AdjustTime(1);
//        如果校准所有条屏的时间则:AdjustTime(0);

//    如果用户要发送三幕内容到一号条屏上(假设内容在Buf1,Buf2,Buf3中,长度为Buf1Len,Buf2Len,Buf3Len):
//        Buf1=new char[Buf1Len+1];
//        Buf2=new char[Buf2Len+1];
//        Buf3=new char[Buf3Len+1];
//        SendContent1(1,1,0,Buf1,Buf1Len);
//        SendContent1(1,0,0,Buf2,Buf2L2n);
//        SendContent1(1,0,1,Buf3,Buf3Len);
//        delete Buf1;
//        delete Buf2;
//        delete Buf3;
//函数AdjustTime和SendContent1的返回值见下:
//返回 -1:地址回传错误, -2:检验和错, -3:串口没打开, -4:LAN发送数据错误 , -5:LAN不能使用协议V1.0,1:发送正确

//    8)如果用户要设置发送的内容在屏上显示移动方式及字体等则在调用SendContent1方法之前调用方法SetTiaoPingMoveInfo.
//    9)当发送完毕后将串口关闭StopComm

//附录:
//动画方式(Cartoon)取值为:
//    0：吃豆
//    1：射箭
//    2：举重
//    3：狮子
//    4：奔马

//引入，引出方式。
//    引入方式：
//      =0:"右端-左端移入"
//      1:"左端-右端移入"
//      2:"下端-上端卷入"
//      3:"上端-下端卷入"
//      4:"右端-左端跳入"
//      5:"左端-右端展开"
//      6:"右端-左端展开"
//      7:"下端-上端展开"
//      8:"上端-下端展开"
//      9:"中间-两端展开"
//      10:"两端-中间展开"
//      11:"中间-上下展开"
//      12:"上下-中间展开"
//      13:"立   即   显   示"
//      14:"预                备"
//      15:"随                机"
//引出方式：
//      =0:"右端-左端移出"
//      1:"左端-右端移出"
//      2:"下端-上端卷出"
//      3:"上端-下端卷出"
//      4:"右端-左端跳出"
//      5:"左端-右端闭合"
//      6:"右端-左端闭合"
//      7:"下端-上端闭合"
//      8:"上端-下端闭合"
//      9:"中间-两端闭合"
//      10:"两端-中间闭合"
//      11:"中间-上下闭合"
//      12:"上下-中间闭合"
//      13:"立   即   消   失"
//      14:"预                备"
//      15:"随                机"

