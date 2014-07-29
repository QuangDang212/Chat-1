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
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Chat
{
    public class Client
    {
        public delegate void StringEventHandler(object sender, string text);
        public event StringEventHandler MessageReceived;

        public delegate void DisconnectEventHandler(object sender, string reason);
        public event DisconnectEventHandler Disconnected;

        public bool Authorized { get; set; }
        public UserInfo UserInfo { get; set; }
        public string IP { get; private set; }

        private TcpClient client;
        private int bufferSize = 1024;
        private bool isConnected;

        public Client(TcpClient client)
        {
            this.client = client;
            Initialize();
        }

        public Client(string ip, int port)
        {
            client = new TcpClient(ip, port);
            Initialize();
        }

        private void Initialize()
        {
            isConnected = true;
            UserInfo = new UserInfo("Unknown");
            IP = client.Client.RemoteEndPoint.ToString();
            BeginRead();
        }

        public void Disconnect()
        {
            OnDisconnected(this, "User disconnected.");
            client.Close();
        }

        private void BeginRead()
        {
            if (client.Connected)
            {
                byte[] buffer = new byte[bufferSize];
                client.GetStream().BeginRead(buffer, 0, bufferSize, ReceiveMessages, buffer);
            }
        }

        private void ReceiveMessages(IAsyncResult result)
        {
            if (isConnected)
            {
                int length;

                try
                {
                    length = client.GetStream().EndRead(result);
                }
                catch (Exception e)
                {
                    OnDisconnected(this, e.Message);
                    return;
                }

                if (length > 0)
                {
                    byte[] buffer = (byte[])result.AsyncState;
                    string text = Encoding.UTF8.GetString(buffer, 0, length);
                    OnMessageReceived(text);
                }

                BeginRead();
            }
        }

        protected void OnMessageReceived(string text)
        {
            if (MessageReceived != null)
            {
                MessageReceived(this, text);
            }
        }

        protected void OnDisconnected(object sender, string reason)
        {
            isConnected = false;
            if (Disconnected != null)
            {
                Disconnected(sender, reason);
            }
        }

        public void SendPacket(PacketInfo packetInfo)
        {
            StreamWriter sw = new StreamWriter(client.GetStream());
            string data = JsonConvert.SerializeObject(packetInfo);
            Debug.WriteLine("SendPacket: " + data);
            sw.Write(data);
            sw.Flush();
        }
    }
}