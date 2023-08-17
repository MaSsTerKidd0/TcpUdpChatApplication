using System;
using System.Net.Sockets;
using System.Text;

namespace ChatApp.Models
{
    public class TcpClient : Client
    {
        public TcpClient() : base()
        {
            _sender = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _localEndPoint.Port = 4320;
        }
        protected override void Send(string msg)
        {
            byte[] lengthBytes = BitConverter.GetBytes(msg.Length);
            byte[] messageBytes = Encoding.ASCII.GetBytes(msg);

            _sender.Send(lengthBytes);
            _sender.Send(messageBytes);
        }
        protected override string Receive()
        {
            byte[] messageReceived = new byte[1024];
            int byteRecv = _sender.Receive(messageReceived, 0, messageReceived.Length, SocketFlags.None);
            return Encoding.ASCII.GetString(messageReceived, 0, byteRecv);
        }
    }
}
