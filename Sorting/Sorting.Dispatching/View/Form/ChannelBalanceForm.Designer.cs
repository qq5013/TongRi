namespace Sorting.Dispatching.View
{
    partial class ChannelBalanceForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.bsMain = new System.Windows.Forms.BindingSource(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClearBalance = new System.Windows.Forms.Button();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column1 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column2 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column4 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column5 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column6 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column9 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTool.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.btnClearBalance);
            this.pnlTool.Controls.Add(this.btnPrint);
            this.pnlTool.Controls.Add(this.btnExit);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Size = new System.Drawing.Size(868, 53);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvMain);
            this.pnlContent.Size = new System.Drawing.Size(868, 425);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(868, 478);
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender;
            this.dgvMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMain.AutoGenerateColumns = false;
            this.dgvMain.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMain.ColumnHeadersHeight = 20;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column11,
            this.Column3,
            this.Column1,
            this.Column2,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column9,
            this.Column7,
            this.Column8,
            this.Column10,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16});
            this.dgvMain.DataSource = this.bsMain;
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.Location = new System.Drawing.Point(0, 0);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.RowHeadersWidth = 30;
            this.dgvMain.RowTemplate.Height = 23;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(868, 425);
            this.dgvMain.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExit.Image = global::Sorting.Dispatching.Properties.Resources.close;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(48, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 51);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Image = global::Sorting.Dispatching.Properties.Resources.refresh;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(48, 51);
            this.btnRefresh.TabIndex = 13;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPrint.Image = global::Sorting.Dispatching.Properties.Resources.printer;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPrint.Location = new System.Drawing.Point(96, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(48, 51);
            this.btnPrint.TabIndex = 16;
            this.btnPrint.Text = "打印";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClearBalance
            // 
            this.btnClearBalance.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnClearBalance.Image = global::Sorting.Dispatching.Properties.Resources.go;
            this.btnClearBalance.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClearBalance.Location = new System.Drawing.Point(144, 0);
            this.btnClearBalance.Name = "btnClearBalance";
            this.btnClearBalance.Size = new System.Drawing.Size(48, 51);
            this.btnClearBalance.TabIndex = 17;
            this.btnClearBalance.Text = "清仓";
            this.btnClearBalance.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClearBalance.UseVisualStyleBackColor = true;
            this.btnClearBalance.Click += new System.EventHandler(this.btnClearBalance_Click);
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "ORDERDATE";
            this.Column11.HeaderText = "订单日期";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "LINECODE";
            this.Column3.FilteringEnabled = false;
            this.Column3.HeaderText = "分拣线";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.Width = 80;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "CHANNELNAME";
            this.Column1.FilteringEnabled = false;
            this.Column1.HeaderText = "烟道名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.Width = 80;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "CHANNELTYPE";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.FilteringEnabled = false;
            this.Column2.HeaderText = "烟道类别";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.Width = 80;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "CIGARETTECODE";
            this.Column4.FilteringEnabled = false;
            this.Column4.HeaderText = "卷烟代码";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "CIGARETTENAME";
            this.Column5.FilteringEnabled = false;
            this.Column5.HeaderText = "卷烟名称";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 150;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "QUANTITY";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column6.FilteringEnabled = false;
            this.Column6.HeaderText = "仓库余量";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 60;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "QUANTITY1";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column9.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column9.FilteringEnabled = false;
            this.Column9.HeaderText = "第一批余量";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "QUANTITY2";
            this.Column7.HeaderText = "第二批余量";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "QUANTITY3";
            this.Column8.HeaderText = "第三批余量";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "QUANTITY4";
            this.Column10.HeaderText = "第四批余量";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "QUANTITY5";
            this.Column12.HeaderText = "第五批余量";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "QUANTITY6";
            this.Column13.HeaderText = "第六批余量";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "QUANTITY7";
            this.Column14.HeaderText = "第七批余量";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            // 
            // Column15
            // 
            this.Column15.DataPropertyName = "QUANTITY8";
            this.Column15.HeaderText = "第八批余量";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            // 
            // Column16
            // 
            this.Column16.DataPropertyName = "QUANTITY9";
            this.Column16.HeaderText = "第九批余量";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            // 
            // ChannelBalanceForm
            // 
            this.ClientSize = new System.Drawing.Size(868, 478);
            this.Name = "ChannelBalanceForm";
            this.pnlTool.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.BindingSource bsMain;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClearBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column3;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column1;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column2;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column4;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column5;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column6;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
    }
}
