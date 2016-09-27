namespace Sorting.Dispatching.View.Base
{
    partial class frmProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProduct));
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.bsMain = new System.Windows.Forms.BindingSource(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.Column2 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column4 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column7 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column1 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column11 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column8 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.pnlTool.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.btnExit);
            this.pnlTool.Controls.Add(this.btnDelete);
            this.pnlTool.Controls.Add(this.btnModify);
            this.pnlTool.Controls.Add(this.btnAdd);
            this.pnlTool.Controls.Add(this.btnQuery);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Size = new System.Drawing.Size(676, 53);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvMain);
            this.pnlContent.Size = new System.Drawing.Size(676, 386);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(676, 439);
            this.pnlMain.ParentChanged += new System.EventHandler(this.pnlMain_ParentChanged);
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
            this.Column2,
            this.Column4,
            this.Column12,
            this.Column9,
            this.Column14,
            this.Column3,
            this.Column7,
            this.Column1,
            this.Column13,
            this.Column10,
            this.Column11,
            this.Column5,
            this.Column6,
            this.Column8});
            this.dgvMain.DataSource = this.bsMain;
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.Location = new System.Drawing.Point(0, 0);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.RowHeadersWidth = 30;
            this.dgvMain.RowTemplate.Height = 23;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(676, 386);
            this.dgvMain.TabIndex = 3;
            this.dgvMain.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMain_CellFormatting);
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExit.Image = global::Sorting.Dispatching.Properties.Resources.close;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(240, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 51);
            this.btnExit.TabIndex = 61;
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
            this.btnDelete.Location = new System.Drawing.Point(192, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(48, 51);
            this.btnDelete.TabIndex = 60;
            this.btnDelete.Text = "删除";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Visible = false;
            // 
            // btnModify
            // 
            this.btnModify.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnModify.Image = global::Sorting.Dispatching.Properties.Resources.document_edit;
            this.btnModify.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnModify.Location = new System.Drawing.Point(144, 0);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(48, 51);
            this.btnModify.TabIndex = 59;
            this.btnModify.Text = "修改";
            this.btnModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAdd.Location = new System.Drawing.Point(96, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(48, 51);
            this.btnAdd.TabIndex = 58;
            this.btnAdd.Text = "新增";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click_1);
            // 
            // btnQuery
            // 
            this.btnQuery.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnQuery.Image = global::Sorting.Dispatching.Properties.Resources.zoom;
            this.btnQuery.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnQuery.Location = new System.Drawing.Point(48, 0);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(48, 51);
            this.btnQuery.TabIndex = 53;
            this.btnQuery.Text = "查询";
            this.btnQuery.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Image = global::Sorting.Dispatching.Properties.Resources.refresh;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(48, 51);
            this.btnRefresh.TabIndex = 52;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "ProductCode";
            this.Column2.FilteringEnabled = false;
            this.Column2.HeaderText = "产品编码";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.Width = 60;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "ProductName";
            this.Column4.FilteringEnabled = false;
            this.Column4.HeaderText = "产品名称";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.Width = 200;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "ShortName";
            this.Column12.HeaderText = "产品简称";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "FactoryID";
            this.Column9.HeaderText = "供应商编码";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "FactoryName";
            this.Column14.HeaderText = "供应商名称";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Width = 150;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "IsAbnormity";
            this.Column3.FilteringEnabled = false;
            this.Column3.HeaderText = "是否异型";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.Width = 60;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "Barcode";
            this.Column7.FilteringEnabled = false;
            this.Column7.HeaderText = "产品条码";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "BarcodePack";
            this.Column1.FilteringEnabled = false;
            this.Column1.HeaderText = "包装条码";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "Unit";
            this.Column13.HeaderText = "计量单位";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 70;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "Length";
            this.Column10.FilteringEnabled = false;
            this.Column10.HeaderText = "长";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column10.Width = 80;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "Width";
            this.Column11.FilteringEnabled = false;
            this.Column11.HeaderText = "宽";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column11.Width = 120;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Height";
            this.Column5.HeaderText = "高";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 80;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "Status";
            this.Column6.FilteringEnabled = false;
            this.Column6.HeaderText = "状态";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6.Width = 150;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "IsPick";
            this.Column8.FilteringEnabled = false;
            this.Column8.HeaderText = "是否分拣";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // frmProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 439);
            this.Name = "frmProduct";
            this.Text = "产品资料";
            this.pnlTool.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsMain;
        protected System.Windows.Forms.Button btnRefresh;
        protected System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.DataGridView dgvMain;
        protected System.Windows.Forms.Button btnExit;
        protected System.Windows.Forms.Button btnDelete;
        protected System.Windows.Forms.Button btnModify;
        protected System.Windows.Forms.Button btnAdd;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column2;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column3;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column7;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column10;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column6;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column8;
        
    }
}