namespace ChatApp.Models
{
    public class ClientInfo
    {
        private static ClientInfo _instance;

        #region Properties
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public bool Sex { get; set; }
        public string BirthDate { get; set; }
        private ClientInfo() { }
        #endregion
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
