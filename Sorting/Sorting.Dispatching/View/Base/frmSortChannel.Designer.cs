namespace Sorting.Dispatching.View.Base
{
    partial class frmSortChannel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSortChannel));
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.bsMain = new System.Windows.Forms.BindingSource(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.btnParameter = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.Column2 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column4 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column3 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column7 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colChannelSpec = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colChannelGroup = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column5 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column1 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
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
            this.pnlTool.Controls.Add(this.btnParameter);
            this.pnlTool.Controls.Add(this.btnModify);
            this.pnlTool.Controls.Add(this.btnAdd);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Size = new System.Drawing.Size(698, 53);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvMain);
            this.pnlContent.Size = new System.Drawing.Size(698, 376);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(698, 429);
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
            this.Column3,
            this.Column7,
            this.colChannelSpec,
            this.colChannelGroup,
            this.Column5,
            this.Column1,
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
            this.dgvMain.Size = new System.Drawing.Size(698, 376);
            this.dgvMain.TabIndex = 4;
            this.dgvMain.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMain_CellFormatting);
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExit.Image = global::Sorting.Dispatching.Properties.Resources.close;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(192, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 51);
            this.btnExit.TabIndex = 61;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnParameter
            // 
            this.btnParameter.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnParameter.Image = global::Sorting.Dispatching.Properties.Resources.calculator;
            this.btnParameter.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnParameter.Location = new System.Drawing.Point(144, 0);
            this.btnParameter.Name = "btnParameter";
            this.btnParameter.Size = new System.Drawing.Size(48, 51);
            this.btnParameter.TabIndex = 60;
            this.btnParameter.Text = "设定";
            this.btnParameter.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnParameter.UseVisualStyleBackColor = true;
            this.btnParameter.Click += new System.EventHandler(this.btnParameter_Click);
            // 
            // btnModify
            // 
            this.btnModify.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnModify.Image = global::Sorting.Dispatching.Properties.Resources.document_edit;
            this.btnModify.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnModify.Location = new System.Drawing.Point(96, 0);
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
            this.btnAdd.Location = new System.Drawing.Point(48, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(48, 51);
            this.btnAdd.TabIndex = 58;
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
            this.btnRefresh.TabIndex = 57;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "CHANNELCODE";
            this.Column2.FilteringEnabled = false;
            this.Column2.HeaderText = "货仓代码";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "CHANNELNAME";
            this.Column4.FilteringEnabled = false;
            this.Column4.HeaderText = "货仓名称";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.Width = 150;
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
            // Column7
            // 
            this.Column7.DataPropertyName = "CHANNELTYPE";
            this.Column7.FilteringEnabled = false;
            this.Column7.HeaderText = "货仓类型";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colChannelSpec
            // 
            this.colChannelSpec.DataPropertyName = "ChannelGroup";
            this.colChannelSpec.FilteringEnabled = false;
            this.colChannelSpec.HeaderText = "货仓规格";
            this.colChannelSpec.Name = "colChannelSpec";
            this.colChannelSpec.ReadOnly = true;
            this.colChannelSpec.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colChannelGroup
            // 
            this.colChannelGroup.DataPropertyName = "GroupNo";
            this.colChannelGroup.FilteringEnabled = false;
            this.colChannelGroup.HeaderText = "货仓组别";
            this.colChannelGroup.Name = "colChannelGroup";
            this.colChannelGroup.ReadOnly = true;
            this.colChannelGroup.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "ProductCode";
            this.Column5.FilteringEnabled = false;
            this.Column5.HeaderText = "产品编码";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "ProductName";
            this.Column1.FilteringEnabled = false;
            this.Column1.HeaderText = "产品名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.Width = 120;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "STATUS";
            this.Column6.FilteringEnabled = false;
            this.Column6.HeaderText = "状态";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "CHANNELORDER";
            this.Column8.FilteringEnabled = false;
            this.Column8.HeaderText = "排列顺序";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // frmSortChannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 429);
            this.Name = "frmSortChannel";
            this.Text = "分拣货仓";
            this.pnlTool.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Button btnExit;
        protected System.Windows.Forms.Button btnParameter;
        protected System.Windows.Forms.Button btnModify;
        protected System.Windows.Forms.Button btnAdd;
        protected System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.BindingSource bsMain;
        private System.Windows.Forms.DataGridView dgvMain;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column2;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column4;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column3;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column7;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn colChannelSpec;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn colChannelGroup;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column5;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column1;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column6;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column8;
    }
}