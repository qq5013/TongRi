namespace Sorting.Dispatching.View.Report
{
    partial class frmReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.preview1 = new FastReport.Preview.PreviewControl();
            this.SuspendLayout();
            // 
            // preview1
            // 
            this.preview1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.preview1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preview1.Font = new System.Drawing.Font("Tahoma", 8F);
            this.preview1.Location = new System.Drawing.Point(0, 0);
            this.preview1.Name = "preview1";
            this.preview1.PageOffset = new System.Drawing.Point(10, 10);
            this.preview1.Size = new System.Drawing.Size(764, 449);
            this.preview1.StatusbarVisible = false;
            this.preview1.TabIndex = 1;
            this.preview1.UIStyle = FastReport.Utils.UIStyle.VisualStudio2005;
            // 
            // frmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 449);
            this.Controls.Add(this.preview1);
            this.Name = "frmReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmReport";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FastReport.Preview.PreviewControl preview1;
    }
}