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
        private const int localPort = 4000;
        private const int remotePort = 4000;

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

                    // TODO: через Invoke добавить сообщение в окно чата.

                    // TODO вывод remoteIp чтоб знать от кого.
                }
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
