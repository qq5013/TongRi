namespace Sorting.Dispatching.View.Base
{
    partial class frmHandSort
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHandSort));
            this.btnRefresh = new System.Windows.Forms.Button();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.dgvMaster = new System.Windows.Forms.DataGridView();
            this.ORDERDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column2 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column5 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column7 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column8 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column9 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.bsMaster = new System.Windows.Forms.BindingSource(this.components);
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.SORTNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDERID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIGARETTECODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIGARETTENAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANTITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pnlTool.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.btnExit);
            this.pnlTool.Controls.Add(this.btnDelete);
            this.pnlTool.Controls.Add(this.btnAdd);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Size = new System.Drawing.Size(689, 53);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.scMain);
            this.pnlContent.Size = new System.Drawing.Size(689, 372);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(689, 425);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Image = global::Sorting.Dispatching.Properties.Resources.refresh;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(48, 51);
            this.btnRefresh.TabIndex = 17;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.dgvMaster);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.dgvDetail);
            this.scMain.Size = new System.Drawing.Size(689, 372);
            this.scMain.SplitterDistance = 174;
            this.scMain.TabIndex = 1;
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
            this.Column1,
            this.Column2,
            this.Column5,
            this.Column3,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9});
            this.dgvMaster.DataSource = this.bsMaster;
            this.dgvMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaster.Location = new System.Drawing.Point(0, 0);
            this.dgvMaster.MultiSelect = false;
            this.dgvMaster.Name = "dgvMaster";
            this.dgvMaster.ReadOnly = true;
            this.dgvMaster.RowHeadersWidth = 30;
            this.dgvMaster.RowTemplate.Height = 23;
            this.dgvMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaster.Size = new System.Drawing.Size(689, 174);
            this.dgvMaster.TabIndex = 0;
            this.dgvMaster.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaster_RowEnter);
            // 
            // ORDERDATE
            // 
            this.ORDERDATE.DataPropertyName = "ORDERDATE";
            this.ORDERDATE.HeaderText = "订单日期";
            this.ORDERDATE.Name = "ORDERDATE";
            this.ORDERDATE.ReadOnly = true;
            this.ORDERDATE.Width = 80;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "SORTID";
            this.Column1.FilteringEnabled = false;
            this.Column1.HeaderText = "订单顺序";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 80;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "ORDERID";
            this.Column2.FilteringEnabled = false;
            this.Column2.HeaderText = "订单号";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 150;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "ROUTECODE";
            this.Column5.FilteringEnabled = false;
            this.Column5.HeaderText = "线路代码";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 110;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "AMOUNT";
            this.Column3.HeaderText = "总数量";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "ROUTENAME";
            this.Column6.FilteringEnabled = false;
            this.Column6.HeaderText = "线路名称";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 150;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "CUSTOMERCODE";
            this.Column7.FilteringEnabled = false;
            this.Column7.HeaderText = "客户代码";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 110;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "CUSTOMERDESC";
            this.Column8.FilteringEnabled = false;
            this.Column8.HeaderText = "客户名称";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 150;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "DELIVERYDATE";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column9.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column9.FilteringEnabled = false;
            this.Column9.HeaderText = "送货日期";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 110;
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Lavender;
            this.dgvDetail.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDetail.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SORTNO,
            this.ORDERID,
            this.CIGARETTECODE,
            this.CIGARETTENAME,
            this.QUANTITY});
            this.dgvDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetail.Location = new System.Drawing.Point(0, 0);
            this.dgvDetail.MultiSelect = false;
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.ReadOnly = true;
            this.dgvDetail.RowHeadersWidth = 30;
            this.dgvDetail.RowTemplate.Height = 23;
            this.dgvDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetail.Size = new System.Drawing.Size(689, 194);
            this.dgvDetail.TabIndex = 0;
            // 
            // SORTNO
            // 
            this.SORTNO.DataPropertyName = "ORDERDETAILID";
            this.SORTNO.HeaderText = "序号";
            this.SORTNO.Name = "SORTNO";
            this.SORTNO.ReadOnly = true;
            this.SORTNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SORTNO.Width = 70;
            // 
            // ORDERID
            // 
            this.ORDERID.DataPropertyName = "ORDERID";
            this.ORDERID.FillWeight = 150F;
            this.ORDERID.HeaderText = "订单号";
            this.ORDERID.Name = "ORDERID";
            this.ORDERID.ReadOnly = true;
            this.ORDERID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CIGARETTECODE
            // 
            this.CIGARETTECODE.DataPropertyName = "CIGARETTECODE";
            this.CIGARETTECODE.HeaderText = "卷烟代码";
            this.CIGARETTECODE.Name = "CIGARETTECODE";
            this.CIGARETTECODE.ReadOnly = true;
            this.CIGARETTECODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CIGARETTENAME
            // 
            this.CIGARETTENAME.DataPropertyName = "CIGARETTENAME";
            this.CIGARETTENAME.HeaderText = "卷烟名称";
            this.CIGARETTENAME.Name = "CIGARETTENAME";
            this.CIGARETTENAME.ReadOnly = true;
            this.CIGARETTENAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CIGARETTENAME.Width = 200;
            // 
            // QUANTITY
            // 
            this.QUANTITY.DataPropertyName = "QUANTITY";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.QUANTITY.DefaultCellStyle = dataGridViewCellStyle6;
            this.QUANTITY.HeaderText = "数量";
            this.QUANTITY.Name = "QUANTITY";
            this.QUANTITY.ReadOnly = true;
            this.QUANTITY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.QUANTITY.Width = 80;
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExit.Image = global::Sorting.Dispatching.Properties.Resources.close;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(144, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 51);
            this.btnExit.TabIndex = 59;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDelete.Image = global::Sorting.Dispatching.Properties.Resources.delete;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDelete.Location = new System.Drawing.Point(96, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(48, 51);
            this.btnDelete.TabIndex = 58;
            this.btnDelete.Text = "删除";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAdd.Location = new System.Drawing.Point(48, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(48, 51);
            this.btnAdd.TabIndex = 57;
            this.btnAdd.Text = "新增";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // frmHandSort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 425);
            this.Name = "frmHandSort";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "手动分拣管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlTool.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.DataGridView dgvMaster;
        private System.Windows.Forms.DataGridView dgvDetail;
        private System.Windows.Forms.BindingSource bsMaster;
        protected System.Windows.Forms.Button btnExit;
        protected System.Windows.Forms.Button btnDelete;
        protected System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn SORTNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDERID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIGARETTECODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIGARETTENAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANTITY;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDERDATE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column1;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column2;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column6;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column7;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column8;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column9;
    }
}