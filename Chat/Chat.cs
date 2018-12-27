using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
