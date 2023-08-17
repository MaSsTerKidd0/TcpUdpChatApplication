﻿using System;
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
        protected const int EXIT = 4;
        string serverResponse = "";


        static Dictionary<int, string> reqTypes = new Dictionary<int, string>() {
            { 0, "100#" },
            { 1, "101#" },
            { 2, "200#" },
            { EXIT, "300#"}
        };

        static Dictionary<int, Func<String>> reqFunctions = new Dictionary<int, Func<String>>()
        {
            { 1, SendBroadCastMsg}
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
            string clientRequest = "";
            string respone = "";

            Task.Run(async () =>
            {
                while (true)
                {
                    serverResponse = Receive();
                    if (serverResponse != "")
                    {
                        MessageBox.Show(serverResponse);
                        serverResponse = "";
                    }
                }
            });

            Send(reqTypes[0] + ClientInfo.Instance.UserName);
            LoadAvailableClients();
        }


        //TODO: If Pressed X Button call The Exit function
        protected virtual void CloseConnection()
        {
            _sender.Shutdown(SocketShutdown.Both);
            _sender.Close();
            _sender.Dispose();
        }
        protected abstract void Send(string msg);
        protected abstract string Receive();

        private static string SendBroadCastMsg()
        {
            string msg;
            Console.WriteLine("Please enter The Msg you want to BroadCast: ");
            msg = Console.ReadLine();
            return msg;
        }

        private void LoadAvailableClients()
        {
            string clientRequest = reqTypes[2];
            Send(clientRequest);
        }


    }
}