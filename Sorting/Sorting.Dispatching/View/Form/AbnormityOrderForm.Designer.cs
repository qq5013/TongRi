namespace Sorting.Dispatching.View
{
    partial class AbnormityOrderForm
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
            this.bsMaster = new System.Windows.Forms.BindingSource(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvMaster = new System.Windows.Forms.DataGridView();
            this.btnToExcel = new System.Windows.Forms.Button();
            this.ORDERDATE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column2 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column3 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.pnlTool.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaster)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.btnToExcel);
            this.pnlTool.Controls.Add(this.btnExit);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Size = new System.Drawing.Size(1044, 53);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvMaster);
            this.pnlContent.Size = new System.Drawing.Size(1044, 405);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1044, 458);
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExit.Image = global::Sorting.Dispatching.Properties.Resources.close;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(48, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 51);
            this.btnExit.TabIndex = 16;
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
            this.btnRefresh.TabIndex = 15;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvMaster
            // 
            this.dgvMaster.AllowUserToAddRows = false;
            this.dgvMaster.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender;
            this.dgvMaster.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMaster.AutoGenerateColumns = false;
            this.dgvMaster.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMaster.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaster.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ORDERDATE,
            this.Column10,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column8,
            this.Column9,
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgvMaster.DataSource = this.bsMaster;
            this.dgvMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaster.Location = new System.Drawing.Point(0, 0);
            this.dgvMaster.MultiSelect = false;
            this.dgvMaster.Name = "dgvMaster";
            this.dgvMaster.ReadOnly = true;
            this.dgvMaster.RowHeadersWidth = 30;
            this.dgvMaster.RowTemplate.Height = 23;
            this.dgvMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaster.Size = new System.Drawing.Size(1044, 405);
            this.dgvMaster.TabIndex = 1;
            // 
            // btnToExcel
            // 
            this.btnToExcel.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnToExcel.Image = global::Sorting.Dispatching.Properties.Resources.go;
            this.btnToExcel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnToExcel.Location = new System.Drawing.Point(96, 0);
            this.btnToExcel.Name = "btnToExcel";
            this.btnToExcel.Size = new System.Drawing.Size(48, 51);
            this.btnToExcel.TabIndex = 17;
            this.btnToExcel.Text = "导出";
            this.btnToExcel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnToExcel.UseVisualStyleBackColor = true;
            this.btnToExcel.Click += new System.EventHandler(this.btnToExcel_Click);
            // 
            // ORDERDATE
            // 
            this.ORDERDATE.DataPropertyName = "ORDERDATE";
            this.ORDERDATE.FilteringEnabled = false;
            this.ORDERDATE.HeaderText = "订单日期";
            this.ORDERDATE.Name = "ORDERDATE";
            this.ORDERDATE.ReadOnly = true;
            this.ORDERDATE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ORDERDATE.Width = 80;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "SORTNO";
            this.Column10.HeaderText = "订单序号";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 80;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "ORDERID";
            this.Column4.HeaderText = "订单编号";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "CUSTOMERCODE";
            this.Column5.HeaderText = "客户编号";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "CUSTOMERNAME";
            this.Column6.HeaderText = "客户名称";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "ROUTECODE";
            this.Column8.HeaderText = "线路编码";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "ROUTENAME";
            this.Column9.HeaderText = "线路名称";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 150;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "PRODUCTCODE";
            this.Column1.FilteringEnabled = false;
            this.Column1.HeaderText = "产品编号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "PRODUCTNAME";
            this.Column2.FilteringEnabled = false;
            this.Column2.HeaderText = "产品名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.Width = 150;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "QUANTITY";
            this.Column3.FilteringEnabled = false;
            this.Column3.HeaderText = "订单数量";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // AbnormityOrderForm
            // 
            this.ClientSize = new System.Drawing.Size(1044, 458);
            this.Name = "AbnormityOrderForm";
            this.pnlTool.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaster)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.BindingSource bsMaster;
        private System.Windows.Forms.DataGridView dgvMaster;
        private System.Windows.Forms.Button btnToExcel;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn ORDERDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column1;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column2;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column3;
    }
}
