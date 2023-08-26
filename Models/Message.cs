using CommunityToolkit.Mvvm.ComponentModel;

namespace ChatApp.Models
{
    public class Message : ObservableObject
    {
        private string _sender;
        private string _messageText;
        private string _background;
        private string _textColor;
        private string _time;

        public string Sender
        {
            get => _sender;
            set => SetProperty(ref _sender, value);
        }

        public string MessageText
        {
            get => _messageText;
            set => SetProperty(ref _messageText, value);
        }
        public string Background
        {
            get => _background;
            set => SetProperty(ref _background, value);
        }
        public string TextColor
        {
            get => _textColor;
            set => SetProperty(ref _textColor, value);
        }
        public string Time
        {
            get => _time;
            set { _time = value; }
        }
    }
}
