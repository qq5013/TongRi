namespace Sorting.Dispatching.View.Dialog
{
    partial class OrderBatchEdit
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
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.ckbDownload = new System.Windows.Forms.CheckBox();
            this.ckbValid = new System.Windows.Forms.CheckBox();
            this.ckbUploadNo1 = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtOrderDate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "订单批次：";
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(97, 46);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.ReadOnly = true;
            this.txtBatchNo.Size = new System.Drawing.Size(160, 21);
            this.txtBatchNo.TabIndex = 1;
            // 
            // ckbDownload
            // 
            this.ckbDownload.AutoSize = true;
            this.ckbDownload.Location = new System.Drawing.Point(97, 76);
            this.ckbDownload.Name = "ckbDownload";
            this.ckbDownload.Size = new System.Drawing.Size(48, 16);
            this.ckbDownload.TabIndex = 2;
            this.ckbDownload.Text = "下载";
            this.ckbDownload.UseVisualStyleBackColor = true;
            // 
            // ckbValid
            // 
            this.ckbValid.AutoSize = true;
            this.ckbValid.Location = new System.Drawing.Point(155, 76);
            this.ckbValid.Name = "ckbValid";
            this.ckbValid.Size = new System.Drawing.Size(48, 16);
            this.ckbValid.TabIndex = 3;
            this.ckbValid.Text = "优化";
            this.ckbValid.UseVisualStyleBackColor = true;
            // 
            // ckbUploadNo1
            // 
            this.ckbUploadNo1.AutoSize = true;
            this.ckbUploadNo1.Location = new System.Drawing.Point(97, 107);
            this.ckbUploadNo1.Name = "ckbUploadNo1";
            this.ckbUploadNo1.Size = new System.Drawing.Size(96, 16);
            this.ckbUploadNo1.TabIndex = 4;
            this.ckbUploadNo1.Text = "上传一号工程";
            this.ckbUploadNo1.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(155, 142);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(74, 142);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtOrderDate
            // 
            this.txtOrderDate.Location = new System.Drawing.Point(97, 16);
            this.txtOrderDate.Name = "txtOrderDate";
            this.txtOrderDate.ReadOnly = true;
            this.txtOrderDate.Size = new System.Drawing.Size(160, 21);
            this.txtOrderDate.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "订单日期：";
            // 
            // OrderBatchEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 188);
            this.Controls.Add(this.txtOrderDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.ckbUploadNo1);
            this.Controls.Add(this.ckbValid);
            this.Controls.Add(this.ckbDownload);
            this.Controls.Add(this.txtBatchNo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderBatchEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "订单批次修改";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.CheckBox ckbDownload;
        private System.Windows.Forms.CheckBox ckbValid;
        private System.Windows.Forms.CheckBox ckbUploadNo1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtOrderDate;
        private System.Windows.Forms.Label label2;
    }
}