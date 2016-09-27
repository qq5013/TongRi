using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Microsoft.Win32;
using Sorting.Dispatching.Dal;

namespace Sorting.Dispatching.Schedule
{
    public class UploadData
    {
        private Dictionary<string, string> parameter = null;
        private string txtFile = "";
        private string zipFile = "";
        private bool isAbnormity = false;

        public UploadData()
        {
            parameter = new ParameterDal().FindParameter();
            txtFile = "RetailerOrder" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
            zipFile = parameter["NoOneProFilePath"] + txtFile + ".zip";
            txtFile = txtFile + ".Order";

            try
            {
                DirectoryInfo dir = new DirectoryInfo(parameter["NoOneProFilePath"]);
                if (!dir.Exists)
                    dir.Create();                
            }
            catch (Exception e)
            {
                ProcessState.Status = "ERROR";
                ProcessState.Message = e.Message;
            }
        }

        public UploadData(bool IsAbnormity)
        {
            parameter = new Dal.ParameterDal().FindParameter();
            txtFile = "RetailerOrder" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
            zipFile = parameter["NoOneProFilePath"] + txtFile + ".zip";
            txtFile = txtFile + (IsAbnormity ? ".YXOrder" : ".Order");
            this.isAbnormity = IsAbnormity;
            try
            {
                DirectoryInfo dir = new DirectoryInfo(parameter["NoOneProFilePath"]);
                if (!dir.Exists)
                    dir.Create();
            }
            catch (Exception e)
            {
                ProcessState.Status = "ERROR";
                ProcessState.Message = e.Message;
            }
        }

        public void Upload(string orderDate, string batchNo)
        {
            try
            {
                ProcessState.Status = "PROCESSING";
                ProcessState.TotalCount = 5;
                ProcessState.Step = 1;

                CreateDataFile(orderDate,batchNo);
                ProcessState.CompleteCount = 1;

                CreateZipFile();
                ProcessState.CompleteCount = 2;

                SendZipFile();
                ProcessState.CompleteCount = 3;

                SaveUploadStatus(batchNo);
                ProcessState.CompleteCount = 4;

                DeleteFiles();
                ProcessState.CompleteCount = 5;
            }
            catch (Exception e)
            {
                ProcessState.Status = "ERROR";
                ProcessState.Message = e.Message;
            }
        }

        /// <summary>
        /// ���������ļ�
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public bool CreateDataFile(string orderDate, string batchNo)
        {
            bool hasData = false;
            FileStream file = new FileStream(parameter["NoOneProFilePath"] + txtFile, FileMode.Create);
            StreamWriter writer = new StreamWriter(file, Encoding.UTF8);
            OrderScheduleDal orderDal = new OrderScheduleDal();
            DataTable table;
            int columnCount;

            try
            {
                if (!this.isAbnormity)
                {
                    //�����ּ����
                    table = orderDal.GetOrder(orderDate, batchNo, 1);
                    columnCount = table.Columns.Count;
                    foreach (DataRow row in table.Rows)
                    {
                        //string s = row["SORTNO"].ToString();
                        string s = row["ID"].ToString();
                        for (int i = 1; i < columnCount; i++)
                            s += ("," + row[i].ToString().Trim());
                        s += ";";
                        writer.WriteLine(s);
                        writer.Flush();
                        hasData = true;
                    }
                }

                if (!this.isAbnormity)
                {
                    //�ֹ��ּ����
                    table = orderDal.GetOrder(orderDate, batchNo, 2);
                    columnCount = table.Columns.Count;
                    foreach (DataRow row in table.Rows)
                    {
                        string s = row["ID"].ToString();
                        for (int i = 1; i < columnCount; i++)
                            s += ("," + row[i].ToString().Trim());
                        s += ";";
                        writer.WriteLine(s);
                        writer.Flush();
                        hasData = true;
                    }
                }

                if (!this.isAbnormity)
                {
                    //�����ּ����
                    table = orderDal.GetOrder(orderDate, batchNo, 3);
                    columnCount = table.Columns.Count;
                    foreach (DataRow row in table.Rows)
                    {
                        string s = row["SORTNO"].ToString();
                        for (int i = 1; i < columnCount; i++)
                            s += ("," + row[i].ToString().Trim());
                        s += ";";
                        writer.WriteLine(s);
                        writer.Flush();
                        hasData = true;
                    }
                }

                if (this.isAbnormity)
                {
                    //���ηּ����
                    table = orderDal.GetOrder(orderDate, batchNo, 4);
                    columnCount = table.Columns.Count;
                    int index_cigarette = 0;
                    string cigaretteCode = "";
                    foreach (DataRow row in table.Rows)
                    {
                        if (cigaretteCode != row[4].ToString().Trim())
                        {
                            index_cigarette++;
                            cigaretteCode = row[4].ToString().Trim();
                        }
                        string s = row["ID"].ToString();
                        s += ("," + index_cigarette.ToString());

                        for (int i = 2; i < columnCount; i++)
                            s += ("," + row[i].ToString().Trim());
                        s += ";";
                        writer.WriteLine(s);
                        writer.Flush();
                        hasData = true;
                    }
                }

                file.Close();
                return hasData;
            }
            catch (Exception e)
            {
                file.Close();
                throw e;
            }
        }

        /// <summary>
        /// ����ѹ���ļ�
        /// </summary>
        public void CreateZipFile()
        {
            //String the_rar;
            //RegistryKey the_Reg;
            //Object the_Obj;
            String the_Info;
            ProcessStartInfo the_StartInfo;
            System.Diagnostics.Process zipProcess;

            ////the_Reg = Registry.ClassesRoot.OpenSubKey("Applications\\WinRAR.exe\\Shell\\Open\\Command");
            ////the_Obj = the_Reg.GetValue("");
            ////the_rar = the_Obj.ToString();
            ////the_Reg.Close();
            //the_rar = the_rar.Substring(1, the_rar.Length - 7);
            the_Info = " a    " + zipFile + "  " + txtFile;
            the_StartInfo = new ProcessStartInfo();
            the_StartInfo.WorkingDirectory = parameter["NoOneProFilePath"];
            the_StartInfo.FileName = @"C:\Program Files\WinRAR\WinRAR.exe";
            the_StartInfo.Arguments = the_Info;
            the_StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            zipProcess = new System.Diagnostics.Process();
            zipProcess.StartInfo = the_StartInfo;
            zipProcess.Start();

            //�ȴ�ѹ���ļ������˳�
            while (!zipProcess.HasExited)
            {
                System.Threading.Thread.Sleep(100);
            }
            //ZipFloClass.ZipFile(parameter["NoOneProFilePath"] + txtFile,txtFile, zipFile);
        }

        /// <summary>
        /// ����ѹ���ļ�������һ�Ź���
        /// </summary>
        public void SendZipFile()
        {
            TcpClient client = new TcpClient();

            client.Connect(parameter["NoOneProIP"], Convert.ToInt32(parameter["NoOneProPort"]));
            NetworkStream stream = client.GetStream();

            FileStream file = new FileStream(zipFile, FileMode.Open);
            
            int fileLength = (int)file.Length;
            byte[] fileBytes = BitConverter.GetBytes(fileLength);
            Array.Reverse(fileBytes);
            //�����ļ�����
            stream.Write(fileBytes, 0, 4);
            stream.Flush();

            byte[] data = new byte[1024];
            int len = 0;
            while ((len = file.Read(data, 0, 1024)) > 0)
            {
                stream.Write(data, 0, len);
            }

            file.Close();

            byte[] buffer = new byte[1024];
            string recvStr = "";
            while (true)
            {
                int bytes = stream.Read(buffer, 0, 1024);

                if (bytes <= 0)
                    continue;
                else
                {
                    recvStr = Encoding.ASCII.GetString(buffer, bytes - 3, 2);

                    if (recvStr == "##")
                    {
                        recvStr = Encoding.GetEncoding("gb2312").GetString(buffer, 4, bytes - 5);                        
                        break;
                    }
                }
            }
            client.Close();

            if (recvStr.Split(';').Length > 2)
            {
                throw new Exception(recvStr);
            }        
        }

        /// <summary>
        /// �����ϴ�����״̬
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void SaveUploadStatus(string batchNo)
        {
            BatchDal batchDal = new BatchDal();
            batchDal.SaveUploadUser("Admin", batchNo);
        }

        /// <summary>
        /// ɾ�������ļ���ѹ���ļ�
        /// </summary>
        public void DeleteFiles()
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(parameter["NoOneProFilePath"]);
                FileInfo[] files = dir.GetFiles("*.*Order");

                if (files != null)
                {
                    foreach (FileInfo file in files)
                        file.Delete();
                }

                dir = new DirectoryInfo(parameter["NoOneProFilePath"]);
                if (dir.Exists)
                {
                    files = dir.GetFiles("*.zip");
                    if (files != null)
                    {
                        foreach (FileInfo file in files)
                            file.Delete();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
