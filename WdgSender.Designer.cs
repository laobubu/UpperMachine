namespace UpperMachine
{
    partial class WdgSender
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WdgSender));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnEmpty = new System.Windows.Forms.Button();
            this.txtRecv = new System.Windows.Forms.TextBox();
            this.rbRecvText = new System.Windows.Forms.RadioButton();
            this.rbRecvHex = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.rbSendText = new System.Windows.Forms.RadioButton();
            this.rbSendHex = new System.Windows.Forms.RadioButton();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.cbSendConfig = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(431, 327);
            this.splitContainer1.SplitterDistance = 163;
            this.splitContainer1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.btnEmpty, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtRecv, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.rbRecvText, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.rbRecvHex, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnSave, 1, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(431, 163);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // btnEmpty
            // 
            this.btnEmpty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEmpty.Location = new System.Drawing.Point(283, 51);
            this.btnEmpty.Name = "btnEmpty";
            this.btnEmpty.Size = new System.Drawing.Size(145, 30);
            this.btnEmpty.TabIndex = 3;
            this.btnEmpty.Text = "清空";
            this.btnEmpty.UseVisualStyleBackColor = true;
            this.btnEmpty.Click += new System.EventHandler(this.btnEmpty_Click);
            // 
            // txtRecv
            // 
            this.txtRecv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecv.Location = new System.Drawing.Point(3, 3);
            this.txtRecv.Multiline = true;
            this.txtRecv.Name = "txtRecv";
            this.tableLayoutPanel2.SetRowSpan(this.txtRecv, 5);
            this.txtRecv.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRecv.Size = new System.Drawing.Size(274, 157);
            this.txtRecv.TabIndex = 2;
            this.txtRecv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRecv_KeyDown);
            // 
            // rbRecvText
            // 
            this.rbRecvText.AutoSize = true;
            this.rbRecvText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbRecvText.Location = new System.Drawing.Point(283, 3);
            this.rbRecvText.Name = "rbRecvText";
            this.rbRecvText.Size = new System.Drawing.Size(145, 18);
            this.rbRecvText.TabIndex = 4;
            this.rbRecvText.Text = "文本";
            this.rbRecvText.UseVisualStyleBackColor = true;
            // 
            // rbRecvHex
            // 
            this.rbRecvHex.AutoSize = true;
            this.rbRecvHex.Checked = true;
            this.rbRecvHex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbRecvHex.Location = new System.Drawing.Point(283, 27);
            this.rbRecvHex.Name = "rbRecvHex";
            this.rbRecvHex.Size = new System.Drawing.Size(145, 18);
            this.rbRecvHex.TabIndex = 5;
            this.rbRecvHex.TabStop = true;
            this.rbRecvHex.Text = "十六进制";
            this.rbRecvHex.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.btnSend, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtSend, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rbSendText, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.rbSendHex, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSendFile, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbSendConfig, 1, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(431, 160);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // btnSend
            // 
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSend.Location = new System.Drawing.Point(283, 51);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(145, 30);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtSend
            // 
            this.txtSend.AcceptsTab = true;
            this.txtSend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSend.Location = new System.Drawing.Point(3, 3);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.tableLayoutPanel1.SetRowSpan(this.txtSend, 6);
            this.txtSend.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSend.Size = new System.Drawing.Size(274, 154);
            this.txtSend.TabIndex = 2;
            this.txtSend.WordWrap = false;
            this.txtSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSend_KeyDown);
            // 
            // rbSendText
            // 
            this.rbSendText.AutoSize = true;
            this.rbSendText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbSendText.Location = new System.Drawing.Point(283, 3);
            this.rbSendText.Name = "rbSendText";
            this.rbSendText.Size = new System.Drawing.Size(145, 18);
            this.rbSendText.TabIndex = 4;
            this.rbSendText.Text = "文本";
            this.rbSendText.UseVisualStyleBackColor = true;
            // 
            // rbSendHex
            // 
            this.rbSendHex.AutoSize = true;
            this.rbSendHex.Checked = true;
            this.rbSendHex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbSendHex.Location = new System.Drawing.Point(283, 27);
            this.rbSendHex.Name = "rbSendHex";
            this.rbSendHex.Size = new System.Drawing.Size(145, 18);
            this.rbSendHex.TabIndex = 5;
            this.rbSendHex.TabStop = true;
            this.rbSendHex.Text = "十六进制";
            this.rbSendHex.UseVisualStyleBackColor = true;
            // 
            // btnSendFile
            // 
            this.btnSendFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSendFile.Location = new System.Drawing.Point(283, 87);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(145, 30);
            this.btnSendFile.TabIndex = 6;
            this.btnSendFile.Text = "发送文件";
            this.btnSendFile.UseVisualStyleBackColor = true;
            this.btnSendFile.Click += new System.EventHandler(this.btnSendFile_Click);
            // 
            // cbSendConfig
            // 
            this.cbSendConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbSendConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSendConfig.FormattingEnabled = true;
            this.cbSendConfig.Items.AddRange(new object[] {
            "不使用发送快捷键",
            "按下 Enter 发送",
            "用 Ctrl+Enter 发送"});
            this.cbSendConfig.Location = new System.Drawing.Point(283, 137);
            this.cbSendConfig.Name = "cbSendConfig";
            this.cbSendConfig.Size = new System.Drawing.Size(145, 22);
            this.cbSendConfig.TabIndex = 7;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(283, 87);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(145, 30);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "保存为文件";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // WdgSender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 327);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ID = "WidgetCtrlPanel";
            this.Name = "WdgSender";
            this.Text = "数据控制台";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WdgSender_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.RadioButton rbSendText;
        private System.Windows.Forms.RadioButton rbSendHex;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnEmpty;
        private System.Windows.Forms.TextBox txtRecv;
        private System.Windows.Forms.RadioButton rbRecvText;
        private System.Windows.Forms.RadioButton rbRecvHex;
        private System.Windows.Forms.Button btnSendFile;
        private System.Windows.Forms.ComboBox cbSendConfig;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnSave;
    }
}