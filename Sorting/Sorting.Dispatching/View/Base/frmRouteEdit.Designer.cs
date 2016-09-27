namespace Sorting.Dispatching.View.Base
{
    partial class frmRouteEdit
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
            this.txtSortId = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtRouteName = new System.Windows.Forms.TextBox();
            this.txtRouteCode = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbIsSort = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtSortId
            // 
            this.txtSortId.Location = new System.Drawing.Point(72, 84);
            this.txtSortId.MaxLength = 3;
            this.txtSortId.Name = "txtSortId";
            this.txtSortId.Size = new System.Drawing.Size(92, 21);
            this.txtSortId.TabIndex = 37;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(219, 141);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 36;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(126, 141);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 35;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtRouteName
            // 
            this.txtRouteName.Location = new System.Drawing.Point(72, 51);
            this.txtRouteName.MaxLength = 20;
            this.txtRouteName.Name = "txtRouteName";
            this.txtRouteName.ReadOnly = true;
            this.txtRouteName.Size = new System.Drawing.Size(296, 21);
            this.txtRouteName.TabIndex = 34;
            // 
            // txtRouteCode
            // 
            this.txtRouteCode.Location = new System.Drawing.Point(72, 19);
            this.txtRouteCode.MaxLength = 3;
            this.txtRouteCode.Name = "txtRouteCode";
            this.txtRouteCode.ReadOnly = true;
            this.txtRouteCode.Size = new System.Drawing.Size(92, 21);
            this.txtRouteCode.TabIndex = 33;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(12, 88);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(53, 12);
            this.lblType.TabIndex = 32;
            this.lblType.Text = "配送顺序";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 57);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(53, 12);
            this.lblName.TabIndex = 31;
            this.lblName.Text = "线路名称";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(12, 25);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(53, 12);
            this.lblCode.TabIndex = 30;
            this.lblCode.Text = "线路代码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 38;
            this.label1.Text = "是否分拣";
            // 
            // cmbIsSort
            // 
            this.cmbIsSort.FormattingEnabled = true;
            this.cmbIsSort.Items.AddRange(new object[] {
            "否",
            "是"});
            this.cmbIsSort.Location = new System.Drawing.Point(72, 114);
            this.cmbIsSort.Name = "cmbIsSort";
            this.cmbIsSort.Size = new System.Drawing.Size(91, 20);
            this.cmbIsSort.TabIndex = 39;
            // 
            // frmRouteEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 178);
            this.Controls.Add(this.cmbIsSort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSortId);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtRouteName);
            this.Controls.Add(this.txtRouteCode);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRouteEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配送线路编辑";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSortId;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtRouteName;
        private System.Windows.Forms.TextBox txtRouteCode;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbIsSort;
    }
}