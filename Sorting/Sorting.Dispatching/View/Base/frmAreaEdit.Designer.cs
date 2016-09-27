namespace Sorting.Dispatching.View.Base
{
    partial class frmAreaEdit
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtAreaName = new System.Windows.Forms.TextBox();
            this.txtAreaCode = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtSortId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(225, 132);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 28;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(132, 132);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 27;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtAreaName
            // 
            this.txtAreaName.Location = new System.Drawing.Point(76, 42);
            this.txtAreaName.MaxLength = 20;
            this.txtAreaName.Name = "txtAreaName";
            this.txtAreaName.ReadOnly = true;
            this.txtAreaName.Size = new System.Drawing.Size(296, 21);
            this.txtAreaName.TabIndex = 25;
            // 
            // txtAreaCode
            // 
            this.txtAreaCode.Location = new System.Drawing.Point(76, 10);
            this.txtAreaCode.MaxLength = 3;
            this.txtAreaCode.Name = "txtAreaCode";
            this.txtAreaCode.ReadOnly = true;
            this.txtAreaCode.Size = new System.Drawing.Size(92, 21);
            this.txtAreaCode.TabIndex = 24;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(18, 79);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(53, 12);
            this.lblType.TabIndex = 19;
            this.lblType.Text = "配送顺序";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(18, 48);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(53, 12);
            this.lblName.TabIndex = 18;
            this.lblName.Text = "区域名称";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(18, 16);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(53, 12);
            this.lblCode.TabIndex = 17;
            this.lblCode.Text = "区域编码";
            // 
            // txtSortId
            // 
            this.txtSortId.Location = new System.Drawing.Point(77, 75);
            this.txtSortId.MaxLength = 3;
            this.txtSortId.Name = "txtSortId";
            this.txtSortId.Size = new System.Drawing.Size(92, 21);
            this.txtSortId.TabIndex = 29;
            // 
            // frmAreaEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 173);
            this.Controls.Add(this.txtSortId);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtAreaName);
            this.Controls.Add(this.txtAreaCode);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAreaEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "区域资料编辑";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtAreaName;
        private System.Windows.Forms.TextBox txtAreaCode;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtSortId;
    }
}