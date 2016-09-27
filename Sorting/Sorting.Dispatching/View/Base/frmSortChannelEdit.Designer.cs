namespace Sorting.Dispatching.View.Base
{
    partial class frmSortChannelEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.txtChannelCode = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.btnProductCode = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtChannelOrder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtChannelName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 49;
            this.label1.Text = "产品名称";
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "禁用",
            "启用"});
            this.cmbStatus.Location = new System.Drawing.Point(89, 152);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(77, 20);
            this.cmbStatus.TabIndex = 48;
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(90, 96);
            this.txtProductName.MaxLength = 3;
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.ReadOnly = true;
            this.txtProductName.Size = new System.Drawing.Size(157, 21);
            this.txtProductName.TabIndex = 47;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(141, 193);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 46;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(48, 193);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 45;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(89, 68);
            this.txtProductCode.MaxLength = 20;
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(77, 21);
            this.txtProductCode.TabIndex = 44;
            // 
            // txtChannelCode
            // 
            this.txtChannelCode.Location = new System.Drawing.Point(89, 15);
            this.txtChannelCode.MaxLength = 3;
            this.txtChannelCode.Name = "txtChannelCode";
            this.txtChannelCode.ReadOnly = true;
            this.txtChannelCode.Size = new System.Drawing.Size(77, 21);
            this.txtChannelCode.TabIndex = 43;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(31, 156);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(53, 12);
            this.lblType.TabIndex = 42;
            this.lblType.Text = "是否启用";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(31, 72);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(53, 12);
            this.lblName.TabIndex = 41;
            this.lblName.Text = "产品编码";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(31, 19);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(53, 12);
            this.lblCode.TabIndex = 40;
            this.lblCode.Text = "货仓代码";
            // 
            // btnProductCode
            // 
            this.btnProductCode.Location = new System.Drawing.Point(169, 67);
            this.btnProductCode.Name = "btnProductCode";
            this.btnProductCode.Size = new System.Drawing.Size(31, 22);
            this.btnProductCode.TabIndex = 50;
            this.btnProductCode.Text = "...";
            this.btnProductCode.UseVisualStyleBackColor = true;
            this.btnProductCode.Click += new System.EventHandler(this.btnProductCode_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(207, 67);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(40, 22);
            this.btnClear.TabIndex = 51;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtChannelOrder
            // 
            this.txtChannelOrder.Location = new System.Drawing.Point(89, 124);
            this.txtChannelOrder.MaxLength = 3;
            this.txtChannelOrder.Name = "txtChannelOrder";
            this.txtChannelOrder.Size = new System.Drawing.Size(77, 21);
            this.txtChannelOrder.TabIndex = 53;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 52;
            this.label2.Text = "排烟顺序";
            // 
            // txtChannelName
            // 
            this.txtChannelName.Location = new System.Drawing.Point(89, 41);
            this.txtChannelName.MaxLength = 3;
            this.txtChannelName.Name = "txtChannelName";
            this.txtChannelName.ReadOnly = true;
            this.txtChannelName.Size = new System.Drawing.Size(77, 21);
            this.txtChannelName.TabIndex = 55;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 54;
            this.label3.Text = "货仓名称";
            // 
            // frmSortChannelEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 228);
            this.Controls.Add(this.txtChannelName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtChannelOrder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnProductCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtProductCode);
            this.Controls.Add(this.txtChannelCode);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSortChannelEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "分拣货仓编辑";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.TextBox txtChannelCode;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Button btnProductCode;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtChannelOrder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtChannelName;
        private System.Windows.Forms.Label label3;
    }
}