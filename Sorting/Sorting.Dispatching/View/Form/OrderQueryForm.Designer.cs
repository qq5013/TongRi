namespace Sorting.Dispatching.View
{
    partial class OrderQueryForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.dgvMaster = new System.Windows.Forms.DataGridView();
            this.bsMaster = new System.Windows.Forms.BindingSource(this.components);
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.ORDERDATE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.BATCHNO = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.MAINSORTNO = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.MAINORDERID = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.ROUTECODE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.ROUTENAME = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CUSTOMERCODE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CUSTOMERNAME = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.MAINQUANTITY = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.QUANTITY1 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.STATUS = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.STATUS1 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.PACKAGE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SORTNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDERID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHANNELNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHANNELTYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIGARETTECODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIGARETTENAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANTITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHANNELLINE = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.pnlTool.Controls.Add(this.btnEnd);
            this.pnlTool.Controls.Add(this.btnUpdate);
            this.pnlTool.Controls.Add(this.btnExit);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Size = new System.Drawing.Size(1044, 53);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.scMain);
            this.pnlContent.Size = new System.Drawing.Size(1044, 405);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1044, 458);
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
            this.scMain.Size = new System.Drawing.Size(1044, 405);
            this.scMain.SplitterDistance = 191;
            this.scMain.TabIndex = 0;
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
            this.BATCHNO,
            this.MAINSORTNO,
            this.MAINORDERID,
            this.ROUTECODE,
            this.ROUTENAME,
            this.CUSTOMERCODE,
            this.CUSTOMERNAME,
            this.MAINQUANTITY,
            this.QUANTITY1,
            this.STATUS,
            this.STATUS1,
            this.PACKAGE});
            this.dgvMaster.DataSource = this.bsMaster;
            this.dgvMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaster.Location = new System.Drawing.Point(0, 0);
            this.dgvMaster.MultiSelect = false;
            this.dgvMaster.Name = "dgvMaster";
            this.dgvMaster.ReadOnly = true;
            this.dgvMaster.RowHeadersWidth = 30;
            this.dgvMaster.RowTemplate.Height = 23;
            this.dgvMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaster.Size = new System.Drawing.Size(1044, 191);
            this.dgvMaster.TabIndex = 0;
            this.dgvMaster.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaster_RowEnter);
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Lavender;
            this.dgvDetail.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDetail.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column11,
            this.SORTNO,
            this.ORDERID,
            this.CHANNELNAME,
            this.CHANNELTYPE,
            this.CIGARETTECODE,
            this.CIGARETTENAME,
            this.QUANTITY,
            this.CHANNELLINE});
            this.dgvDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetail.Location = new System.Drawing.Point(0, 0);
            this.dgvDetail.MultiSelect = false;
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.ReadOnly = true;
            this.dgvDetail.RowHeadersWidth = 30;
            this.dgvDetail.RowTemplate.Height = 23;
            this.dgvDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetail.Size = new System.Drawing.Size(1044, 210);
            this.dgvDetail.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Image = global::Sorting.Dispatching.Properties.Resources.close;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(144, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 51);
            this.btnExit.TabIndex = 17;
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
            // btnUpdate
            // 
            this.btnUpdate.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnUpdate.Image = global::Sorting.Dispatching.Properties.Resources.document_edit;
            this.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnUpdate.Location = new System.Drawing.Point(48, 0);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(48, 51);
            this.btnUpdate.TabIndex = 16;
            this.btnUpdate.Text = "修正";
            this.btnUpdate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Image = global::Sorting.Dispatching.Properties.Resources.close;
            this.btnEnd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEnd.Location = new System.Drawing.Point(95, 0);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(50, 51);
            this.btnEnd.TabIndex = 18;
            this.btnEnd.Text = "结束";
            this.btnEnd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // ORDERDATE
            // 
            this.ORDERDATE.DataPropertyName = "ORDERDATE";
            this.ORDERDATE.FilteringEnabled = false;
            this.ORDERDATE.HeaderText = "订单日期";
            this.ORDERDATE.Name = "ORDERDATE";
            this.ORDERDATE.ReadOnly = true;
            this.ORDERDATE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // BATCHNO
            // 
            this.BATCHNO.DataPropertyName = "BATCHNO";
            this.BATCHNO.FilteringEnabled = false;
            this.BATCHNO.HeaderText = "分拣批次";
            this.BATCHNO.Name = "BATCHNO";
            this.BATCHNO.ReadOnly = true;
            this.BATCHNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // MAINSORTNO
            // 
            this.MAINSORTNO.DataPropertyName = "SORTNO";
            this.MAINSORTNO.FilteringEnabled = false;
            this.MAINSORTNO.HeaderText = "流水号";
            this.MAINSORTNO.Name = "MAINSORTNO";
            this.MAINSORTNO.ReadOnly = true;
            // 
            // MAINORDERID
            // 
            this.MAINORDERID.DataPropertyName = "ORDERID";
            this.MAINORDERID.FilteringEnabled = false;
            this.MAINORDERID.HeaderText = "订单号";
            this.MAINORDERID.Name = "MAINORDERID";
            this.MAINORDERID.ReadOnly = true;
            // 
            // ROUTECODE
            // 
            this.ROUTECODE.DataPropertyName = "ROUTECODE";
            this.ROUTECODE.FilteringEnabled = false;
            this.ROUTECODE.HeaderText = "线路代码";
            this.ROUTECODE.Name = "ROUTECODE";
            this.ROUTECODE.ReadOnly = true;
            // 
            // ROUTENAME
            // 
            this.ROUTENAME.DataPropertyName = "ROUTENAME";
            this.ROUTENAME.FilteringEnabled = false;
            this.ROUTENAME.HeaderText = "线路名称";
            this.ROUTENAME.Name = "ROUTENAME";
            this.ROUTENAME.ReadOnly = true;
            this.ROUTENAME.Width = 120;
            // 
            // CUSTOMERCODE
            // 
            this.CUSTOMERCODE.DataPropertyName = "CUSTOMERCODE";
            this.CUSTOMERCODE.FilteringEnabled = false;
            this.CUSTOMERCODE.HeaderText = "客户代码";
            this.CUSTOMERCODE.Name = "CUSTOMERCODE";
            this.CUSTOMERCODE.ReadOnly = true;
            // 
            // CUSTOMERNAME
            // 
            this.CUSTOMERNAME.DataPropertyName = "CUSTOMERNAME";
            this.CUSTOMERNAME.FilteringEnabled = false;
            this.CUSTOMERNAME.HeaderText = "客户名称";
            this.CUSTOMERNAME.Name = "CUSTOMERNAME";
            this.CUSTOMERNAME.ReadOnly = true;
            this.CUSTOMERNAME.Width = 120;
            // 
            // MAINQUANTITY
            // 
            this.MAINQUANTITY.DataPropertyName = "QUANTITY";
            this.MAINQUANTITY.FilteringEnabled = false;
            this.MAINQUANTITY.HeaderText = "通道机数量";
            this.MAINQUANTITY.Name = "MAINQUANTITY";
            this.MAINQUANTITY.ReadOnly = true;
            this.MAINQUANTITY.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // QUANTITY1
            // 
            this.QUANTITY1.DataPropertyName = "QUANTITY1";
            this.QUANTITY1.FilteringEnabled = false;
            this.QUANTITY1.HeaderText = "立式机数量";
            this.QUANTITY1.Name = "QUANTITY1";
            this.QUANTITY1.ReadOnly = true;
            this.QUANTITY1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // STATUS
            // 
            this.STATUS.DataPropertyName = "STATUS";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.STATUS.DefaultCellStyle = dataGridViewCellStyle3;
            this.STATUS.FilteringEnabled = false;
            this.STATUS.HeaderText = "通道机下单";
            this.STATUS.Name = "STATUS";
            this.STATUS.ReadOnly = true;
            this.STATUS.Width = 110;
            // 
            // STATUS1
            // 
            this.STATUS1.DataPropertyName = "STATUS1";
            this.STATUS1.FilteringEnabled = false;
            this.STATUS1.HeaderText = "立式机下单";
            this.STATUS1.Name = "STATUS1";
            this.STATUS1.ReadOnly = true;
            this.STATUS1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.STATUS1.Width = 110;
            // 
            // PACKAGE
            // 
            this.PACKAGE.DataPropertyName = "STATUS2";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.PACKAGE.DefaultCellStyle = dataGridViewCellStyle4;
            this.PACKAGE.FilteringEnabled = false;
            this.PACKAGE.HeaderText = "状态";
            this.PACKAGE.Name = "PACKAGE";
            this.PACKAGE.ReadOnly = true;
            this.PACKAGE.Width = 113;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "ROWID";
            this.Column11.HeaderText = "序号";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // SORTNO
            // 
            this.SORTNO.DataPropertyName = "SORTNO";
            this.SORTNO.HeaderText = "流水号";
            this.SORTNO.Name = "SORTNO";
            this.SORTNO.ReadOnly = true;
            this.SORTNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SORTNO.Width = 70;
            // 
            // ORDERID
            // 
            this.ORDERID.DataPropertyName = "ORDERID";
            this.ORDERID.HeaderText = "订单号";
            this.ORDERID.Name = "ORDERID";
            this.ORDERID.ReadOnly = true;
            this.ORDERID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CHANNELNAME
            // 
            this.CHANNELNAME.DataPropertyName = "CHANNELNAME";
            this.CHANNELNAME.HeaderText = "货仓名称";
            this.CHANNELNAME.Name = "CHANNELNAME";
            this.CHANNELNAME.ReadOnly = true;
            this.CHANNELNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CHANNELNAME.Width = 80;
            // 
            // CHANNELTYPE
            // 
            this.CHANNELTYPE.DataPropertyName = "CHANNELTYPE";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CHANNELTYPE.DefaultCellStyle = dataGridViewCellStyle7;
            this.CHANNELTYPE.HeaderText = "货仓类型";
            this.CHANNELTYPE.Name = "CHANNELTYPE";
            this.CHANNELTYPE.ReadOnly = true;
            this.CHANNELTYPE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CHANNELTYPE.Width = 80;
            // 
            // CIGARETTECODE
            // 
            this.CIGARETTECODE.DataPropertyName = "ProductCode";
            this.CIGARETTECODE.HeaderText = "产品代码";
            this.CIGARETTECODE.Name = "CIGARETTECODE";
            this.CIGARETTECODE.ReadOnly = true;
            this.CIGARETTECODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CIGARETTENAME
            // 
            this.CIGARETTENAME.DataPropertyName = "ProductNAME";
            this.CIGARETTENAME.HeaderText = "产品名称";
            this.CIGARETTENAME.Name = "CIGARETTENAME";
            this.CIGARETTENAME.ReadOnly = true;
            this.CIGARETTENAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CIGARETTENAME.Width = 200;
            // 
            // QUANTITY
            // 
            this.QUANTITY.DataPropertyName = "QUANTITY";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.QUANTITY.DefaultCellStyle = dataGridViewCellStyle8;
            this.QUANTITY.HeaderText = "数量";
            this.QUANTITY.Name = "QUANTITY";
            this.QUANTITY.ReadOnly = true;
            this.QUANTITY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.QUANTITY.Width = 80;
            // 
            // CHANNELLINE
            // 
            this.CHANNELLINE.DataPropertyName = "ISABNORMITY";
            this.CHANNELLINE.HeaderText = "异形";
            this.CHANNELLINE.Name = "CHANNELLINE";
            this.CHANNELLINE.ReadOnly = true;
            this.CHANNELLINE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CHANNELLINE.Visible = false;
            // 
            // OrderQueryForm
            // 
            this.ClientSize = new System.Drawing.Size(1044, 458);
            this.Name = "OrderQueryForm";
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
        

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.DataGridView dgvMaster;
        private System.Windows.Forms.DataGridView dgvDetail;
        private System.Windows.Forms.BindingSource bsMaster;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnEnd;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn ORDERDATE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn BATCHNO;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn MAINSORTNO;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn MAINORDERID;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn ROUTECODE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn ROUTENAME;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CUSTOMERCODE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CUSTOMERNAME;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn MAINQUANTITY;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn QUANTITY1;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn STATUS;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn STATUS1;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn PACKAGE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn SORTNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDERID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHANNELNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHANNELTYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIGARETTECODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIGARETTENAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANTITY;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHANNELLINE;
    }
}
