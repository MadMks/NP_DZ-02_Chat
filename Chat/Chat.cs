using System;
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
        private const int port = 4000;

        private Socket socket = null;

        public Chat()
        {
            try
            {
                socket = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Dgram,
                    ProtocolType.Udp);
                
                // TODO -> setSocketOption
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // TODO закрыть сокет !!!
        }

        internal void Listening()
        {
            Task.Factory.StartNew(Listen);
        }

        private void Listen()
        {
            try
            {
                MessageBox.Show(GetLocalIpAddress());
                //MessageBox.Show()

                // Прослушиваем по адресу (наш локальный адрес).
                //IPEndPoint localIp 
                //    = new IPEndPoint(
                //        IPAddress)
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Close();
            }
        }

        private string GetLocalIpAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
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
