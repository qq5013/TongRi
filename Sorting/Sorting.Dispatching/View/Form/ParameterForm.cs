using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MCP.Config;
using DB.Util;
using ParamUtil;

namespace Sorting.Dispatching.View
{
    public partial class ParameterForm : OpView.View.ToolbarForm
    {
        private Parameter parameter = new Parameter();
        private DBConfigUtil config = new DBConfigUtil("DefaultConnection", "SQLSERVER");
        private DBConfigUtil serverConfig = new DBConfigUtil("ServerConnection", "SQLSERVER");
        private DBConfigUtil stockConfig = new DBConfigUtil("StockServerConnection", "SQLSERVER");
        private Dictionary<string, string> attributes = null;

        public ParameterForm()
        {
            InitializeComponent();
            ReadParameter();
        }

        private void ReadParameter()
        {
            //本机数据库连接参数
            parameter.ServerName = config.Parameters["server"].ToString();
            parameter.DBName = config.Parameters["database"].ToString();
            parameter.DBUser = config.Parameters["uid"].ToString();
            parameter.Password = config.Parameters["password"].ToString();

            //仓储系统数据库连接参数
            parameter.StockServerName = stockConfig.Parameters["server"].ToString();
            parameter.StockDBName = stockConfig.Parameters["database"].ToString();
            parameter.StockDBUser = stockConfig.Parameters["uid"].ToString();
            parameter.StockPwassword = stockConfig.Parameters["password"].ToString();

            //读取Context配置文件参数
            ConfigUtil configUtil = new ConfigUtil();
            attributes = configUtil.GetAttribute();
            parameter.LineCode = attributes["LineCode"];
            parameter.LogisticsUrl = attributes["LogisticsUrl"];
            parameter.LedTextLength = attributes["LedTextLength"];
            parameter.PortName = attributes["LedCOM"];
            parameter.AppendPara = attributes["AppendPara"];
            parameter.FontColor = attributes["FontColor"];
            parameter.FontColorBreak = attributes["FontColorBreak"];
            parameter.FontColorEmpty = attributes["FontColorEmpty"];
            parameter.SpeakCount = attributes["SpeakCount"];
            propertyGrid.SelectedObject = parameter;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //保存本机数据库连接参数
                config.Parameters["server"] = parameter.ServerName;
                config.Parameters["database"] = parameter.DBName;
                config.Parameters["uid"] = parameter.DBUser;
                config.Parameters["Password"] = config.Parameters["Password"].ToString() == parameter.Password ? parameter.Password : DB.Util.Coding.Encoding(parameter.Password);
                config.Save();

                //保存仓储系统数据库连接参数
                stockConfig.Parameters["server"] = parameter.StockServerName;
                stockConfig.Parameters["database"] = parameter.StockDBName;
                stockConfig.Parameters["uid"] = parameter.StockDBUser;
                stockConfig.Parameters["Password"] = stockConfig.Parameters["Password"].ToString() == parameter.StockPwassword ? parameter.StockPwassword : DB.Util.Coding.Encoding(parameter.StockPwassword);
                stockConfig.Save();

                //保存Context参数
                attributes["LineCode"] = parameter.LineCode;
                attributes["LogisticsUrl"] = parameter.LogisticsUrl;
                attributes["LedTextLength"] = parameter.LedTextLength;
                attributes["LedCOM"] = parameter.PortName;
                attributes["AppendPara"] = parameter.AppendPara;
                attributes["FontColor"] = parameter.FontColor;
                attributes["FontColorBreak"] = parameter.FontColorBreak;
                attributes["FontColorEmpty"] = parameter.FontColorEmpty;

                ConfigUtil configUtil = new ConfigUtil();
                configUtil.Save(attributes);


                MessageBox.Show("系统参数保存成功，请重新启动本系统。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show("保存系统参数过程中出现异常，原因：" + exp.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }
    }
}

