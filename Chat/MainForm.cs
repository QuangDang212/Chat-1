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

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Chat
{
    public partial class MainForm : Form
    {
        private ServerManager server;
        private ClientManager client;

        public MainForm()
        {
            InitializeComponent();
        }

        private void AddMessageClient(string message)
        {
            AddMessage(rtbClientChat, message);
        }

        private void AddMessageServer(string message)
        {
            AddMessage(rtbServerConsole, message);
        }

        private void AddMessage(RichTextBox textBox, string message)
        {
            if (textBox.TextLength > 0)
            {
                textBox.AppendText("\r\n");
            }
            textBox.SelectionColor = Color.Gray;
            textBox.AppendText(DateTime.Now.ToLongTimeString() + " ");
            textBox.SelectionColor = Color.Black;
            textBox.AppendText(message);
        }

        private void ClientSendMessage()
        {
            if (client != null && client.IsConnected)
            {
                client.SendMessage(txtClientMessage.Text);
                txtClientMessage.ResetText();
            }
        }

        private void ServerSendMessage()
        {
            if (server != null && server.IsWorking)
            {
                server.SendMessageToAll("Admin", txtServerMessage.Text);
                txtServerMessage.ResetText();
            }
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            btnServerStart.Enabled = false;
            if (btnServerStart.Text == "Start server")
            {
                if (nudServerPort.Value > 0)
                {
                    btnServerStart.Text = "Stop server";
                    server = new ServerManager((int)nudServerPort.Value, txtServerPassword.Text);
                    server.ConsoleOutput += server_ConsoleOutput;
                    server.UserConnected += server_UserConnected;
                    server.UserDisconnected += server_UserDisconnected;
                    server.StartServer();
                    btnServerSend.Enabled = true;
                }
            }
            else
            {
                btnServerStart.Text = "Start server";
                server.StopServer();
                btnServerSend.Enabled = false;
            }
            btnServerStart.Enabled = true;
        }

        private void server_ConsoleOutput(string text)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                AddMessageServer(text);
            }));
        }

        private void server_UserConnected(UserInfo userInfo)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                lvServerUsers.Items.Add(userInfo.Nickname);
            }));
        }

        private void server_UserDisconnected(UserInfo userInfo)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                ListViewItem lvi = ServerFindUser(userInfo);
                if (lvi != null) lvi.Remove();
            }));
        }

        private ListViewItem ServerFindUser(UserInfo userInfo)
        {
            foreach (ListViewItem lvi in lvServerUsers.Items)
            {
                if (lvi.Text == userInfo.Nickname) return lvi;
            }

            return null;
        }

        private ListViewItem ClientFindUser(UserInfo userInfo)
        {
            foreach (ListViewItem lvi in lvClientUsers.Items)
            {
                if (lvi.Text == userInfo.Nickname) return lvi;
            }

            return null;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            btnClientConnect.Enabled = false;
            if (btnClientConnect.Text == "Connect")
            {
                if (!string.IsNullOrEmpty(txtClientIP.Text) && nudClientPort.Value > 0 && !string.IsNullOrEmpty(txtClientNickname.Text))
                {
                    client = new ClientManager(txtClientIP.Text, (int)nudClientPort.Value, txtClientNickname.Text);
                    client.Connect(txtClientPassword.Text);

                    if (client != null && client.IsConnected)
                    {
                        client.Notification += new ClientManager.StringEventHandler(client_Notification);
                        client.MessageReceived += new ClientManager.MessageEventHandler(client_MessageReceived);
                        client.UserConnected += client_UserConnected;
                        client.UserDisconnected += client_UserDisconnected;
                        client.UserListReceived += client_UserListReceived;
                        client.Kicked += client_Kicked;
                        btnClientConnect.Text = "Disconnect";
                        btnClientSend.Enabled = true;
                    }
                }
            }
            else
            {
                ClientDisconnect();
            }
            btnClientConnect.Enabled = true;
        }

        private void ClientDisconnect()
        {
            if (client != null && client.IsConnected)
            {
                client.Disconnect();
            }
            lvClientUsers.Items.Clear();
            btnClientConnect.Text = "Connect";
            btnClientSend.Enabled = false;
        }

        private void client_UserConnected(UserInfo userInfo)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                if (!lvClientUsers.Items.Cast<ListViewItem>().Any(x => x.Text == userInfo.Nickname))
                {
                    lvClientUsers.Items.Add(userInfo.Nickname);
                }
            }));
        }

        private void client_UserDisconnected(UserInfo userInfo)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                ListViewItem lvi = ClientFindUser(userInfo);
                if (lvi != null) lvi.Remove();
            }));
        }

        private void client_UserListReceived(UserInfo[] userList)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                foreach (UserInfo userInfo in userList)
                {
                    if (!lvClientUsers.Items.Cast<ListViewItem>().Any(x => x.Text == userInfo.Nickname))
                    {
                        lvClientUsers.Items.Add(userInfo.Nickname);
                    }
                }
            }));
        }

        private void client_Notification(string text)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                AddMessageClient(text);
            }));
        }

        private void client_Kicked(string reason)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                AddMessageClient("Kicked: " + reason);
                ClientDisconnect();
            }));
        }

        private void client_MessageReceived(MessageInfo messageInfo)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                string user;
                if (messageInfo.ToUser != null && !string.IsNullOrEmpty(messageInfo.ToUser.Nickname))
                {
                    user = string.Format("{0} -> {1}", messageInfo.FromUser.Nickname, messageInfo.ToUser.Nickname);
                }
                else
                {
                    user = messageInfo.FromUser.Nickname;
                }
                AddMessageClient(string.Format("{0}: {1}", user, messageInfo.Text));
            }));
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ClientSendMessage();
            }
        }

        private void btnClientSend_Click(object sender, EventArgs e)
        {
            ClientSendMessage();
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

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}