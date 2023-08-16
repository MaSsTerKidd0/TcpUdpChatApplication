using System.Net.Sockets;
using System.Text;

namespace ChatApp.Models
{
    public class UdpClient : Client
    {
        public UdpClient() : base()
        {
            _sender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        protected override void Send(string msg)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(msg);
            _sender.SendTo(buffer, _localEndPoint);
        }
        protected override string Receive()
        {
            string msg = "";
            int byteRecv;
            byte[] messageReceived = new byte[1024];

            byteRecv = _sender.Receive(messageReceived);
            msg = Encoding.ASCII.GetString(messageReceived, 0, byteRecv);
            return msg;
        }
    }
}
