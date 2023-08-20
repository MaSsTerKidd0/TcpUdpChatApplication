using System.Collections.Generic;

namespace ChatApp.Models
{
    public class ClientInfo
    {
        #region Fields
        private static ClientInfo _instance;
        private Dictionary<string, Chat> _clientChats = new Dictionary<string, Chat>();
        #endregion

        #region Properties
        public Client Client { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public bool Sex { get; set; }
        public string BirthDate { get; set; }
        public Dictionary<string, Chat> ClientChats { get { return _clientChats; } }
        #endregion

        private ClientInfo()
        {
            _clientChats = new Dictionary<string, Chat>();
        }
        public static ClientInfo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ClientInfo();
                }
                return _instance;
            }
        }

        public void InitializeClientInfo(string userName, string password, int age, bool sex, string birthDate)
        {
            UserName = userName;
            Password = password;
            Age = age;
            Sex = sex;
            BirthDate = birthDate;
        }
    }
}
