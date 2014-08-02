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
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Chat
{
    public class ServerManager
    {
        public event Action<string> ConsoleOutput;
        public event Action<UserInfo> UserConnected;
        public event Action<UserInfo> UserDisconnected;

        public bool IsWorking { get; private set; }
        public List<Client> Clients { get; private set; }

        public int Port { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }

        private TcpListener tcpListener;

        public ServerManager(int port, string nickname, string password = "")
        {
            Port = port;
            Nickname = nickname;
            Password = password;
        }

        public void StartServer()
        {
            if (Port < 1 || Port > 65535)
            {
                throw new Exception("Invalid port number.");
            }

            if (string.IsNullOrEmpty(Nickname))
            {
                throw new Exception("Nickname can't be empty.");
            }

            IsWorking = true;
            Clients = new List<Client>();
            tcpListener = new TcpListener(IPAddress.Any, Port);
            tcpListener.Start();
            Thread listenThread = new Thread(ListenThread);
            listenThread.IsBackground = true;
            listenThread.Start();
            OnConsoleOutput("Server started.");
        }

        public void StopServer()
        {
            KickAllClients("Server closing.");
            IsWorking = false;
            tcpListener.Stop();
            OnConsoleOutput("Server stopped.");
        }

        private void ListenThread()
        {
            while (IsWorking)
            {
                try
                {
                    TcpClient incomingClient = tcpListener.AcceptTcpClient();
                    Client client = new Client(incomingClient);
                    client.MessageReceived += ClientMessageReceived;
                    client.Disconnected += ClientDisconnected;
                    OnConsoleOutput(string.Format("Client connected: {0}", client.IP));
                }
                catch (SocketException)
                {
                }
            }
        }

        protected void OnConsoleOutput(string text)
        {
            if (ConsoleOutput != null)
            {
                ConsoleOutput(text);
            }
        }

        protected void OnUserConnected(UserInfo userInfo)
        {
            if (UserConnected != null)
            {
                UserConnected(userInfo);
            }
        }

        protected void OnUserDisconnected(UserInfo userInfo)
        {
            if (UserDisconnected != null)
            {
                UserDisconnected(userInfo);
            }
        }

        private bool ClientConnect(Client client, string password = "")
        {
            if (!string.IsNullOrEmpty(Password) && !password.Equals(Password, StringComparison.InvariantCultureIgnoreCase))
            {
                KickClient(client, "Wrong password.");
                return false;
            }

            string nickname = client.UserInfo.Nickname;

            if (string.IsNullOrEmpty(nickname) || nickname.Equals(Nickname, StringComparison.InvariantCultureIgnoreCase))
            {
                KickClient(client, "Invalid nickname.");
                return false;
            }

            if (Clients.Any(x => nickname.Equals(x.UserInfo.Nickname, StringComparison.InvariantCultureIgnoreCase)))
            {
                KickClient(client, "Nickname is already in use: " + nickname);
                return false;
            }

            Clients.Add(client);
            OnUserConnected(client.UserInfo);
            client.Authorized = true;

            SendUserList(client);

            PacketInfo packetInfoConnected = new PacketInfo("Connected");
            packetInfoConnected.Data = client.UserInfo;
            SendToAll(packetInfoConnected);

            return true;
        }

        private void ClientDisconnected(Client client, string reason)
        {
            if (client.Authorized)
            {
                Clients.Remove(client);
                OnUserDisconnected(client.UserInfo);
                OnConsoleOutput(string.Format("Client disconnected: {0} {1} {2}", client.UserInfo.Nickname, client.IP, reason));

                if (IsWorking)
                {
                    PacketInfo packetInfoDisconnected = new PacketInfo("Disconnected");
                    packetInfoDisconnected.Data = client.UserInfo;
                    SendToAll(packetInfoDisconnected);
                }
            }
        }

        private void ClientMessageReceived(Client client, string text)
        {
            OnConsoleOutput(string.Format("{0} {1}: {2}", client.UserInfo.Nickname, client.IP, text));

            PacketInfo packetInfo;

            try
            {
                packetInfo = JsonConvert.DeserializeObject<PacketInfo>(text);
            }
            catch
            {
                return;
            }

            if (packetInfo != null && !string.IsNullOrEmpty(packetInfo.Command))
            {
                if (packetInfo.Command == "Connect")
                {
                    client.UserInfo.Nickname = packetInfo.GetParameter("Nickname");
                    string password = packetInfo.GetParameter("Password");
                    ClientConnect(client, password);
                }
                else if (client.Authorized)
                {
                    switch (packetInfo.Command)
                    {
                        case "Disconnect":
                            string reason = packetInfo.GetParameter("Reason");
                            ClientDisconnected(client, reason);
                            break;
                        case "Message":
                            if (client.Authorized)
                            {
                                PacketInfo packetInfoMessage = new PacketInfo("Message");
                                MessageInfo messageInfo = packetInfo.GetData<MessageInfo>();
                                messageInfo.FromUser = client.UserInfo;
                                packetInfoMessage.Data = messageInfo;

                                if (messageInfo.ToUser != null && !string.IsNullOrEmpty(messageInfo.ToUser.Nickname))
                                {
                                    SendTo(messageInfo.ToUser, packetInfoMessage);
                                    SendTo(messageInfo.FromUser, packetInfoMessage);
                                }
                                else
                                {
                                    SendToAll(packetInfoMessage);
                                }
                            }
                            break;
                        case "Ping":
                            SendTo(client.UserInfo, new PacketInfo("Pong"));
                            break;
                    }
                }
            }
        }

        public void KickClient(Client client, string reason = "")
        {
            PacketInfo packetInfo = new PacketInfo("Kick");
            if (!string.IsNullOrEmpty(reason)) packetInfo.AddParameter("Reason", reason);
            client.SendPacket(packetInfo);
            client.Disconnect();
        }

        public void KickAllClients(string reason)
        {
            foreach (Client client in Clients.ToArray())
            {
                KickClient(client, reason);
            }
        }

        public void SendUserList(Client client)
        {
            if (Clients.Count > 0)
            {
                PacketInfo packetInfo = new PacketInfo("UserList");
                packetInfo.Data = Clients.Select(x => x.UserInfo).ToArray();
                client.SendPacket(packetInfo);
            }
        }

        public bool SendTo(UserInfo userInfo, PacketInfo packetInfo)
        {
            if (userInfo != null && !string.IsNullOrEmpty(userInfo.Nickname))
            {
                Client client = Clients.FirstOrDefault(x => x.UserInfo.Nickname == userInfo.Nickname);

                if (client != null)
                {
                    client.SendPacket(packetInfo);
                    return true;
                }
            }

            return false;
        }

        public void SendMessageToAll(string message)
        {
            SendMessageToAll(Nickname, message);
        }

        public void SendMessageToAll(string nickname, string message)
        {
            if (!string.IsNullOrEmpty(nickname) && !string.IsNullOrEmpty(message))
            {
                PacketInfo packetInfoMessage = new PacketInfo("Message");
                MessageInfo messageInfo = new MessageInfo(message);
                messageInfo.FromUser = new UserInfo(nickname);
                packetInfoMessage.Data = messageInfo;
                SendToAll(packetInfoMessage);
            }
        }

        public void SendToAll(PacketInfo packetInfo)
        {
            foreach (Client client in Clients)
            {
                client.SendPacket(packetInfo);
            }
        }
    }
}