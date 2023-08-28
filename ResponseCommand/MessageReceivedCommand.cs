using ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace ChatApp.ResponseCommand
{
    public class MessageReceivedCommand : IResponseCommand
    {
        ObservableCollection<Message> _messages;
        public MessageReceivedCommand(ObservableCollection<Message> messages)
        {
            _messages = messages;
        }
        public void Execute(string response)
        {
            string[] responseParts = response.Split('#');
            string from = responseParts[1];
            string to = responseParts[2];
            string dateTimeString = responseParts[3].ToString();
            string message = responseParts[4];

            CreateChat(from);
            CreateChat(to);

            Message newMessage = new Message
            {
                Sender = from == ClientInfo.Instance.UserName ? "Me" : from,
                MessageText = message,
                Background = (from == ClientInfo.Instance.UserName ? Colors.LightBlue.ToString() : Colors.LightGray.ToString()),
                TextColor = (from == ClientInfo.Instance.UserName ? Colors.Crimson.ToString() : Colors.Black.ToString()),
                Time = dateTimeString
            };

            ClientInfo.Instance.ClientChats[from].Messages.Add(newMessage);
            ClientInfo.Instance.ClientChats[to].Messages.Add(newMessage);

            _messages = new ObservableCollection<Message>(OrderMessagesByTime(to, from));
        }

        private List<Message> OrderMessagesByTime(string to, string from)
        {
            List<Message> allMessages = new List<Message>();
            DateTime time;
            allMessages.ToList().AddRange(ClientInfo.Instance.ClientChats[from].Messages);
            allMessages.ToList().AddRange(ClientInfo.Instance.ClientChats[to].Messages);

            List<Message> orderedMessages = allMessages.OrderBy(message => DateTime.TryParse(message.Time, out time) ? time : DateTime.MinValue).ToList();

            return orderedMessages;
        }
        private void CreateChat(string clientName)
        {
            if (!ClientInfo.Instance.ClientChats.ContainsKey(clientName))
            {
                ClientInfo.Instance.ClientChats.Add(clientName, new Chat());
            }
        }
    }
}
