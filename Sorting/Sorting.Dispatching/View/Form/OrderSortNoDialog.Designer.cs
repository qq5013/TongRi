namespace Sorting.Dispatching.View
{
    partial class OrderSortNoDialog
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSortNo = new System.Windows.Forms.TextBox();
            this.lblSortNo = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbChannelType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtSortNo
            // 
            this.txtSortNo.Location = new System.Drawing.Point(145, 12);
            this.txtSortNo.Name = "txtSortNo";
            this.txtSortNo.Size = new System.Drawing.Size(90, 21);
            this.txtSortNo.TabIndex = 1;
            this.txtSortNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSortNo_KeyPress);
            // 
            // lblSortNo
            // 
            this.lblSortNo.AutoSize = true;
            this.lblSortNo.Location = new System.Drawing.Point(32, 16);
            this.lblSortNo.Name = "lblSortNo";
            this.lblSortNo.Size = new System.Drawing.Size(113, 12);
            this.lblSortNo.TabIndex = 4;
            this.lblSortNo.Text = "请输入分拣流水号：";
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(50, 158);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(146, 158);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(30, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 83);
            this.label1.TabIndex = 5;
            this.label1.Text = "注：此功能会将大于输入之流水号的订单且为所选分拣机类型的改为未下达状态，分拣机类型为空表示通道机与立式机同时修正";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "请选择分拣机类型：";
            // 
            // cbChannelType
            // 
            this.cbChannelType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChannelType.FormattingEnabled = true;
            this.cbChannelType.Items.AddRange(new object[] {
            "",
            "通道机",
            "立式机"});
            this.cbChannelType.Location = new System.Drawing.Point(145, 41);
            this.cbChannelType.Name = "cbChannelType";
            this.cbChannelType.Size = new System.Drawing.Size(90, 20);
            this.cbChannelType.TabIndex = 8;
            // 
            // OrderSortNoDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(284, 204);
            this.Controls.Add(this.cbChannelType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtSortNo);
            this.Controls.Add(this.lblSortNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderSortNoDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "分拣流水号";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSortNo;
        private System.Windows.Forms.Label lblSortNo;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbChannelType;
    }
}