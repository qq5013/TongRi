using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Sorting.ASCS
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool ExisFlag = false;
            System.Diagnostics.Process currentProccess = System.Diagnostics.Process.GetCurrentProcess();
            System.Diagnostics.Process[] currentProccessArray = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process p in currentProccessArray)
            {
                if (p.ProcessName == currentProccess.ProcessName && p.Id != currentProccess.Id)
                {
                    ExisFlag = true;
                    break;
                }
            }

            if (ExisFlag)
            {
                MessageBox.Show("分拣监控系统已经执行！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                Application.Run(new MainForm());
            }
        }
    }
}