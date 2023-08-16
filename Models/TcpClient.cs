using System.Net.Sockets;
using System.Text;

namespace ChatApp.Models
{
    public class TcpClient : Client
    {
        public TcpClient() : base()
        {
            _sender = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }
        protected override void Send(string msg)
        {
            byte[] messageSent;
            int byteSent;
            messageSent = Encoding.ASCII.GetBytes(msg);
            byteSent = _sender.Send(messageSent);
        }
        protected override string Receive()
        {
            byte[] messageReceived = new byte[1024];
            int byteRecv = _sender.Receive(messageReceived, SocketFlags.None);
            return Encoding.ASCII.GetString(messageReceived, 0, byteRecv);
        }
    }
}
