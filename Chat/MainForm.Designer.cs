namespace Chat
{
    partial class MainForm
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
            this.lblServerPort = new System.Windows.Forms.Label();
            this.btnServerStart = new System.Windows.Forms.Button();
            this.btnClientConnect = new System.Windows.Forms.Button();
            this.txtClientNickname = new System.Windows.Forms.TextBox();
            this.txtClientIP = new System.Windows.Forms.TextBox();
            this.lblClientNickname = new System.Windows.Forms.Label();
            this.lblClientPort = new System.Windows.Forms.Label();
            this.lblClientIP = new System.Windows.Forms.Label();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpClient = new System.Windows.Forms.TabPage();
            this.txtClientMessageTo = new System.Windows.Forms.TextBox();
            this.btnClientCommands = new System.Windows.Forms.Button();
            this.txtClientPassword = new System.Windows.Forms.TextBox();
            this.lblClientPassword = new System.Windows.Forms.Label();
            this.nudClientPort = new System.Windows.Forms.NumericUpDown();
            this.pClientChat = new System.Windows.Forms.Panel();
            this.rtbClientChat = new System.Windows.Forms.RichTextBox();
            this.lvClientUsers = new System.Windows.Forms.ListView();
            this.chClientNickname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnClientSend = new System.Windows.Forms.Button();
            this.txtClientMessage = new System.Windows.Forms.TextBox();
            this.tpServer = new System.Windows.Forms.TabPage();
            this.txtServerPassword = new System.Windows.Forms.TextBox();
            this.lblServerPassword = new System.Windows.Forms.Label();
            this.btnServerSend = new System.Windows.Forms.Button();
            this.txtServerMessage = new System.Windows.Forms.TextBox();
            this.nudServerPort = new System.Windows.Forms.NumericUpDown();
            this.pServerConsole = new System.Windows.Forms.Panel();
            this.rtbServerConsole = new System.Windows.Forms.RichTextBox();
            this.lvServerUsers = new System.Windows.Forms.ListView();
            this.chServerNickname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsClientCommands = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiClientTextColor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClientSendPing = new System.Windows.Forms.ToolStripMenuItem();
            this.tcMain.SuspendLayout();
            this.tpClient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudClientPort)).BeginInit();
            this.pClientChat.SuspendLayout();
            this.tpServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudServerPort)).BeginInit();
            this.pServerConsole.SuspendLayout();
            this.cmsClientCommands.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblServerPort
            // 
            this.lblServerPort.AutoSize = true;
            this.lblServerPort.Location = new System.Drawing.Point(8, 16);
            this.lblServerPort.Name = "lblServerPort";
            this.lblServerPort.Size = new System.Drawing.Size(29, 13);
            this.lblServerPort.TabIndex = 2;
            this.lblServerPort.Text = "Port:";
            // 
            // btnServerStart
            // 
            this.btnServerStart.Location = new System.Drawing.Point(296, 11);
            this.btnServerStart.Name = "btnServerStart";
            this.btnServerStart.Size = new System.Drawing.Size(96, 23);
            this.btnServerStart.TabIndex = 4;
            this.btnServerStart.Text = "Start server";
            this.btnServerStart.UseVisualStyleBackColor = true;
            this.btnServerStart.Click += new System.EventHandler(this.btnServerStart_Click);
            // 
            // btnClientConnect
            // 
            this.btnClientConnect.Location = new System.Drawing.Point(624, 11);
            this.btnClientConnect.Name = "btnClientConnect";
            this.btnClientConnect.Size = new System.Drawing.Size(80, 23);
            this.btnClientConnect.TabIndex = 6;
            this.btnClientConnect.Text = "Connect";
            this.btnClientConnect.UseVisualStyleBackColor = true;
            this.btnClientConnect.Click += new System.EventHandler(this.btnClientConnect_Click);
            // 
            // txtClientNickname
            // 
            this.txtClientNickname.Location = new System.Drawing.Point(320, 12);
            this.txtClientNickname.Name = "txtClientNickname";
            this.txtClientNickname.Size = new System.Drawing.Size(112, 20);
            this.txtClientNickname.TabIndex = 5;
            this.txtClientNickname.Text = "Jaex";
            // 
            // txtClientIP
            // 
            this.txtClientIP.Location = new System.Drawing.Point(32, 12);
            this.txtClientIP.Name = "txtClientIP";
            this.txtClientIP.Size = new System.Drawing.Size(112, 20);
            this.txtClientIP.TabIndex = 3;
            this.txtClientIP.Text = "127.0.0.1";
            // 
            // lblClientNickname
            // 
            this.lblClientNickname.AutoSize = true;
            this.lblClientNickname.Location = new System.Drawing.Point(256, 16);
            this.lblClientNickname.Name = "lblClientNickname";
            this.lblClientNickname.Size = new System.Drawing.Size(58, 13);
            this.lblClientNickname.TabIndex = 2;
            this.lblClientNickname.Text = "Nickname:";
            // 
            // lblClientPort
            // 
            this.lblClientPort.AutoSize = true;
            this.lblClientPort.Location = new System.Drawing.Point(152, 16);
            this.lblClientPort.Name = "lblClientPort";
            this.lblClientPort.Size = new System.Drawing.Size(29, 13);
            this.lblClientPort.TabIndex = 1;
            this.lblClientPort.Text = "Port:";
            // 
            // lblClientIP
            // 
            this.lblClientIP.AutoSize = true;
            this.lblClientIP.Location = new System.Drawing.Point(8, 16);
            this.lblClientIP.Name = "lblClientIP";
            this.lblClientIP.Size = new System.Drawing.Size(20, 13);
            this.lblClientIP.TabIndex = 0;
            this.lblClientIP.Text = "IP:";
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpClient);
            this.tcMain.Controls.Add(this.tpServer);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(5, 5);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(816, 558);
            this.tcMain.TabIndex = 7;
            // 
            // tpClient
            // 
            this.tpClient.Controls.Add(this.txtClientMessageTo);
            this.tpClient.Controls.Add(this.btnClientCommands);
            this.tpClient.Controls.Add(this.txtClientPassword);
            this.tpClient.Controls.Add(this.lblClientPassword);
            this.tpClient.Controls.Add(this.nudClientPort);
            this.tpClient.Controls.Add(this.pClientChat);
            this.tpClient.Controls.Add(this.lvClientUsers);
            this.tpClient.Controls.Add(this.btnClientConnect);
            this.tpClient.Controls.Add(this.btnClientSend);
            this.tpClient.Controls.Add(this.txtClientNickname);
            this.tpClient.Controls.Add(this.txtClientMessage);
            this.tpClient.Controls.Add(this.txtClientIP);
            this.tpClient.Controls.Add(this.lblClientNickname);
            this.tpClient.Controls.Add(this.lblClientIP);
            this.tpClient.Controls.Add(this.lblClientPort);
            this.tpClient.Location = new System.Drawing.Point(4, 22);
            this.tpClient.Name = "tpClient";
            this.tpClient.Padding = new System.Windows.Forms.Padding(3);
            this.tpClient.Size = new System.Drawing.Size(808, 532);
            this.tpClient.TabIndex = 0;
            this.tpClient.Text = "Client";
            this.tpClient.UseVisualStyleBackColor = true;
            // 
            // txtClientMessageTo
            // 
            this.txtClientMessageTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClientMessageTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClientMessageTo.Location = new System.Drawing.Point(586, 502);
            this.txtClientMessageTo.Name = "txtClientMessageTo";
            this.txtClientMessageTo.Size = new System.Drawing.Size(102, 20);
            this.txtClientMessageTo.TabIndex = 16;
            // 
            // btnClientCommands
            // 
            this.btnClientCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClientCommands.Location = new System.Drawing.Point(694, 500);
            this.btnClientCommands.Name = "btnClientCommands";
            this.btnClientCommands.Size = new System.Drawing.Size(30, 24);
            this.btnClientCommands.TabIndex = 15;
            this.btnClientCommands.Text = "...";
            this.btnClientCommands.UseVisualStyleBackColor = true;
            this.btnClientCommands.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnClientCommands_MouseClick);
            // 
            // txtClientPassword
            // 
            this.txtClientPassword.Location = new System.Drawing.Point(504, 12);
            this.txtClientPassword.Name = "txtClientPassword";
            this.txtClientPassword.Size = new System.Drawing.Size(112, 20);
            this.txtClientPassword.TabIndex = 14;
            // 
            // lblClientPassword
            // 
            this.lblClientPassword.AutoSize = true;
            this.lblClientPassword.Location = new System.Drawing.Point(440, 16);
            this.lblClientPassword.Name = "lblClientPassword";
            this.lblClientPassword.Size = new System.Drawing.Size(56, 13);
            this.lblClientPassword.TabIndex = 13;
            this.lblClientPassword.Text = "Password:";
            // 
            // nudClientPort
            // 
            this.nudClientPort.Location = new System.Drawing.Point(184, 12);
            this.nudClientPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudClientPort.Name = "nudClientPort";
            this.nudClientPort.Size = new System.Drawing.Size(63, 20);
            this.nudClientPort.TabIndex = 12;
            this.nudClientPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudClientPort.Value = new decimal(new int[] {
            56863,
            0,
            0,
            0});
            // 
            // pClientChat
            // 
            this.pClientChat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pClientChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pClientChat.Controls.Add(this.rtbClientChat);
            this.pClientChat.Location = new System.Drawing.Point(8, 40);
            this.pClientChat.Name = "pClientChat";
            this.pClientChat.Size = new System.Drawing.Size(570, 456);
            this.pClientChat.TabIndex = 11;
            // 
            // rtbClientChat
            // 
            this.rtbClientChat.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rtbClientChat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbClientChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbClientChat.Location = new System.Drawing.Point(0, 0);
            this.rtbClientChat.Name = "rtbClientChat";
            this.rtbClientChat.ReadOnly = true;
            this.rtbClientChat.Size = new System.Drawing.Size(568, 454);
            this.rtbClientChat.TabIndex = 7;
            this.rtbClientChat.Text = "";
            this.rtbClientChat.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbClientChat_LinkClicked);
            // 
            // lvClientUsers
            // 
            this.lvClientUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvClientUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvClientUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chClientNickname});
            this.lvClientUsers.FullRowSelect = true;
            this.lvClientUsers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvClientUsers.Location = new System.Drawing.Point(586, 40);
            this.lvClientUsers.Name = "lvClientUsers";
            this.lvClientUsers.Size = new System.Drawing.Size(214, 456);
            this.lvClientUsers.TabIndex = 10;
            this.lvClientUsers.UseCompatibleStateImageBehavior = false;
            this.lvClientUsers.View = System.Windows.Forms.View.Details;
            this.lvClientUsers.DoubleClick += new System.EventHandler(this.lvClientUsers_DoubleClick);
            // 
            // chClientNickname
            // 
            this.chClientNickname.Text = "User list";
            this.chClientNickname.Width = 214;
            // 
            // btnClientSend
            // 
            this.btnClientSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClientSend.Location = new System.Drawing.Point(728, 500);
            this.btnClientSend.Name = "btnClientSend";
            this.btnClientSend.Size = new System.Drawing.Size(72, 24);
            this.btnClientSend.TabIndex = 9;
            this.btnClientSend.Text = "Send";
            this.btnClientSend.UseVisualStyleBackColor = true;
            this.btnClientSend.Click += new System.EventHandler(this.btnClientSend_Click);
            // 
            // txtClientMessage
            // 
            this.txtClientMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClientMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClientMessage.Location = new System.Drawing.Point(8, 502);
            this.txtClientMessage.Name = "txtClientMessage";
            this.txtClientMessage.Size = new System.Drawing.Size(570, 20);
            this.txtClientMessage.TabIndex = 8;
            this.txtClientMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClientMessage_KeyDown);
            // 
            // tpServer
            // 
            this.tpServer.Controls.Add(this.txtServerPassword);
            this.tpServer.Controls.Add(this.lblServerPassword);
            this.tpServer.Controls.Add(this.btnServerSend);
            this.tpServer.Controls.Add(this.txtServerMessage);
            this.tpServer.Controls.Add(this.nudServerPort);
            this.tpServer.Controls.Add(this.pServerConsole);
            this.tpServer.Controls.Add(this.lvServerUsers);
            this.tpServer.Controls.Add(this.btnServerStart);
            this.tpServer.Controls.Add(this.lblServerPort);
            this.tpServer.Location = new System.Drawing.Point(4, 22);
            this.tpServer.Name = "tpServer";
            this.tpServer.Size = new System.Drawing.Size(808, 532);
            this.tpServer.TabIndex = 1;
            this.tpServer.Text = "Server";
            this.tpServer.UseVisualStyleBackColor = true;
            // 
            // txtServerPassword
            // 
            this.txtServerPassword.Location = new System.Drawing.Point(176, 12);
            this.txtServerPassword.Name = "txtServerPassword";
            this.txtServerPassword.Size = new System.Drawing.Size(112, 20);
            this.txtServerPassword.TabIndex = 13;
            // 
            // lblServerPassword
            // 
            this.lblServerPassword.AutoSize = true;
            this.lblServerPassword.Location = new System.Drawing.Point(112, 16);
            this.lblServerPassword.Name = "lblServerPassword";
            this.lblServerPassword.Size = new System.Drawing.Size(56, 13);
            this.lblServerPassword.TabIndex = 12;
            this.lblServerPassword.Text = "Password:";
            // 
            // btnServerSend
            // 
            this.btnServerSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnServerSend.Location = new System.Drawing.Point(728, 500);
            this.btnServerSend.Name = "btnServerSend";
            this.btnServerSend.Size = new System.Drawing.Size(72, 24);
            this.btnServerSend.TabIndex = 11;
            this.btnServerSend.Text = "Send";
            this.btnServerSend.UseVisualStyleBackColor = true;
            this.btnServerSend.Click += new System.EventHandler(this.btnServerSend_Click);
            // 
            // txtServerMessage
            // 
            this.txtServerMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServerMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServerMessage.Location = new System.Drawing.Point(8, 502);
            this.txtServerMessage.Name = "txtServerMessage";
            this.txtServerMessage.Size = new System.Drawing.Size(712, 20);
            this.txtServerMessage.TabIndex = 10;
            this.txtServerMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtServerMessage_KeyDown);
            // 
            // nudServerPort
            // 
            this.nudServerPort.Location = new System.Drawing.Point(41, 12);
            this.nudServerPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudServerPort.Name = "nudServerPort";
            this.nudServerPort.Size = new System.Drawing.Size(63, 20);
            this.nudServerPort.TabIndex = 7;
            this.nudServerPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudServerPort.Value = new decimal(new int[] {
            56863,
            0,
            0,
            0});
            // 
            // pServerConsole
            // 
            this.pServerConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pServerConsole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pServerConsole.Controls.Add(this.rtbServerConsole);
            this.pServerConsole.Location = new System.Drawing.Point(8, 40);
            this.pServerConsole.Name = "pServerConsole";
            this.pServerConsole.Size = new System.Drawing.Size(570, 456);
            this.pServerConsole.TabIndex = 8;
            // 
            // rtbServerConsole
            // 
            this.rtbServerConsole.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rtbServerConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbServerConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbServerConsole.Location = new System.Drawing.Point(0, 0);
            this.rtbServerConsole.Name = "rtbServerConsole";
            this.rtbServerConsole.ReadOnly = true;
            this.rtbServerConsole.Size = new System.Drawing.Size(568, 454);
            this.rtbServerConsole.TabIndex = 6;
            this.rtbServerConsole.Text = "";
            this.rtbServerConsole.WordWrap = false;
            // 
            // lvServerUsers
            // 
            this.lvServerUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvServerUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvServerUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chServerNickname});
            this.lvServerUsers.FullRowSelect = true;
            this.lvServerUsers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvServerUsers.Location = new System.Drawing.Point(586, 40);
            this.lvServerUsers.Name = "lvServerUsers";
            this.lvServerUsers.Size = new System.Drawing.Size(214, 456);
            this.lvServerUsers.TabIndex = 7;
            this.lvServerUsers.UseCompatibleStateImageBehavior = false;
            this.lvServerUsers.View = System.Windows.Forms.View.Details;
            // 
            // chServerNickname
            // 
            this.chServerNickname.Text = "User list";
            this.chServerNickname.Width = 214;
            // 
            // cmsClientCommands
            // 
            this.cmsClientCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiClientTextColor,
            this.tsmiClientSendPing});
            this.cmsClientCommands.Name = "cmsClientCommands";
            this.cmsClientCommands.ShowImageMargin = false;
            this.cmsClientCommands.Size = new System.Drawing.Size(152, 70);
            // 
            // tsmiClientTextColor
            // 
            this.tsmiClientTextColor.Name = "tsmiClientTextColor";
            this.tsmiClientTextColor.Size = new System.Drawing.Size(151, 22);
            this.tsmiClientTextColor.Text = "Change text color...";
            this.tsmiClientTextColor.Click += new System.EventHandler(this.tsmiClientTextColor_Click);
            // 
            // tsmiClientSendPing
            // 
            this.tsmiClientSendPing.Name = "tsmiClientSendPing";
            this.tsmiClientSendPing.Size = new System.Drawing.Size(151, 22);
            this.tsmiClientSendPing.Text = "Send ping";
            this.tsmiClientSendPing.Click += new System.EventHandler(this.tsmiClientSendPing_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 568);
            this.Controls.Add(this.tcMain);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.tcMain.ResumeLayout(false);
            this.tpClient.ResumeLayout(false);
            this.tpClient.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudClientPort)).EndInit();
            this.pClientChat.ResumeLayout(false);
            this.tpServer.ResumeLayout(false);
            this.tpServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudServerPort)).EndInit();
            this.pServerConsole.ResumeLayout(false);
            this.cmsClientCommands.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblServerPort;
        private System.Windows.Forms.Button btnServerStart;
        private System.Windows.Forms.Label lblClientNickname;
        private System.Windows.Forms.Label lblClientPort;
        private System.Windows.Forms.Label lblClientIP;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpClient;
        private System.Windows.Forms.TextBox txtClientNickname;
        private System.Windows.Forms.TextBox txtClientIP;
        private System.Windows.Forms.Button btnClientConnect;
        private System.Windows.Forms.RichTextBox rtbClientChat;
        private System.Windows.Forms.Button btnClientSend;
        private System.Windows.Forms.TextBox txtClientMessage;
        private System.Windows.Forms.TabPage tpServer;
		private System.Windows.Forms.RichTextBox rtbServerConsole;
        private System.Windows.Forms.ListView lvServerUsers;
        private System.Windows.Forms.ColumnHeader chServerNickname;
        private System.Windows.Forms.ListView lvClientUsers;
        private System.Windows.Forms.ColumnHeader chClientNickname;
        private System.Windows.Forms.Panel pClientChat;
        private System.Windows.Forms.NumericUpDown nudClientPort;
        private System.Windows.Forms.NumericUpDown nudServerPort;
        private System.Windows.Forms.Panel pServerConsole;
        private System.Windows.Forms.Button btnServerSend;
        private System.Windows.Forms.TextBox txtServerMessage;
        private System.Windows.Forms.TextBox txtClientPassword;
        private System.Windows.Forms.Label lblClientPassword;
        private System.Windows.Forms.TextBox txtServerPassword;
        private System.Windows.Forms.Label lblServerPassword;
        private System.Windows.Forms.Button btnClientCommands;
        private System.Windows.Forms.TextBox txtClientMessageTo;
        private System.Windows.Forms.ContextMenuStrip cmsClientCommands;
        private System.Windows.Forms.ToolStripMenuItem tsmiClientTextColor;
        private System.Windows.Forms.ToolStripMenuItem tsmiClientSendPing;
    }
}

