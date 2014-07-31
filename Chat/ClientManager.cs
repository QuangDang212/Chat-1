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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Chat
{
    public class ClientManager
    {
        public delegate void MessageEventHandler(MessageInfo messageInfo);
        public event MessageEventHandler MessageReceived;

        public delegate void StringEventHandler(string text);
        public event StringEventHandler Notification;

        public delegate void UserConnectedEventHandler(UserInfo userInfo);
        public event UserConnectedEventHandler UserConnected;

        public delegate void UserDisconnectedEventHandler(UserInfo userInfo);
        public event UserDisconnectedEventHandler UserDisconnected;

        public delegate void UserListReceivedEventHandler(UserInfo[] userList);
        public event UserListReceivedEventHandler UserListReceived;

        public delegate void KickedEventHandler(string reason);
        public event KickedEventHandler Kicked;

        public event Action<long> Pong;

        public bool IsConnected { get; private set; }
        public bool IsAuthenticated { get; private set; }

        public string IPAddress { get; set; }
        public int Port { get; set; }
        public string Nickname { get; set; }

        private Client client;
        private Stopwatch pingTimer;

        public ClientManager(string ip, int port, string nickname)
        {
            IPAddress = ip;
            Port = port;
            Nickname = nickname;
        }

        public void Connect(string password = "")
        {
            try
            {
                client = new Client(IPAddress, Port);
                client.UserInfo.Nickname = Nickname;
                client.MessageReceived += client_MessageReceived;
                client.Disconnected += client_Disconnected;
                PacketInfo packetInfo = new PacketInfo("Connect");
                packetInfo.AddParameter("Nickname", Nickname);
                if (!string.IsNullOrEmpty(password)) packetInfo.AddParameter("Password", password);
                client.SendPacket(packetInfo);
                IsConnected = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void client_Disconnected(Client client, string reason)
        {
            IsConnected = IsAuthenticated = false;
            OnNotification("Disconnected.");
        }

        public void Disconnect()
        {
            PacketInfo packetInfo = new PacketInfo("Disconnect");
            packetInfo.AddParameter("Reason", "User disconnect");
            client.SendPacket(packetInfo);
            client.Disconnect();
        }

        private void OnUserConnected(UserInfo info)
        {
            if (UserConnected != null) UserConnected(info);
        }

        private void OnUserDisconnected(UserInfo info)
        {
            if (UserDisconnected != null) UserDisconnected(info);
        }

        private void OnUserListReceived(UserInfo[] userList)
        {
            if (UserListReceived != null) UserListReceived(userList);
        }

        private void OnKicked(string reason)
        {
            if (Kicked != null) Kicked(reason);
        }

        private void OnPong(long elapsed)
        {
            if (Pong != null) Pong(elapsed);
        }

        private void client_MessageReceived(Client client, string text)
        {
            PacketInfo packetInfo = JsonConvert.DeserializeObject<PacketInfo>(text);

            switch (packetInfo.Command)
            {
                case "Connected":
                    UserInfo connectedUserInfo = packetInfo.GetData<UserInfo>();
                    OnNotification(connectedUserInfo.Nickname + " connected.");
                    OnUserConnected(connectedUserInfo);
                    break;
                case "Disconnected":
                    UserInfo disconnectedUserInfo = packetInfo.GetData<UserInfo>();
                    OnNotification(disconnectedUserInfo.Nickname + " disconnected.");
                    OnUserDisconnected(disconnectedUserInfo);
                    break;
                case "Message":
                    MessageInfo messageInfo = packetInfo.GetData<MessageInfo>();
                    OnMessageReceived(messageInfo);
                    break;
                case "UserList":
                    UserInfo[] userList = packetInfo.GetData<UserInfo[]>();
                    OnUserListReceived(userList);
                    break;
                case "Kick":
                    string kickReason = packetInfo.Parameters["Reason"];
                    OnKicked(kickReason);
                    break;
                case "Pong":
                    if (pingTimer != null)
                    {
                        OnPong(pingTimer.ElapsedMilliseconds);
                    }
                    break;
            }
        }

        public void SendMessage(string message, string toUser, Color color)
        {
            if (!string.IsNullOrEmpty(message))
            {
                PacketInfo packetInfo = new PacketInfo("Message");
                MessageInfo messageInfo = new MessageInfo(message);
                if (!string.IsNullOrEmpty(toUser)) messageInfo.ToUser = new UserInfo(toUser);
                messageInfo.TextColor = color;
                packetInfo.Data = messageInfo;
                client.SendPacket(packetInfo);
            }
        }

        public void SendPing()
        {
            pingTimer = Stopwatch.StartNew();
            client.SendPacket(new PacketInfo("Ping"));
        }

        protected void OnNotification(string text)
        {
            if (Notification != null)
            {
                Notification(text);
            }
        }

        protected void OnMessageReceived(MessageInfo messageInfo)
        {
            if (MessageReceived != null)
            {
                MessageReceived(messageInfo);
            }
        }
    }
}