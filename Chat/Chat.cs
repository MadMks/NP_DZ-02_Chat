﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat
{
    class Chat
    {
        private const int localPort = 4000;
        private const int remotePort = 4000;

        private Socket socket = null;

        public TextBox ChatMessages { get; set; }
        public string Login { get; set; }
        
        public Chat()
        {
            try
            {
                socket = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Dgram,
                    ProtocolType.Udp);

                socket.SetSocketOption(
                    SocketOptionLevel.Socket,
                    SocketOptionName.Broadcast,
                    1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                this.Close();
            }
        }

        public Chat(TextBox textBoxChat) : this()
        {
            this.ChatMessages = textBoxChat;
        }

        internal void Send(string message)
        {
            try
            {
                byte[] buffer = Encoding.Unicode.GetBytes(
                this.CreateCompleteMessage(message));
                EndPoint remotePoint
                    = new IPEndPoint(
                        IPAddress.Broadcast,
                        remotePort);

                this.socket.SendTo(buffer, remotePoint);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal void Listening()
        {
            Task.Factory.StartNew(Listen);
        }

        private void Listen()
        {
            try
            {
                // Прослушиваем по адресу (наш локальный адрес).
                IPEndPoint localIp
                    = new IPEndPoint(
                        this.GetLocalIpAddress(),
                        localPort);

                socket.Bind(localIp);

                while (true)
                {
                    // Получаем сообщение.
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] buffer = new byte[256];
                    EndPoint remoteIp 
                        = new IPEndPoint(IPAddress.Any, remotePort);

                    do
                    {
                        bytes = socket.ReceiveFrom(buffer, ref remoteIp);
                        builder.Append(
                            Encoding.Unicode.GetString(buffer, 0, bytes)
                            );

                    } while (socket.Available > 0);


                    this.ChatMessages.Invoke(
                        new Action<string>(AddTextToChat),
                        builder.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string CreateCompleteMessage(string message)
        {
            message =
                this.Login + ": "
                + message;

            return message;
        }

        private void AddTextToChat(string newMessage)
        {
            this.ChatMessages.Text
                = this.ChatMessages.Text
                + newMessage
                + Environment.NewLine;
        }

        private IPAddress GetLocalIpAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }

            throw new Exception("No network adapters " +
                "with an IPv4 address in the system!");
        }

        internal void Close()
        {
            if (this.socket != null)
            {
                this.socket.Shutdown(SocketShutdown.Both);
                this.socket.Close();
                this.socket = null;
            }
        }
    }
}
