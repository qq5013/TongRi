using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace Sorting.Dispatching
{
    public class ExcelHelper
    {
        /// <summary>
        ///  从excel2007文件中读出dt
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static System.Data.DataTable ExcelToDataTable(string fileName)
        {
            System.Data.DataTable dt;
            string conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1'";
            OleDbConnection myConn = new OleDbConnection(conStr);
            string strCom = " SELECT * FROM [Sheet1$]";
            myConn.Open();
            OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn);
            dt = new System.Data.DataTable();
            myCommand.Fill(dt);
            myConn.Close();
            return dt;
        }


        public static void DataTabletoExcel(System.Data.DataTable tmpDataTable, string strFileName)
        {
            if (tmpDataTable == null)
                return;

            int rowNum = tmpDataTable.Rows.Count;
            int columnNum = tmpDataTable.Columns.Count;
            int rowIndex = 1;
            int columnIndex = 0;

            Application xlApp = new ApplicationClass();
            xlApp.DefaultFilePath = "";
            xlApp.DisplayAlerts = true;
            xlApp.SheetsInNewWorkbook = 1;
            Workbook xlBook = xlApp.Workbooks.Add(true);

            //将DataTable的列名导入Excel表第一行
            foreach (DataColumn dc in tmpDataTable.Columns)
            {
                columnIndex++;
                xlApp.Cells[rowIndex, columnIndex] = dc.ColumnName;
            }

            //将DataTable中的数据导入Excel中

            for (int i = 0; i < rowNum; i++)
            {
                rowIndex++;
                columnIndex = 0;
                for (int j = 0; j < columnNum; j++)
                {
                    columnIndex++;
                    xlApp.Cells[rowIndex, columnIndex] = tmpDataTable.Rows[i][j].ToString();
                }
            }
            //xlBook.SaveCopyAs(HttpUtility.UrlDecode(strFileName, System.Text.Encoding.UTF8));
            xlBook.SaveCopyAs(strFileName);
            xlBook.Close(false);
        }
        /// <summary>
        /// DataTable直接导出Excel,此方法会把DataTable的数据用Excel打开,再自己手动去保存到确切的位置
        /// </summary>
        /// <param name="dt">要导出Excel的DataTable</param>
        /// <returns></returns>
        public static bool DoExport(System.Data.DataTable dt)
        {
            Application app = new ApplicationClass();
            if (app == null)
            {
                throw new Exception("Excel无法启动");
            }
            app.Visible = true;
            Workbooks wbs = app.Workbooks;
            Workbook wb = wbs.Add(Missing.Value);
            Worksheet ws = (Worksheet)wb.Worksheets[1];

            int cnt = dt.Rows.Count;
            int columncnt = dt.Columns.Count;

            // *****************获取数据********************
            object[,] objData = new Object[cnt + 1, columncnt];  // 创建缓存数据
            // 获取列标题
            for (int i = 0; i < columncnt; i++)
            {
                string columnName = GetColumnName(dt.Columns[i].ColumnName);
                objData[0, i] = columnName;
            }
            // 获取具体数据
            for (int i = 0; i < cnt; i++)
            {
                System.Data.DataRow dr = dt.Rows[i];
                for (int j = 0; j < columncnt; j++)
                {
                    objData[i + 1, j] = dr[j];
                }
            }
            //********************* 写入Excel******************
            Range r = ws.get_Range(app.Cells[1, 1], app.Cells[cnt + 1, columncnt]);
            r.NumberFormat = "@";
            //r = r.get_Resize(cnt+1, columncnt);
            r.Value2 = objData;
            r.EntireColumn.AutoFit();

            app = null;
            return true;

        }
        private static string GetColumnName(string columnName)
        {
            string chnName = columnName;
            if (columnName == "BATCHNO")
                chnName = "批次号";
            else if (columnName == "ORDERDATE")
                chnName = "订单日期";
            else if (columnName == "ORDERID")
                chnName = "订单编号";
            else if (columnName == "SORTNO")
                chnName = "订单序号";
            else if (columnName == "CHANNELCODE")
                chnName = "货仓编号";
            else if (columnName == "CHANNELNAME")
                chnName = "货仓名称";
            else if (columnName == "CHANNELTYPE")
                chnName = "货仓类型";
            else if (columnName == "ProductCode")
                chnName = "产品代码";
            else if (columnName == "ProductName")
                chnName = "产品名称";
            else if (columnName == "QUANTITY")
                chnName = "数量";
            else if (columnName == "BOXES")
                chnName = "件数";
            else if (columnName == "BALANCE")
                chnName = "尾数";
            else if (columnName == "LINECODE")
                chnName = "线路编号";
            else if (columnName == "CUSTOMERCODE")
                chnName = "客户编号";
            else if (columnName == "CUDTOMERNAME")
                chnName = "客户姓名";
            else if (columnName == "ROUTECODE")
                chnName = "线路编号";
            else if (columnName == "ROUTENAME")
                chnName = "线路名称";
            else if (columnName == "ISABNORMITY")
                chnName = "异形";
            else if (columnName == "DELIVERDATE")
                chnName = "送货日期";
            else if (columnName == "SORTID")
                chnName = "配送顺序";
            else if(columnName=="TOTAL_CIGARETTE_QUANTITY")
                chnName = "订单数量";
            else if (columnName == "TOTAL_CIGARETTE_QUANTITY")
                chnName = "订单数量";
            else if (columnName == "TOTAL_QUANTITY")
                chnName = "订单总数量";
            else if (columnName == "CIGARETTE_PERCENT")
                chnName = "所占百分比";
            else if (columnName == "CIGARETTE_COUNT")
                chnName = "品规数量";
            else if (columnName == "QUANTITY1")
                chnName = "第一批余量";
            else if (columnName == "QUANTITY2")
                chnName = "第二批余量";
            else if (columnName == "QUANTITY3")
                chnName = "第三批余量";
            else if (columnName == "QUANTITY4")
                chnName = "第四批余量";
            else if (columnName == "QUANTITY5")
                chnName = "第五批余量";
            else if (columnName == "QUANTITY6")
                chnName = "第六批余量";
            else if (columnName == "QUANTITY7")
                chnName = "第七批余量";
            else if (columnName == "QUANTITY8")
                chnName = "第八批余量";
            else if (columnName == "QUANTITY9")
                chnName = "第九批余量";
            else if (columnName == "CHANNELID")
                chnName = "货仓ID";
            else if (columnName == "ROWID")
                chnName = "序号";
            else if (columnName == "BREAKTYPE")
                chnName = "故障类型";
            else if (columnName == "BREAKNAME")
                chnName = "故障名称";
            else if (columnName == "BREAKCOUNT")
                chnName = "故障次数";
            else if (columnName == "BEGINTIME")
                chnName = "开始时间";
            else if (columnName == "ENDTIME")
                chnName = "结束时间";
            else if (columnName == "BREAKTIME")
                chnName = "故障时长";
            else if (columnName == "ISDOWNLOAD")
                chnName = "是否下载";
            else if (columnName == "ISUPLOAD")
                chnName = "是否上传";
            else if (columnName == "ISVALID")
                chnName = "是否优化";
            else if (columnName == "ISUPTONOONEPRO")
                chnName = "上传一号工程";
            else if (columnName == "BEGINSORTTIME")
                chnName = "开始分拣时间";
            else if (columnName == "ENDSORTTIME")
                chnName = "结束分拣时间";
            else if (columnName == "AMOUNT")
                chnName = "总分拣数量";
            else if (columnName == "TAMOUNT")
                chnName = "通道机数量";
            else if (columnName == "LAMOUNT")
                chnName = "立式机数量";
            else if (columnName == "SORTTIME")
                chnName = "分拣时长";
            else if (columnName == "EFFICIENCY")
                chnName = "分拣效率";
            else if (columnName == "REMAINQUANTITY")
                chnName = "剩余数量";
            else if (columnName == "BOXQUANTITY")
                chnName = "剩余件数";
            else if (columnName == "ITEMQUANTITY")
                chnName = "剩余条数";
            else if (columnName == "SORTQUANTITY")
                chnName = "已分拣量";
            return chnName;
        }
    }
}
