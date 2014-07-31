#region License Information (GPL v3)

/*
    Copyright (C) Jaex

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

using Chat.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Chat
{
    public partial class MainForm : Form
    {
        private bool clientConnected;

        public bool ClientConnected
        {
            get
            {
                return clientConnected;
            }
            set
            {
                clientConnected = value;

                if (clientConnected)
                {
                    btnClientConnect.Text = "Disconnect";
                }
                else
                {
                    btnClientConnect.Text = "Connect";
                    lvClientUsers.Items.Clear();
                }

                txtClientIP.Enabled = nudClientPort.Enabled = txtClientNickname.Enabled = txtClientPassword.Enabled = !clientConnected;
                btnClientCommands.Enabled = btnClientSend.Enabled = clientConnected;
            }
        }

        private bool serverConnected;

        public bool ServerConnected
        {
            get
            {
                return serverConnected;
            }
            set
            {
                serverConnected = value;

                if (serverConnected)
                {
                    btnServerStart.Text = "Stop server";
                }
                else
                {
                    btnServerStart.Text = "Start server";
                    lvServerUsers.Items.Clear();
                }

                nudServerPort.Enabled = txtServerNickname.Enabled = txtServerPassword.Enabled = !serverConnected;
                btnServerSend.Enabled = serverConnected;
            }
        }

        private Color textColor;

        public Color TextColor
        {
            get
            {
                return textColor;
            }
            set
            {
                textColor = value;
                txtClientMessage.ForeColor = textColor;
            }
        }

        private ClientManager client;
        private ServerManager server;

        public MainForm()
        {
            InitializeComponent();
            Icon = Resources.Icon;

            ClientConnected = false;
            ServerConnected = false;
            TextColor = Color.Black;
        }

        private void InvokeForm(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

        #region Client functions

        private void ClientConnect()
        {
            if (!string.IsNullOrEmpty(txtClientIP.Text) && nudClientPort.Value > 0 && !string.IsNullOrEmpty(txtClientNickname.Text))
            {
                client = new ClientManager(txtClientIP.Text, (int)nudClientPort.Value, txtClientNickname.Text);
                client.Connect(txtClientPassword.Text);

                if (client != null && client.IsConnected)
                {
                    client.Notification += client_Notification;
                    client.MessageReceived += client_MessageReceived;
                    client.UserConnected += client_UserConnected;
                    client.UserDisconnected += client_UserDisconnected;
                    client.UserListReceived += client_UserListReceived;
                    client.Kicked += client_Kicked;
                    client.Pong += client_Pong;

                    ClientConnected = true;
                }
            }
        }

        private void ClientDisconnect()
        {
            if (client != null && client.IsConnected)
            {
                client.Disconnect();
            }

            InvokeForm(() =>
            {
                ClientConnected = false;
            });
        }

        private void AddClientMessage(string message)
        {
            AddClientMessage(null, message, Color.Black);
        }

        private void AddClientMessage(string nick, string message, Color color)
        {
            InvokeForm(() =>
            {
                if (rtbClientChat.TextLength > 0)
                {
                    rtbClientChat.AppendText(Environment.NewLine);
                }

                rtbClientChat.SelectionColor = Color.Gray;
                rtbClientChat.AppendText(DateTime.Now.ToLongTimeString() + " ");
                rtbClientChat.SelectionColor = Color.Black;

                if (!string.IsNullOrEmpty(nick))
                {
                    rtbClientChat.AppendText(nick + ": ");
                    rtbClientChat.SelectionColor = color;
                }

                rtbClientChat.AppendText(message);
                rtbClientChat.ScrollToCaret();
            });
        }

        private void ClientSendMessage()
        {
            if (client != null && client.IsConnected)
            {
                client.SendMessage(txtClientMessage.Text, txtClientMessageTo.Text, TextColor);
                txtClientMessage.ResetText();
            }
        }

        private ListViewItem ClientFindUser(UserInfo userInfo)
        {
            foreach (ListViewItem lvi in lvClientUsers.Items)
            {
                if (lvi.Text == userInfo.Nickname) return lvi;
            }

            return null;
        }

        #endregion Client functions

        #region Server functions

        private void StartServer()
        {
            if (nudServerPort.Value > 0 && !string.IsNullOrEmpty(txtServerNickname.Text))
            {
                server = new ServerManager((int)nudServerPort.Value, txtServerNickname.Text, txtServerPassword.Text);
                server.ConsoleOutput += server_ConsoleOutput;
                server.UserConnected += server_UserConnected;
                server.UserDisconnected += server_UserDisconnected;
                server.StartServer();

                ServerConnected = true;
            }
        }

        private void StopServer()
        {
            if (server != null)
            {
                server.StopServer();
            }

            ServerConnected = false;
        }

        private void AddServerMessage(string message)
        {
            InvokeForm(() =>
            {
                rtbServerConsole.SelectionColor = Color.Gray;
                rtbServerConsole.AppendText(DateTime.Now.ToLongTimeString() + " ");
                rtbServerConsole.SelectionColor = Color.Black;
                rtbServerConsole.AppendText(message + Environment.NewLine);
                rtbServerConsole.ScrollToCaret();
            });
        }

        private void ServerSendMessage()
        {
            if (server != null && server.IsWorking)
            {
                server.SendMessageToAll(txtServerMessage.Text);
                txtServerMessage.ResetText();
            }
        }

        private ListViewItem ServerFindUser(UserInfo userInfo)
        {
            foreach (ListViewItem lvi in lvServerUsers.Items)
            {
                if (lvi.Text == userInfo.Nickname) return lvi;
            }

            return null;
        }

        #endregion Server functions

        #region Form events

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void btnClientConnect_Click(object sender, EventArgs e)
        {
            btnClientConnect.Enabled = false;

            if (!ClientConnected)
            {
                ClientConnect();
            }
            else
            {
                ClientDisconnect();
            }

            btnClientConnect.Enabled = true;
        }

        private void rtbClientChat_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void lvClientUsers_DoubleClick(object sender, EventArgs e)
        {
            if (lvClientUsers.SelectedItems.Count > 0)
            {
                txtClientMessageTo.Text = lvClientUsers.SelectedItems[0].Text;
            }
        }

        private void txtClientMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ClientSendMessage();
            }
        }

        private void btnClientCommands_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                cmsClientCommands.Show(btnClientCommands, e.X, e.Y);
            }
        }

        private void tsmiClientTextColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = TextColor;

                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    TextColor = colorDialog.Color;
                }
            }
        }

        private void tsmiClientSendPing_Click(object sender, EventArgs e)
        {
            client.SendPing();
        }

        private void btnClientSend_Click(object sender, EventArgs e)
        {
            ClientSendMessage();
        }

        private void btnServerStart_Click(object sender, EventArgs e)
        {
            btnServerStart.Enabled = false;

            if (!ServerConnected)
            {
                StartServer();
            }
            else
            {
                StopServer();
            }

            btnServerStart.Enabled = true;
        }

        private void txtServerMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ServerSendMessage();
            }
        }

        private void btnServerSend_Click(object sender, EventArgs e)
        {
            ServerSendMessage();
        }

        #endregion Form events

        #region Client events

        private void client_UserConnected(UserInfo userInfo)
        {
            InvokeForm(() =>
            {
                if (!lvClientUsers.Items.Cast<ListViewItem>().Any(x => x.Text == userInfo.Nickname))
                {
                    lvClientUsers.Items.Add(userInfo.Nickname);
                }
            });
        }

        private void client_UserDisconnected(UserInfo userInfo)
        {
            InvokeForm(() =>
            {
                ListViewItem lvi = ClientFindUser(userInfo);
                if (lvi != null) lvi.Remove();
            });
        }

        private void client_UserListReceived(UserInfo[] userList)
        {
            InvokeForm(() =>
            {
                foreach (UserInfo userInfo in userList)
                {
                    if (!lvClientUsers.Items.Cast<ListViewItem>().Any(x => x.Text == userInfo.Nickname))
                    {
                        lvClientUsers.Items.Add(userInfo.Nickname);
                    }
                }
            });
        }

        private void client_Notification(string text)
        {
            AddClientMessage(text);
        }

        private void client_Kicked(string reason)
        {
            AddClientMessage("Kicked: " + reason);
            ClientDisconnect();
        }

        private void client_Pong(long elapsed)
        {
            AddClientMessage("Pong: " + elapsed + "ms");
        }

        private void client_MessageReceived(MessageInfo messageInfo)
        {
            string user;

            if (messageInfo.ToUser != null && !string.IsNullOrEmpty(messageInfo.ToUser.Nickname))
            {
                user = string.Format("{0} > {1}", messageInfo.FromUser.Nickname, messageInfo.ToUser.Nickname);
            }
            else
            {
                user = messageInfo.FromUser.Nickname;
            }

            AddClientMessage(user, messageInfo.Text, messageInfo.TextColor);
        }

        #endregion Client events

        #region Server events

        private void server_ConsoleOutput(string text)
        {
            AddServerMessage(text);
        }

        private void server_UserConnected(UserInfo userInfo)
        {
            InvokeForm(() =>
            {
                lvServerUsers.Items.Add(userInfo.Nickname);
            });
        }

        private void server_UserDisconnected(UserInfo userInfo)
        {
            InvokeForm(() =>
            {
                ListViewItem lvi = ServerFindUser(userInfo);
                if (lvi != null) lvi.Remove();
            });
        }

        #endregion Server events
    }
}