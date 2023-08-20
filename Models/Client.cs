using ChatApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;

namespace ChatApp.Models
{
    public abstract class Client
    {
        protected Socket _sender;
        protected int _port;
        protected IPAddress _ipAddress;
        protected IPEndPoint _localEndPoint;
        protected const string EXIT = "EXIT";
        string serverResponse = "";

        static Dictionary<string, string> reqTypes = new Dictionary<string, string>() {
            { "Register", "100#" },
            { "SendBroadCastMessage", "101#" },
            { "SendPrivateMessage", "102#" },
            { "ChatRequest", "103#" },
            { EXIT, "300#"}
        };

        public Client()
        {
            _ipAddress = IPAddress.Loopback;
            _localEndPoint = new IPEndPoint(_ipAddress, _port);
        }
        public void StartClient()
        {
            InitConnection();
        }
        protected virtual void InitConnection()
        {
            try
            {
                _sender.Connect(_localEndPoint);
                ExecuteClient();
            }
            catch (SocketException se)
            {
                MessageBox.Show("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("Unexpected exception : {0}", e.ToString());
            }
        }

        protected virtual void ExecuteClient()
        {
            Send(reqTypes["Register"] + ClientInfo.Instance.UserName);
            Task.Run(() =>
            {
                while (true)
                {
                    serverResponse = Receive();
                    ChatViewModel.HandleServerResponse(serverResponse);
                }
            });
        }

        public void CloseConnection()
        {
            _sender.Shutdown(SocketShutdown.Both);
            _sender.Close();
            _sender.Dispose();
        }
        protected abstract void Send(string msg);
        protected abstract string Receive();

        //private static string SendBroadCastMsg()
        //{
        //    string msg;
        //    Console.WriteLine("Please enter The Msg you want to BroadCast: ");
        //    msg = Console.ReadLine();
        //    return msg;
        //}


        //TODO:Change To message object
        public void SendMessageRequest(string clientToSendName, string msg)
        {
            string from = ClientInfo.Instance.UserName;
            string to = clientToSendName;

            string request = reqTypes["SendPrivateMessage"] + from + "#" + to + "#" + msg + "#" + DateTime.Now;
            Send(request);
        }
    }
}