namespace Sorting.Dispatching.View.Base
{
    partial class frmProductQuery
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
            this.cmbIsAbnormity = new System.Windows.Forms.ComboBox();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtCigaretteName = new System.Windows.Forms.TextBox();
            this.txtCigaretteCode = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 49;
            this.label1.Text = "异 形 烟";
            // 
            // cmbIsAbnormity
            // 
            this.cmbIsAbnormity.FormattingEnabled = true;
            this.cmbIsAbnormity.Items.AddRange(new object[] {
            "",
            "否",
            "是"});
            this.cmbIsAbnormity.Location = new System.Drawing.Point(83, 75);
            this.cmbIsAbnormity.Name = "cmbIsAbnormity";
            this.cmbIsAbnormity.Size = new System.Drawing.Size(113, 20);
            this.cmbIsAbnormity.TabIndex = 48;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(83, 108);
            this.txtBarcode.MaxLength = 30;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(113, 21);
            this.txtBarcode.TabIndex = 47;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(116, 153);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 46;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(23, 153);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 45;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtCigaretteName
            // 
            this.txtCigaretteName.Location = new System.Drawing.Point(82, 47);
            this.txtCigaretteName.MaxLength = 20;
            this.txtCigaretteName.Name = "txtCigaretteName";
            this.txtCigaretteName.Size = new System.Drawing.Size(114, 21);
            this.txtCigaretteName.TabIndex = 44;
            // 
            // txtCigaretteCode
            // 
            this.txtCigaretteCode.Location = new System.Drawing.Point(82, 15);
            this.txtCigaretteCode.MaxLength = 20;
            this.txtCigaretteCode.Name = "txtCigaretteCode";
            this.txtCigaretteCode.Size = new System.Drawing.Size(114, 21);
            this.txtCigaretteCode.TabIndex = 43;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(24, 112);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(53, 12);
            this.lblType.TabIndex = 42;
            this.lblType.Text = "卷烟条码";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(24, 53);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(53, 12);
            this.lblName.TabIndex = 41;
            this.lblName.Text = "卷烟名称";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(24, 21);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(53, 12);
            this.lblCode.TabIndex = 40;
            this.lblCode.Text = "卷烟代码";
            // 
            // frmCigaretteQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 197);
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
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCigaretteQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "卷烟品牌查询";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbIsAbnormity;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtCigaretteName;
        private System.Windows.Forms.TextBox txtCigaretteCode;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCode;
    }
}