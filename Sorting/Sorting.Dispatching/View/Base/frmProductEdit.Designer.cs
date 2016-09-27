namespace Sorting.Dispatching.View.Base
{
    partial class frmProductEdit
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
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtCigaretteName = new System.Windows.Forms.TextBox();
            this.txtCigaretteCode = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.cmbIsAbnormity = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtShowName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPackageNum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(72, 137);
            this.txtBarcode.MaxLength = 3;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(92, 21);
            this.txtBarcode.TabIndex = 37;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(217, 207);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 36;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(124, 207);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 35;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtCigaretteName
            // 
            this.txtCigaretteName.Location = new System.Drawing.Point(71, 44);
            this.txtCigaretteName.MaxLength = 20;
            this.txtCigaretteName.Name = "txtCigaretteName";
            this.txtCigaretteName.ReadOnly = true;
            this.txtCigaretteName.Size = new System.Drawing.Size(296, 21);
            this.txtCigaretteName.TabIndex = 34;
            // 
            // txtCigaretteCode
            // 
            this.txtCigaretteCode.Location = new System.Drawing.Point(71, 12);
            this.txtCigaretteCode.MaxLength = 3;
            this.txtCigaretteCode.Name = "txtCigaretteCode";
            this.txtCigaretteCode.ReadOnly = true;
            this.txtCigaretteCode.Size = new System.Drawing.Size(92, 21);
            this.txtCigaretteCode.TabIndex = 33;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(13, 141);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(53, 12);
            this.lblType.TabIndex = 32;
            this.lblType.Text = "卷烟条码";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(13, 50);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(53, 12);
            this.lblName.TabIndex = 31;
            this.lblName.Text = "卷烟名称";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(13, 18);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(53, 12);
            this.lblCode.TabIndex = 30;
            this.lblCode.Text = "卷烟代码";
            // 
            // cmbIsAbnormity
            // 
            this.cmbIsAbnormity.FormattingEnabled = true;
            this.cmbIsAbnormity.Items.AddRange(new object[] {
            "否",
            "是"});
            this.cmbIsAbnormity.Location = new System.Drawing.Point(72, 104);
            this.cmbIsAbnormity.Name = "cmbIsAbnormity";
            this.cmbIsAbnormity.Size = new System.Drawing.Size(91, 20);
            this.cmbIsAbnormity.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 39;
            this.label1.Text = "异 形 烟";
            // 
            // txtShowName
            // 
            this.txtShowName.Location = new System.Drawing.Point(72, 73);
            this.txtShowName.MaxLength = 10;
            this.txtShowName.Name = "txtShowName";
            this.txtShowName.Size = new System.Drawing.Size(92, 21);
            this.txtShowName.TabIndex = 41;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 40;
            this.label2.Text = "LED显示";
            // 
            // txtPackageNum
            // 
            this.txtPackageNum.Location = new System.Drawing.Point(72, 164);
            this.txtPackageNum.MaxLength = 3;
            this.txtPackageNum.Name = "txtPackageNum";
            this.txtPackageNum.Size = new System.Drawing.Size(92, 21);
            this.txtPackageNum.TabIndex = 43;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 42;
            this.label3.Text = "每件条数";
            // 
            // frmCigaretteEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 242);
            this.Controls.Add(this.txtPackageNum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtShowName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbIsAbnormity);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtCigaretteName);
            this.Controls.Add(this.txtCigaretteCode);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCigaretteEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "卷烟品牌编辑";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtCigaretteName;
        private System.Windows.Forms.TextBox txtCigaretteCode;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.ComboBox cmbIsAbnormity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtShowName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPackageNum;
        private System.Windows.Forms.Label label3;
    }
}