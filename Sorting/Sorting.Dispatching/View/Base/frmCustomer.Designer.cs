namespace Sorting.Dispatching.View.Base
{
    partial class frmCustomer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCustomer));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bsMain = new System.Windows.Forms.BindingSource(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.Column8 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column9 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column2 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column4 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column10 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column1 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column11 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column12 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.pnlTool.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.btnExit);
            this.pnlTool.Controls.Add(this.btnDelete);
            this.pnlTool.Controls.Add(this.btnModify);
            this.pnlTool.Controls.Add(this.btnAdd);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Size = new System.Drawing.Size(706, 53);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvMain);
            this.pnlContent.Size = new System.Drawing.Size(706, 386);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(706, 439);
            this.pnlMain.ParentChanged += new System.EventHandler(this.pnlMain_ParentChanged);
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExit.Image = global::Sorting.Dispatching.Properties.Resources.close;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(192, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 51);
            this.btnExit.TabIndex = 56;
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
            this.btnDelete.Location = new System.Drawing.Point(144, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(48, 51);
            this.btnDelete.TabIndex = 55;
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
            this.btnModify.Location = new System.Drawing.Point(96, 0);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(48, 51);
            this.btnModify.TabIndex = 54;
            this.btnModify.Text = "修改";
            this.btnModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Visible = false;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAdd.Location = new System.Drawing.Point(48, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(48, 51);
            this.btnAdd.TabIndex = 53;
            this.btnAdd.Text = "新增";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
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
            this.Column8,
            this.Column9,
            this.Column2,
            this.Column4,
            this.Column10,
            this.Column1,
            this.Column11,
            this.Column12});
            this.dgvMain.DataSource = this.bsMain;
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.Location = new System.Drawing.Point(0, 0);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.RowHeadersWidth = 30;
            this.dgvMain.RowTemplate.Height = 23;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(706, 386);
            this.dgvMain.TabIndex = 3;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "CustomerCode";
            this.Column8.FilteringEnabled = false;
            this.Column8.HeaderText = "客户代码";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "CustomerName";
            this.Column9.FilteringEnabled = false;
            this.Column9.HeaderText = "客户名称";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column9.Width = 120;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "ROUTECODE";
            this.Column2.FilteringEnabled = false;
            this.Column2.HeaderText = "线路代码";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "ROUTENAME";
            this.Column4.FilteringEnabled = false;
            this.Column4.HeaderText = "线路名称";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.Width = 250;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "LICENSENO";
            this.Column10.FilteringEnabled = false;
            this.Column10.HeaderText = "专卖证号";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "SORTID";
            this.Column1.FilteringEnabled = false;
            this.Column1.HeaderText = "配送顺序";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.Width = 80;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "ADDRESS";
            this.Column11.FilteringEnabled = false;
            this.Column11.HeaderText = "送货地址";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "TELNO";
            this.Column12.FilteringEnabled = false;
            this.Column12.HeaderText = "联系电话";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // frmCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 439);
            this.Name = "frmCustomer";
            this.Text = "客户资料";
            this.pnlTool.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsMain;
        protected System.Windows.Forms.Button btnExit;
        protected System.Windows.Forms.Button btnDelete;
        protected System.Windows.Forms.Button btnModify;
        protected System.Windows.Forms.Button btnAdd;
        protected System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvMain;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column8;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column9;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column2;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column4;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column10;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column1;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column11;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column12;
    }
}