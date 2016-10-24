namespace Sorting.Dispatching.View
{
    partial class ButtonArea
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ButtonArea));
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.pnlButton = new System.Windows.Forms.TableLayoutPanel();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnOperate = new System.Windows.Forms.Button();
            this.btnOptimize = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnUploadLogistics = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.pnlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "");
            this.imgList.Images.SetKeyName(1, "");
            this.imgList.Images.SetKeyName(2, "");
            this.imgList.Images.SetKeyName(3, "");
            this.imgList.Images.SetKeyName(4, "");
            this.imgList.Images.SetKeyName(5, "");
            this.imgList.Images.SetKeyName(6, "");
            this.imgList.Images.SetKeyName(7, "");
            this.imgList.Images.SetKeyName(8, "swap32.gif");
            this.imgList.Images.SetKeyName(9, "down32.gif");
            this.imgList.Images.SetKeyName(10, "setup32.gif");
            this.imgList.Images.SetKeyName(11, "stop.png");
            this.imgList.Images.SetKeyName(12, "down.png");
            this.imgList.Images.SetKeyName(13, "play.png");
            this.imgList.Images.SetKeyName(14, "up.png");
            this.imgList.Images.SetKeyName(15, "help.png");
            this.imgList.Images.SetKeyName(16, "chart.png");
            this.imgList.Images.SetKeyName(17, "process_accept.png");
            this.imgList.Images.SetKeyName(18, "application_add.png");
            this.imgList.Images.SetKeyName(19, "calculator_accept.png");
            this.imgList.Images.SetKeyName(20, "chart_accept.png");
            this.imgList.Images.SetKeyName(21, "remove.png");
            this.imgList.Images.SetKeyName(22, "1331.ico");
            this.imgList.Images.SetKeyName(23, "user_accept.png");
            this.imgList.Images.SetKeyName(24, "play2.png");
            // 
            // pnlButton
            // 
            this.pnlButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pnlButton.ColumnCount = 4;
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnlButton.Controls.Add(this.btnStart, 0, 1);
            this.pnlButton.Controls.Add(this.btnDownload, 0, 0);
            this.pnlButton.Controls.Add(this.btnStop, 1, 1);
            this.pnlButton.Controls.Add(this.btnOperate, 2, 1);
            this.pnlButton.Controls.Add(this.btnOptimize, 1, 0);
            this.pnlButton.Controls.Add(this.btnUpload, 2, 0);
            this.pnlButton.Controls.Add(this.btnUploadLogistics, 3, 0);
            this.pnlButton.Controls.Add(this.btnExit, 3, 1);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButton.Location = new System.Drawing.Point(0, 0);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.RowCount = 2;
            this.pnlButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlButton.Size = new System.Drawing.Size(727, 130);
            this.pnlButton.TabIndex = 0;
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.SystemColors.Control;
            this.btnDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDownload.Enabled = false;
            this.btnDownload.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDownload.Image = ((System.Drawing.Image)(resources.GetObject("btnDownload.Image")));
            this.btnDownload.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDownload.Location = new System.Drawing.Point(3, 3);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(175, 59);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "订单数据下载";
            this.btnDownload.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.SystemColors.Control;
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStop.Enabled = false;
            this.btnStop.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnStop.Location = new System.Drawing.Point(184, 68);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(175, 59);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "停止分拣";
            this.btnStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnOperate
            // 
            this.btnOperate.BackColor = System.Drawing.SystemColors.Control;
            this.btnOperate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOperate.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOperate.Image = ((System.Drawing.Image)(resources.GetObject("btnOperate.Image")));
            this.btnOperate.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOperate.Location = new System.Drawing.Point(365, 68);
            this.btnOperate.Name = "btnOperate";
            this.btnOperate.Size = new System.Drawing.Size(175, 59);
            this.btnOperate.TabIndex = 13;
            this.btnOperate.Text = "查询";
            this.btnOperate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOperate.UseVisualStyleBackColor = false;
            this.btnOperate.Click += new System.EventHandler(this.btnOperate_Click);
            // 
            // btnOptimize
            // 
            this.btnOptimize.BackColor = System.Drawing.SystemColors.Control;
            this.btnOptimize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOptimize.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOptimize.Image = ((System.Drawing.Image)(resources.GetObject("btnOptimize.Image")));
            this.btnOptimize.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOptimize.Location = new System.Drawing.Point(184, 3);
            this.btnOptimize.Name = "btnOptimize";
            this.btnOptimize.Size = new System.Drawing.Size(175, 59);
            this.btnOptimize.TabIndex = 16;
            this.btnOptimize.Text = "订单数据优化";
            this.btnOptimize.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOptimize.UseVisualStyleBackColor = false;
            this.btnOptimize.Click += new System.EventHandler(this.btnOptimize_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.BackColor = System.Drawing.SystemColors.Control;
            this.btnUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpload.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpload.Image = ((System.Drawing.Image)(resources.GetObject("btnUpload.Image")));
            this.btnUpload.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnUpload.Location = new System.Drawing.Point(365, 3);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(175, 59);
            this.btnUpload.TabIndex = 5;
            this.btnUpload.Text = "初始化分拣数据";
            this.btnUpload.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnUploadLogistics
            // 
            this.btnUploadLogistics.BackColor = System.Drawing.SystemColors.Control;
            this.btnUploadLogistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUploadLogistics.Enabled = false;
            this.btnUploadLogistics.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUploadLogistics.Image = ((System.Drawing.Image)(resources.GetObject("btnUploadLogistics.Image")));
            this.btnUploadLogistics.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnUploadLogistics.Location = new System.Drawing.Point(546, 3);
            this.btnUploadLogistics.Name = "btnUploadLogistics";
            this.btnUploadLogistics.Size = new System.Drawing.Size(178, 59);
            this.btnUploadLogistics.TabIndex = 17;
            this.btnUploadLogistics.Text = "上传物流系统";
            this.btnUploadLogistics.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUploadLogistics.UseVisualStyleBackColor = false;
            this.btnUploadLogistics.Click += new System.EventHandler(this.btnUploadLogistics_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(546, 68);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(178, 59);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.SystemColors.Control;
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStart.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
            this.btnStart.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnStart.Location = new System.Drawing.Point(3, 68);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(175, 59);
            this.btnStart.TabIndex = 18;
            this.btnStart.Text = "开始分拣";
            this.btnStart.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // ButtonArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlButton);
            this.Name = "ButtonArea";
            this.Size = new System.Drawing.Size(727, 130);
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.TableLayoutPanel pnlButton;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnOperate;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnOptimize;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnUploadLogistics;
        private System.Windows.Forms.Button btnStart;
    }
}
