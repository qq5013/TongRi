namespace Sorting.Dispatching.View.Base
{
    partial class frmSupplyChannelEdit
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
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCigaretteCode = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.txtCigaretteName = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtCigaretteCode = new System.Windows.Forms.TextBox();
            this.txtChannelCode = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtChannelName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtChannelOrder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(200, 67);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(40, 22);
            this.btnClear.TabIndex = 63;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCigaretteCode
            // 
            this.btnCigaretteCode.Location = new System.Drawing.Point(162, 67);
            this.btnCigaretteCode.Name = "btnCigaretteCode";
            this.btnCigaretteCode.Size = new System.Drawing.Size(31, 22);
            this.btnCigaretteCode.TabIndex = 62;
            this.btnCigaretteCode.Text = "...";
            this.btnCigaretteCode.UseVisualStyleBackColor = true;
            this.btnCigaretteCode.Click += new System.EventHandler(this.btnCigaretteCode_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 61;
            this.label1.Text = "卷烟名称";
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "禁用",
            "启用"});
            this.cmbStatus.Location = new System.Drawing.Point(82, 153);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(77, 20);
            this.cmbStatus.TabIndex = 60;
            // 
            // txtCigaretteName
            // 
            this.txtCigaretteName.Location = new System.Drawing.Point(83, 100);
            this.txtCigaretteName.MaxLength = 3;
            this.txtCigaretteName.Name = "txtCigaretteName";
            this.txtCigaretteName.ReadOnly = true;
            this.txtCigaretteName.Size = new System.Drawing.Size(157, 21);
            this.txtCigaretteName.TabIndex = 59;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(140, 191);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 58;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(47, 191);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 57;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtCigaretteCode
            // 
            this.txtCigaretteCode.Location = new System.Drawing.Point(82, 67);
            this.txtCigaretteCode.MaxLength = 20;
            this.txtCigaretteCode.Name = "txtCigaretteCode";
            this.txtCigaretteCode.ReadOnly = true;
            this.txtCigaretteCode.Size = new System.Drawing.Size(77, 21);
            this.txtCigaretteCode.TabIndex = 56;
            // 
            // txtChannelCode
            // 
            this.txtChannelCode.Location = new System.Drawing.Point(82, 14);
            this.txtChannelCode.MaxLength = 3;
            this.txtChannelCode.Name = "txtChannelCode";
            this.txtChannelCode.ReadOnly = true;
            this.txtChannelCode.Size = new System.Drawing.Size(77, 21);
            this.txtChannelCode.TabIndex = 55;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(24, 154);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(53, 12);
            this.lblType.TabIndex = 54;
            this.lblType.Text = "是否启用";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(24, 73);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(53, 12);
            this.lblName.TabIndex = 53;
            this.lblName.Text = "卷烟代码";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(24, 20);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(53, 12);
            this.lblCode.TabIndex = 52;
            this.lblCode.Text = "烟道代码";
            // 
            // txtChannelName
            // 
            this.txtChannelName.Location = new System.Drawing.Point(82, 41);
            this.txtChannelName.MaxLength = 3;
            this.txtChannelName.Name = "txtChannelName";
            this.txtChannelName.ReadOnly = true;
            this.txtChannelName.Size = new System.Drawing.Size(77, 21);
            this.txtChannelName.TabIndex = 65;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 64;
            this.label2.Text = "烟道名称";
            // 
            // txtChannelOrder
            // 
            this.txtChannelOrder.Location = new System.Drawing.Point(83, 126);
            this.txtChannelOrder.MaxLength = 3;
            this.txtChannelOrder.Name = "txtChannelOrder";
            this.txtChannelOrder.Size = new System.Drawing.Size(77, 21);
            this.txtChannelOrder.TabIndex = 67;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 66;
            this.label3.Text = "排烟顺序";
            // 
            // frmSupplyChannelEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 229);
            this.Controls.Add(this.txtChannelOrder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtChannelName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCigaretteCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.txtCigaretteName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtCigaretteCode);
            this.Controls.Add(this.txtChannelCode);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSupplyChannelEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "补货烟道编辑";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnCigaretteCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.TextBox txtCigaretteName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtCigaretteCode;
        private System.Windows.Forms.TextBox txtChannelCode;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtChannelName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtChannelOrder;
        private System.Windows.Forms.Label label3;
    }
}