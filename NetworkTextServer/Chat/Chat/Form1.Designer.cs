namespace Chat
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.ButtonInit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxUsers = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ip";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(139, 23);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(100, 25);
            this.textBoxIP.TabIndex = 1;
            this.textBoxIP.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "端口号";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(139, 91);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(100, 25);
            this.textBoxPort.TabIndex = 3;
            this.textBoxPort.Text = "8888";
            // 
            // ButtonInit
            // 
            this.ButtonInit.Location = new System.Drawing.Point(31, 149);
            this.ButtonInit.Name = "ButtonInit";
            this.ButtonInit.Size = new System.Drawing.Size(116, 23);
            this.ButtonInit.TabIndex = 4;
            this.ButtonInit.Text = "启动服务器";
            this.ButtonInit.UseVisualStyleBackColor = true;
            this.ButtonInit.Click += new System.EventHandler(this.ButtonInit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 253);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "当前连接用户：";
            // 
            // listBoxUsers
            // 
            this.listBoxUsers.FormattingEnabled = true;
            this.listBoxUsers.ItemHeight = 15;
            this.listBoxUsers.Location = new System.Drawing.Point(34, 290);
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.Size = new System.Drawing.Size(205, 244);
            this.listBoxUsers.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 621);
            this.Controls.Add(this.listBoxUsers);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ButtonInit);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Button ButtonInit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxUsers;
    }
}

