namespace Sorting.Dispatching.View.Base
{
    partial class frmHandSortEdit
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnOrderId = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCigaretteName = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtOrderId = new System.Windows.Forms.TextBox();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.SORTNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIGARETTECODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIGARETTENAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANTITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtOrderDate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOrderId
            // 
            this.btnOrderId.Location = new System.Drawing.Point(514, 11);
            this.btnOrderId.Name = "btnOrderId";
            this.btnOrderId.Size = new System.Drawing.Size(31, 22);
            this.btnOrderId.TabIndex = 62;
            this.btnOrderId.Text = "...";
            this.btnOrderId.UseVisualStyleBackColor = true;
            this.btnOrderId.Click += new System.EventHandler(this.btnOrderId_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 61;
            this.label1.Text = "卷烟名称";
            // 
            // txtCigaretteName
            // 
            this.txtCigaretteName.Location = new System.Drawing.Point(102, 81);
            this.txtCigaretteName.MaxLength = 3;
            this.txtCigaretteName.Name = "txtCigaretteName";
            this.txtCigaretteName.ReadOnly = true;
            this.txtCigaretteName.Size = new System.Drawing.Size(110, 21);
            this.txtCigaretteName.TabIndex = 59;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(280, 368);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 58;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(132, 368);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 57;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtOrderId
            // 
            this.txtOrderId.Location = new System.Drawing.Point(365, 12);
            this.txtOrderId.MaxLength = 20;
            this.txtOrderId.Name = "txtOrderId";
            this.txtOrderId.ReadOnly = true;
            this.txtOrderId.Size = new System.Drawing.Size(143, 21);
            this.txtOrderId.TabIndex = 56;
            this.txtOrderId.TextChanged += new System.EventHandler(this.txtOrderId_TextChanged);
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(58, 12);
            this.txtBatchNo.MaxLength = 3;
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.ReadOnly = true;
            this.txtBatchNo.Size = new System.Drawing.Size(110, 21);
            this.txtBatchNo.TabIndex = 55;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(310, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(53, 12);
            this.lblName.TabIndex = 53;
            this.lblName.Text = "订单编号";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(14, 16);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(41, 12);
            this.lblCode.TabIndex = 52;
            this.lblCode.Text = "批次号";
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
            this.CIGARETTECODE,
            this.CIGARETTENAME,
            this.QUANTITY});
            this.dgvDetail.Location = new System.Drawing.Point(13, 39);
            this.dgvDetail.MultiSelect = false;
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.ReadOnly = true;
            this.dgvDetail.RowHeadersWidth = 30;
            this.dgvDetail.RowTemplate.Height = 23;
            this.dgvDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetail.Size = new System.Drawing.Size(530, 323);
            this.dgvDetail.TabIndex = 63;
            // 
            // SORTNO
            // 
            this.SORTNO.DataPropertyName = "ORDERDETAILID";
            this.SORTNO.HeaderText = "序号";
            this.SORTNO.Name = "SORTNO";
            this.SORTNO.ReadOnly = true;
            this.SORTNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SORTNO.Width = 40;
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
            // txtOrderDate
            // 
            this.txtOrderDate.Location = new System.Drawing.Point(222, 12);
            this.txtOrderDate.MaxLength = 3;
            this.txtOrderDate.Name = "txtOrderDate";
            this.txtOrderDate.ReadOnly = true;
            this.txtOrderDate.Size = new System.Drawing.Size(82, 21);
            this.txtOrderDate.TabIndex = 65;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(170, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 64;
            this.label2.Text = "订单日期";
            // 
            // frmHandSortEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 400);
            this.Controls.Add(this.txtOrderDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvDetail);
            this.Controls.Add(this.btnOrderId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCigaretteName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtOrderId);
            this.Controls.Add(this.txtBatchNo);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHandSortEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "手动分拣编辑";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOrderId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCigaretteName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtOrderId;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.DataGridView dgvDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn SORTNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIGARETTECODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIGARETTENAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANTITY;
        private System.Windows.Forms.TextBox txtOrderDate;
        private System.Windows.Forms.Label label2;
    }
}